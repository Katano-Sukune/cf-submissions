using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int r = sc.NextInt();
        int c = sc.NextInt();

        string[] s = new string[r];
        for (int i = 0; i < r; i++)
        {
            s[i] = sc.Next();
        }

        /*
         * 高さ or 幅 1の矩形を選ぶ
         *
         * 移動させる
         */

        /*
         * 元から全部A 0
         * 全部P MORTAL
         *
         * 1辺全部A 1
         *
         * 縁から縁までA 2
         *
         * 角にあるA 2
         *
         * 縁にあるA 3
         *
         * else 4
         */

        bool allA = true;
        bool allP = true;
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                allA &= s[i][j] == 'A';
                allP &= s[i][j] == 'P';
            }
        }

        if (allA)
        {
            Console.WriteLine("0");
            return;
        }

        if (allP)
        {
            Console.WriteLine("MORTAL");
            return;
        }

        bool fL = true;
        bool fR = true;
        for (int i = 0; i < r; i++)
        {
            fL &= s[i][0] == 'A';
            fR &= s[i][c - 1] == 'A';
        }

        bool fU = true;
        bool fD = true;
        for (int j = 0; j < c; j++)
        {
            fU &= s[0][j] == 'A';
            fD &= s[r - 1][j] == 'A';
        }

        if (fL || fR || fU || fD)
        {
            Console.WriteLine("1");
            return;
        }

        if (s[0][0] == 'A' || s[0][c - 1] == 'A' || s[r - 1][0] == 'A' || s[r - 1][c - 1] == 'A')
        {
            Console.WriteLine("2");
            return;
        }

        for (int i = 0; i < r; i++)
        {
            bool f = true;
            for (int j = 0; j < c; j++)
            {
                f &= s[i][j] == 'A';
            }

            if (f)
            {
                Console.WriteLine("2");
                return;
            }
        }

        for (int j = 0; j < c; j++)
        {
            bool f = true;
            for (int i = 0; i < r; i++)
            {
                f &= s[i][j] == 'A';
            }
            
            if (f)
            {
                Console.WriteLine("2");
                return;
            }
        }

        for (int i = 0; i < r; i++)
        {
            if (s[i][0] == 'A' || s[i][c - 1] == 'A')
            {
                Console.WriteLine("3");
                return;
            }
        }

        for (int j = 0; j < c; j++)
        {
            if (s[0][j] == 'A' || s[r - 1][j] == 'A')
            {
                Console.WriteLine("3");
                return;
            }
        }

        Console.WriteLine("4");
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