using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextInt();
            A = sc.IntArray();

            /*
             * n箱送る
             * iにはa_i個ある
             *
             * 各箱の個数がkで割り切れるなら ok
             * k > 1
             *
             * 1秒でiにあるチョコをi+-1に移せる
             *
             * 何秒でokにできるか?
             */

            /*
             * 
             */

            long sum = 0;
            foreach (int i in A)
            {
                sum += i;
            }


            List<long> p = new List<long>();
            {
                long tmp = sum;
                for (long i = 2; i * i <= tmp; i++)
                {
                    if (tmp % i == 0)
                    {
                        p.Add(i);
                        while (tmp % i == 0)
                        {
                            tmp /= i;
                        }
                    }
                }

                if (tmp != 1) p.Add(tmp);
            }

            long ans = long.MaxValue;
            foreach (var k in p)
            {
                // mod k

                // 総和がk以上になるindexまで
                // ceil(k/2)以上になるindexに集める

                long cost = 0;
                long[] mod = new long[N];
                for (int i = 0; i < N; i++)
                {
                    mod[i] = A[i] % k;
                }

                int left = 0;
                long sum2 = 0;
                for (int r = 0; r < N; r++)
                {
                    sum2 += mod[r];
                    if (sum2 >= k)
                    {
                        long m = sum2 - k;
                        long sum3 = 0;
                        int mid = -1;
                        for (int i = left; i <= r; i++)
                        {
                            sum3 += mod[i];
                            if (sum3 >= (k + 1) / 2)
                            {
                                mid = i;
                                break;
                            }
                        }

                        for (int i = left; i <= r; i++)
                        {
                            cost += (i == r ? mod[r] - m : mod[i]) * Math.Abs(i - mid);
                        }

                        // Console.WriteLine($"{mid} {cost} {sum3} {left} {r}");

                        mod[r] = m;
                        sum2 = m;
                        left = r;
                    }
                }

                ans = Math.Min(ans, cost);

                // Console.WriteLine($"{k} {cost}");
            }

            if (ans == long.MaxValue)
            {
                Console.WriteLine("-1");
            }
            else
            {
                Console.WriteLine(ans);
            }
        }
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}