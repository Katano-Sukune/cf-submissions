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

    void Q(Scanner sc)
    {
        string s = sc.Next();
        var ls = new List<int>[26];
        for (int i = 0; i < 26; i++)
        {
            ls[i] = new List<int>();
        }

        for (int i = 0; i < s.Length; i++)
        {
            ls[s[i] - 'a'].Add(i);
        }

        int n = s.Length;
        bool[][] f = new bool[n + 1][];
        f[n] = new bool[26];
        for (int i = n - 1; i >= 0; i--)
        {
            f[i] = new bool[26];
            Array.Copy(f[i + 1], f[i], 26);
            f[i][s[i] - 'a'] = true;
        }

        bool[] u = new bool[26];
        for (int i = 0; i < 26; i++)
        {
            u[i] = !f[0][i];
        }

        List<char> ans = new List<char>();
        int idx = 0;

        while (true)
        {
            // Console.WriteLine(idx);
            bool flag1 = false;
            for (int i = 26 - 1; i >= 0; i--)
            {
                if (u[i]) continue;
                int ng = -1;
                int ok = ls[i].Count;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (ls[i][mid] >= idx) ok = mid;
                    else ng = mid;
                }

                bool flag2 = true;
                for (int j = 0; j < 26 && flag2; j++)
                {
                    if (u[j]) continue;
                    flag2 &= f[idx][j] == f[ls[i][ok]][j];
                }

                if (flag2)
                {
                    ans.Add((char) ('a' + i));
                    idx = ls[i][ok] + 1;
                    u[i] = true;
                    flag1 = true;
                    break;
                }
            }

            if (!flag1) break;
        }

        Console.WriteLine(new string(ans.ToArray()));
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