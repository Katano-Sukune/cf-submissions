using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            sb.AppendLine(Query(sc.NextInt(), sc.NextInt(), sc.Next()));
        }
        Console.Write(sb);
    }

    string Query(int n, int k, string s)
    {
        // s[0] = r,g,bにしたとき変更する数
        int[] cnt = new int[3];
        int ans = int.MaxValue;
        for (int i = 0; i < k - 1; i++)
        {
            int c = -1;
            switch (s[i])
            {
                case 'R':
                    c = 0;
                    break;
                case 'G':
                    c = 2;
                    break;
                case 'B':
                    c = 1;
                    break;
            }
            for (int j = 0; j < 3; j++)
            {
                if (j == (i + c) % 3) continue;
                cnt[j]++;
            }
        }
        // Console.WriteLine(string.Join(" ", cnt));
        for (int i = k - 1; i < n; i++)
        {
            int c = -1;
            switch (s[i])
            {
                case 'R':
                    c = 0;
                    break;
                case 'G':
                    c = 2;
                    break;
                case 'B':
                    c = 1;
                    break;
            }
            for (int j = 0; j < 3; j++)
            {
                if (j == (i + c) % 3) continue;
                cnt[j]++;
            }
            // Console.WriteLine(string.Join(" " ,cnt));
            ans = Math.Min(ans, cnt.Min());
            switch (s[i - k + 1])
            {
                case 'R':
                    c = 0;
                    break;
                case 'G':
                    c = 2;
                    break;
                case 'B':
                    c = 1;
                    break;
            }
            for (int j = 0; j < 3; j++)
            {
                if (j == (i - k + 1 + c) % 3) continue;
                cnt[j]--;
            }
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
