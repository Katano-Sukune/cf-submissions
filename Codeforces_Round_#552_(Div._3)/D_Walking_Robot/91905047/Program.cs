using CompLib.Util;
using System;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int b = sc.NextInt();
        int a = sc.NextInt();
        int[] s = sc.IntArray();

        /*
         * 0のとき
         */
        // できるだけaで動く
        // 1の区間はbで動く

        /*
         * bで動いてaが満充電になる
         * aで動く
         * 
         */

        int tB = b;
        int tA = a;
        int ans = 0;
        for (int i = 0; i < n; i++)
        {
            if (s[i] == 1)
            {
                if (tA == a || (tB == 0 && tA > 0))
                {
                    tA--;
                }
                else if (tB > 0)
                {
                    tA++;
                    tB--;
                }
                else
                {
                    break;
                }
                ans++;

            }
            else
            {
                if (tA > 0)
                {
                    tA--;
                }
                else if (tB > 0)
                {
                    tB--;
                }
                else
                {
                    break;
                }
                ans++;
            }

            // Console.WriteLine($"{tB} {tA}");
        }

        Console.WriteLine(ans);
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
