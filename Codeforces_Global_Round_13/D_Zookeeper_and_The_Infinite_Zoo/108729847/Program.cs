using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int U, V;

    void Q(Scanner sc)
    {
        U = sc.NextInt();
        V = sc.NextInt();

        if (U == V)
        {
            Console.WriteLine("YES");
            return;
        }

        if (U > V)
        {
            Console.WriteLine("NO");
            return;
        }

        var lsU = new List<int>();
        var lsV = new List<int>();
        for (int i = 30 - 1; i >= 0; i--)
        {
            int b = 1 << i;
            if ((U & b) != 0) lsU.Add(i);
            if ((V & b) != 0) lsV.Add(i);
        }

        var dp = new bool[lsU.Count + 1, lsV.Count + 1];
        dp[0, 0] = true;
        for (int i = 0; i < lsU.Count; i++)
        {
            for (int j = 0; j < lsV.Count; j++)
            {
                if (!dp[i, j]) continue;
                if (lsU[i] == lsV[j]) dp[i + 1, j + 1] = true;
                else if (lsV[j] > lsU[i])
                {
                    dp[i + 1, j + 1] = true;
                    dp[i + 1, j] = true;
                }
            }
        }

        Console.WriteLine(dp[lsU.Count, lsV.Count] ? "YES" : "NO");
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