using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();


        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.Next()));
        }
        Console.Write(sb);
    }

    string Q(int n, int k, string s)
    {
        // sの各文字をk個に分配
        // 辞書順最大を最小化
        int[] cnt = new int[26];
        foreach (var c in s)
        {
            cnt[c - 'a']++;
        }
        var ar = s.ToCharArray();
        Array.Sort(ar);
        if (ar[0] != ar[k - 1])
        {
            return ar[k - 1].ToString();
        }

        // 文字 1種
        int unique = 0;
        int min = -1;
        for (int i = 0; i < 26; i++)
        {
            if (cnt[i] > 0)
            {
                unique++;
            }
        }
        if (unique == 1)
        {
            return s.Substring(0, (n + k - 1) / k);
        }
        var sb = new StringBuilder();
        if (unique == 2 && ar[k - 1] == ar[0] && ar[k] != ar[0])
        {
            // 最小がちょうどK個なら2番目　分配
            sb.Append(ar[0]);
            sb.Append(ar[k], (n - 1) / k);
        }
        else
        {
            bool flag = false;
            for (int i = 0; i < 26; i++)
            {
                char ch = (char)('a' + i);
                if (cnt[i] == 0) continue;
                if (flag)
                {
                    sb.Append(ch, cnt[i]);
                    continue;
                }
                sb.Append(ch, cnt[i] - k + 1);
                flag = true;
            }
        }
        return sb.ToString();
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

