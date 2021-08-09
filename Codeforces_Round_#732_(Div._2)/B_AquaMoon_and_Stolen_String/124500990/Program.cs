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
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }

        string[] t = new string[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            t[i] = sc.Next();
        }

        char[] ans = new char[m];
        for (int i = 0; i < m; i++)
        {
            var ar = new char[256];
            for (int j = 0; j < n; j++)
            {
                ar[s[j][i]]++;
            }
            for (int j = 0; j < n - 1; j++)
            {
                ar[t[j][i]]--;
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                if (ar[c] > 0) ans[i] = c;
            }
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