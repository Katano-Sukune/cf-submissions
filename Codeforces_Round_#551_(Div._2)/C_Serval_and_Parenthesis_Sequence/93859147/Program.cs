using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();

        /*
         * sの?を()にする
         *
         * 全体がかっこ列
         * かつ
         * 接頭辞が非カッコ列
         */
        if (n % 2 != 0)
        {
            Console.WriteLine(":(");
            return;
        }

        int cntOpen = s.Count(c => c == '(');
        int cntClose = s.Count(c => c == ')');
        int cntQ = n - (cntOpen + cntClose);

        // cntOpen + rOpen = cntClose + rClose
        // rOpen + rClose = cntQ
        // rOpen = cntQ - rClose
        // cntOpen + cntQ - rClose = cntClose + rClose
        // 2rClose = cntOpen + cntQ - cntClose 
        // 
        int rOpen, rClose;
        int tmp = cntOpen + cntQ - cntClose;
        if (tmp % 2 != 0)
        {
            Console.WriteLine(":(");
            return;
        }

        rClose = tmp / 2;
        rOpen = cntQ - rClose;
        if (rClose < 0 || rOpen < 0)
        {
            Console.WriteLine(":(");
            return;
        }

        var ans = s.ToCharArray();
        for (int i = 0; i < n; i++)
        {
            if (s[i] != '?') continue;
            if (rOpen > 0)
            {
                ans[i] = '(';
                rOpen--;
            }
            else if (rClose > 0)
            {
                ans[i] = ')';
                rClose--;
            }
        }

        int cnt = 0;
        for (int i = 0; i < n; i++)
        {
            if (ans[i] == '(')
            {
                cnt++;
            }
            else
            {
                cnt--;
            }

            if (i + 1 < n && cnt <= 0)
            {
                Console.WriteLine(":(");
                return;
            }
        }

        if (cnt != 0)
        {
            Console.WriteLine(":(");
            return;
        }

        Console.WriteLine(new string(ans));
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