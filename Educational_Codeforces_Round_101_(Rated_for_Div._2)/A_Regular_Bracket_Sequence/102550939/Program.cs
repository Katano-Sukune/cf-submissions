using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        string s = sc.Next();
        int n = s.Length;

        int q = 0;
        int cnt = 0;
        foreach (var c in s)
        {
            if (c == '(') cnt++;
            else if (c == ')') cnt--;
            else if (c == '?') q++;
        }

        // o + c = q
        // c - o = cnt

        int c2 = q + cnt;
        if (c2 % 2 != 0 || c2 < 0)
        {
            Console.WriteLine("NO");
            return;
        }



        int close = c2 / 2;
        int open = q - close;

        if (open < 0)
        {
            Console.WriteLine("NO");
            return;
        }

        int cnt2 = 0;
        foreach (var c in s)
        {
            if (c == '(') cnt2++;
            else if (c == ')') cnt2--;
            else if (c == '?')
            {
                if (open > 0)
                {
                    cnt2++;
                    open--;
                }
                else
                {
                    cnt2--;
                }
            }

            if (cnt2 < 0)
            {
                Console.WriteLine("NO");
                return;
            }
        }

        Console.WriteLine("YES");
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
