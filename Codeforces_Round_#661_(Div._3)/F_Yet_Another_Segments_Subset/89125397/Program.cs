using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Map = new int[200001];
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }


    private int[] Map;

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        var l = new int[n];
        var r = new int[n];
        var ls = new List<int>();
        for (int i = 0; i < n; i++)
        {
            l[i] = sc.NextInt();
            r[i] = sc.NextInt();
            ls.Add(l[i]);
            ls.Add(r[i]);
        }

        ls.Sort();
        
        int k = 0;
        for (int i = 0; i < ls.Count; i++)
        {
            if (i == 0 || ls[i] != ls[i - 1])
            {
                Map[ls[i]] = k++;
            }
        }

        List<int>[] s = new List<int>[k];
        for (int i = 0; i < k; i++)
        {
            s[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            s[Map[l[i]]].Add(Map[r[i]]);
        }


        var f = new bool[k, k];
        for (int i = 0; i < n; i++)
        {
            f[Map[l[i]], Map[r[i]]] = true;
        }

        var dp = new int[k, k];
        for (int len = 1; len <= k; len++)
        {
            for (int left = 0; left + len - 1 < k; left++)
            {
                int right = left + len - 1;
                int result = left + 1 > right ? 0 : dp[left + 1, right];
                for (int i = 0; i < s[left].Count; i++)
                {
                    if (s[left][i] < right)
                        result = Math.Max(result,
                            (left > s[left][i] ? 0 : dp[left, s[left][i]]) +
                            (s[left][i] + 1 > right ? 0 : dp[s[left][i] + 1, right]));
                }

                if (f[left, right]) result++;
                dp[left, right] = result;
            }
        }

        Console.WriteLine(dp[0, k - 1]);
    }

    public static void Main() => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 26).Start();
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