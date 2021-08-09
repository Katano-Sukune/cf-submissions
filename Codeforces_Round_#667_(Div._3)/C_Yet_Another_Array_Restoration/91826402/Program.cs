using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int x = sc.NextInt();
        int y = sc.NextInt();

        /*
         * 配列復元
         * 
         * n個の正整数
         * 
         * x < y
         * x,yが含まれる
         * 
         * 昇順に並べると隣接2個の差は等しい
         * 
         * 最大値が最小のa
         */

        int diff = y - x;
        int d = -1;
        int max = int.MaxValue;
        for (int i = 1; i * i <= diff; i++)
        {
            if (diff % i == 0)
            {
                int j = diff / i;

                // 差 i
                {
                    int m = y % i;
                    if (m == 0) m += i;

                    int tmp = m + (n - 1) * i;
                    tmp = Math.Max(tmp, y);

                    int tmpMin = tmp - (n - 1) * i;

                    if (tmpMin <= x && tmp < max)
                    {
                        d = i;
                        max = tmp;
                    }
                }
                {
                    int m = y % j;
                    if (m == 0) m += j;

                    int tmp = m + (n - 1) * j;
                    tmp = Math.Max(tmp, y);
                    int tmpMin = tmp - (n - 1) * j;

                    if (tmpMin <= x && tmp < max)
                    {
                        d = j;
                        max = tmp;
                    }
                }
            }
        }

        int[] ans = new int[n];
        ans[n - 1] = max;
        for (int i = n - 2; i >= 0; i--)
        {
            ans[i] = ans[i + 1] - d;
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
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
