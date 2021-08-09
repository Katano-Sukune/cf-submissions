using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    string C, S, T;
    public void Solve()
    {
        var sc = new Scanner();
        C = sc.Next();
        S = sc.Next();
        T = sc.Next();

        var dp = new int[C.Length + 1, S.Length, T.Length];
        for (int i = 0; i <= C.Length; i++)
        {
            for (int j = 0; j < S.Length; j++)
            {
                for (int k = 0; k < T.Length; k++)
                {
                    dp[i, j, k] = int.MinValue;
                }
            }
        }

        // iまで見る
        // S j文字一致
        // T k文字一致
        dp[0, 0, 0] = 0;
        for (int i = 0; i < C.Length; i++)
        {
            for (int j = 0; j < S.Length; j++)
            {
                for (int k = 0; k < T.Length; k++)
                {
                    int cur = dp[i, j, k];
                    if (cur == int.MinValue) continue;
                    if (C[i] == '*')
                    {
                        for (char c = 'a'; c <= 'z'; c++)
                        {
                            int nD = cur;
                            int nJ, nK;
                            if (Calc(ref S, j, c, out nJ)) nD++;
                            if (Calc(ref T, k, c, out nK)) nD--;
                            dp[i + 1, nJ, nK] = Math.Max(dp[i + 1, nJ, nK], nD);
                        }
                    }
                    else
                    {
                        int nD = cur;
                        int nJ, nK;
                        if (Calc(ref S, j, C[i], out nJ)) nD++;
                        if (Calc(ref T, k, C[i], out nK)) nD--;
                        dp[i + 1, nJ, nK] = Math.Max(dp[i + 1, nJ, nK], nD);
                    }
                }
            }
        }

        int ans = int.MinValue;
        for (int j = 0; j < S.Length; j++)
        {
            for (int k = 0; k < T.Length; k++)
            {
                ans = Math.Max(ans, dp[C.Length, j, k]);
            }
        }

        Console.WriteLine(ans);
    }

    // strの先頭i文字+c
    // strの先頭何文字一致してるか?
    bool Calc(ref string str, int i, char c, out int next)
    {
        bool f2 = (i + 1 == str.Length) && (str[i] == c);
        for (int l = Math.Min(str.Length - 1, i + 1); l >= 1; l--)
        {
            bool f = true;
            for (int j = 0; f && j < l - 1; j++)
            {
                f &= str[j] == str[j + i - l + 1];
            }
            if (f && str[l - 1] == c)
            {
                next = l;
                return f2;
            }
        }
        next = 0;
        return f2;
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
