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
        int n = sc.NextInt();
        int[] s = sc.IntArray();
        int[] c = sc.IntArray();

        //　i < j < k かつ s_i < s_j < s_k
        // cの和最小

        var dp = new Dictionary<int, int>[4];
        for (int i = 0; i <= 3; i++)
        {
            dp[i] = new Dictionary<int, int>();
        }
        dp[0][int.MinValue] = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 2; j >= 0; j--)
            {
                foreach ((int key, int value) in dp[j])
                {
                    if (key >= s[i]) continue;
                    int cost = value + c[i];
                    int o;
                    if (!dp[j + 1].TryGetValue(s[i], out o))
                    {
                        o = int.MaxValue;
                    }

                    dp[j + 1][s[i]] = Math.Min(o, cost);
                }
            }
        }

        if (dp[3].Count == 0)
        {
            Console.WriteLine("-1");
            return;
        }
        
        int ans = int.MaxValue;
        foreach (var pair in dp[3])
        {
            ans = Math.Min(ans, pair.Value);
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