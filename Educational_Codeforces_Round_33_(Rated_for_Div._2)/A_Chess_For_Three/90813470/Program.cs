using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            a[i] = sc.NextInt();
        }

        /*
         * 3人で勝負
         * 
         * 最初 1,2が戦う
         * 勝ったほうが次の勝負　観戦と戦う
         * 
         * 勝ったリスト正しいか?
         */

        // 勝負 2人
        int s = 1;
        int t = 2;

        // 観戦
        int u = 3;

        for (int i = 0; i < n; i++)
        {
            if (a[i] == s || a[i] == t)
            {
                int ns = a[i];
                int nt = u;
                int nu = s + t - a[i];

                s = ns;
                t = nt;
                u = nu;
            }
            else
            {
                Console.WriteLine("NO");
                return;
            }
        }

        Console.WriteLine("YES");

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
