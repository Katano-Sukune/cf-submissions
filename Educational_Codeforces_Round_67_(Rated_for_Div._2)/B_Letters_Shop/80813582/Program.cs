using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();
        int m = sc.NextInt();
        string[] t = new string[m];
        for (int i = 0; i < m; i++)
        {
            t[i] = sc.Next();
        }

        var sum = new int[26][];
        for (int i = 0; i < 26; i++)
        {
            sum[i] = new int[n + 1];
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                sum[j][i + 1] = sum[j][i];
            }
            sum[s[i] - 'a'][i + 1]++;
        }

        var sb = new StringBuilder();
        for (int i = 0; i < m; i++)
        {
            int[] cnt = new int[26];
            foreach (var c in t[i])
            {
                cnt[c - 'a']++;
            }
            int ans = 0;
            for (int j = 0; j < 26; j++)
            {
                int ok = n;
                int ng = -1;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (sum[j][mid] >= cnt[j]) ok = mid;
                    else ng = mid;
                }
                ans = Math.Max(ans, ok);
            }
            sb.AppendLine(ans.ToString());
        }
        Console.Write(sb);

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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
