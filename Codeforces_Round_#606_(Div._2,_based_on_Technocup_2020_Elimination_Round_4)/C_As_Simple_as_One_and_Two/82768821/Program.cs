using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    int[] CharToInt;
    public void Solve()
    {

        CharToInt = new int[256];
        for (char c = 'a'; c <= 'z'; c++)
        {
            CharToInt[c] = 5;
        }

        CharToInt['o'] = 0;
        CharToInt['n'] = 1;
        CharToInt['e'] = 2;
        CharToInt['t'] = 3;
        CharToInt['w'] = 4;

        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.Next()));
        }
        Console.Write(sb);
    }

    string Q(string s)
    {
        int n = s.Length;
        // 0 o
        // 1 n
        // 2 e
        // 3 t
        // 4 w
        // 5 others

        var dp = new int[n + 1, 6, 6];
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 6; k++)
                {
                    dp[i, j, k] = int.MaxValue;
                }
            }
        }
        dp[0, 5, 5] = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (dp[i, j, k] == int.MaxValue) continue;
                    // 消す
                    dp[i + 1, j, k] = Math.Min(dp[i + 1, j, k], dp[i, j, k] + 1);

                    // 消さない
                    bool one = (s[i] == 'e' && j == 0 && k == 1);
                    bool two = (s[i] == 'o' && j == 3 && k == 4);
                    if (one || two) continue;
                    dp[i + 1, k, CharToInt[s[i]]] = Math.Min(dp[i + 1, k, CharToInt[s[i]]], dp[i, j, k]);
                }
            }
        }

        int min = int.MaxValue;
        int ii = -1;
        int jj = -1;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (dp[n, i, j] < min)
                {
                    min = dp[n, i, j];
                    ii = i;
                    jj = j;
                }
            }
        }

        List<int> ans = new List<int>();
        for (int i = n - 1; i >= 0; i--)
        {
            if (CharToInt[s[i]] == jj)
            {
                // 消してない
                for (int j = 0; j < 6; j++)
                {
                    // dp[i+1,ii,jj] = dp[i,j,ii] + 
                    bool one = j == 0 && ii == 1 && jj == 2;
                    bool two = j == 3 && ii == 4 && jj == 0;
                    if (one || two) continue;
                    if (dp[i + 1, ii, jj] == dp[i, j, ii])
                    {
                        jj = ii;
                        ii = j;
                        break;
                    }
                }

            }
            else
            {
                // 消した
                ans.Add(i + 1);
            }
        }

        ans.Reverse();
        return $"{ans.Count}\n{string.Join(" ", ans)}";
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
