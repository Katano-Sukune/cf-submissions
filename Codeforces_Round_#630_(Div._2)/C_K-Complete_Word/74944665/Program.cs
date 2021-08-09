using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    private int[] primes = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31};

    public void Solve()
    {
        var sc = new Scanner();
        int T = sc.NextInt();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < T; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextCharArray()));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, int k, char[] s)
    {
        int ans = 0;
        for (int i = 0; i < k / 2; i++)
        {
            // 前 i
            // うしろ k-i-1
            int sum = 0;
            int[] cnt = new int[26];
            for (int j = i; j < n; j += k)
            {
                cnt[s[j] - 'a']++;
                sum++;
            }

            for (int j = k - i - 1; j < n; j += k)
            {
                cnt[s[j] - 'a']++;
                sum++;
            }

            int max = cnt.Max();
            ans += sum - max;
        }

        if (k % 2 == 1)
        {
            int sum = 0;
            int[] cnt = new int[26];
            for (int j = k / 2; j < n; j += k)
            {
                cnt[s[j] - 'a']++;
                sum++;
            }

            int max = cnt.Max();
            ans += sum - max;
        }

        return ans.ToString();
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