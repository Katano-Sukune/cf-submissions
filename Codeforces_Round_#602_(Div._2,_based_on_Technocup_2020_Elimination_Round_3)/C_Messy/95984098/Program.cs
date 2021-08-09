using System;
using System.Collections.Generic;
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
        int n = sc.NextInt();
        int k = sc.NextInt();
        string s = sc.Next();

        /*
         * k-1個 () のこり (((...)))...
         *
         * 
         */

        char[] t = new char[n];
        for (int i = 0; i < k - 1; i++)
        {
            t[i * 2] = '(';
            t[i * 2 + 1] = ')';
        }

        for (int i = 0; i < n / 2 - (k - 1); i++)
        {
            t[2 * (k - 1) + i] = '(';
            t[2 * (k - 1) + n / 2 - (k - 1) + i] = ')';
        }

        List<string> ans = new List<string>();
        char[] cur = s.ToCharArray();
        for (int i = 0; i < n; i++)
        {
            if (cur[i] == t[i]) continue;
            for (int j = i + 1; j < n; j++)
            {
                if (cur[j] == t[i])
                {
                    ans.Add($"{i + 1} {j + 1}");
                    for (int l = 0;; l++)
                    {
                        if (i + l >= j - l) break;
                        (cur[i + l], cur[j - l]) = (cur[j - l], cur[i + l]);
                    }

                    break;
                }
            }
        }

        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join("\n", ans));
        // Console.WriteLine(new string(cur));
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