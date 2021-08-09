using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    string[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        long[] cnt = new long[26];
        char c = S[0][0];
        int tmp = 0;
        foreach (char ch in S[0])
        {
            if (c != ch)
            {
                cnt[c - 'a'] = Math.Max(cnt[c - 'a'], tmp);
                tmp = 0;
                c = ch;
            }
            tmp++;
        }
        cnt[c - 'a'] = Math.Max(cnt[c - 'a'], tmp);

        for (int i = 1; i < N; i++)
        {
            // 全部同じ
            int[] cnt2 = new int[26];
            char c2 = S[i][0];
            int tmp2 = 0;
            foreach (var ch in S[i])
            {
                if (c2 != ch)
                {
                    cnt2[c2 - 'a'] = Math.Max(cnt2[c2 - 'a'], tmp2);
                    tmp2 = 0;
                    c2 = ch;
                }
                tmp2++;
            }
            cnt2[c2 - 'a'] = Math.Max(cnt2[c2 - 'a'], tmp2);

            int f = 0;
            for (int j = 0; j < S[i].Length; j++)
            {
                if (S[i][j] == S[i][0]) f++;
                else break;
            }

            int b = 0;
            for (int j = S[i].Length - 1; j >= 0; j--)
            {
                if (S[i][j] == S[i][^1]) b++;
                else break;
            }
            // 全部同じ
            if (cnt2[S[i][0] - 'a'] == S[i].Length)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (j == S[i][0] - 'a')
                    {
                        cnt[j] = cnt[j] * (S[i].Length + 1) + S[i].Length;
                    }
                    else
                    {
                        cnt[j] = cnt[j] > 0 ? 1 : 0;
                    }
                }
            }
            else if (S[i][0] == S[i][^1])
            {
                //先頭、末尾同じ
                for (int j = 0; j < 26; j++)
                {
                    if (j == S[i][0] - 'a')
                    {
                        if (cnt[j] > 0)
                        {
                            cnt[j] = Math.Max(cnt2[j], f + b + 1);
                        }
                        else
                        {
                            cnt[j] = cnt2[j];
                        }
                    }
                    else
                    {
                        cnt[j] = Math.Max(cnt2[j], cnt[j] > 0 ? 1 : 0);
                    }
                }
            }
            else
            {
                for (int j = 0; j < 26; j++)
                {
                    if (j == S[i][0] - 'a')
                    {
                        if (cnt[j] > 0)
                        {
                            cnt[j] = Math.Max(cnt2[j], f + 1);
                        }
                        else
                        {
                            cnt[j] = cnt2[j];
                        }
                    }
                    else if (j == S[i][^1] - 'a')
                    {
                        if (cnt[j] > 0)
                        {
                            cnt[j] = Math.Max(cnt2[j], b + 1);
                        }
                        else
                        {
                            cnt[j] = cnt2[j];
                        }
                    }
                    else
                    {
                        if (cnt[j] > 0)
                        {
                            cnt[j] = Math.Max(cnt2[j], 1);
                        }
                        else
                        {
                            cnt[j] = cnt2[j];
                        }
                    }
                }
            }
        }

        Console.WriteLine(cnt.Max());
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
