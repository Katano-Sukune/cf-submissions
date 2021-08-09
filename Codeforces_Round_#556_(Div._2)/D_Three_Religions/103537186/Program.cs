using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, Q;
    string S;
#if DEBUG
    const int Max = 3;
#else
    const int Max = 250;
#endif
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();

        S = sc.Next();

        // 3つ文字列

        // + i c
        // iの末尾にc追加

        // - i
        // iの末尾削除

        // 3つの文字列を重ならないようにSの部分列におけるか?

        // i,j,k文字目までつくる
        // Sの何文字目までで作れるか?

        // i以降最初にあるjの位置
        int[][] next = new int[N + 1][];
        next[N] = new int[26];
        Array.Fill(next[N], N + 1);
        for (int i = N - 1; i >= 0; i--)
        {
            next[i] = new int[26];

            for (int j = 0; j < 26; j++)
            {
                next[i][j] = next[i + 1][j];
            }
            next[i][S[i] - 'a'] = i + 1;

        }

        var dp = new int[Max + 1, Max + 1, Max + 1];
        for (int i = 0; i <= Max; i++)
        {
            for (int j = 0; j <= Max; j++)
            {
                for (int k = 0; k <= Max; k++)
                {
                    dp[i, j, k] = N + 1;
                }
            }
        }

        dp[0, 0, 0] = 0;

        int cntI = 0;
        int cntJ = 0;
        int cntK = 0;

        List<char> I = new List<char>();
        List<char> J = new List<char>();
        List<char> K = new List<char>();

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int t = 0; t < Q; t++)
        {
            char op = sc.NextChar();
            if (op == '+')
            {
                int ii = sc.NextInt();
                char c = sc.NextChar();
                if (ii == 1)
                {
                    cntI++;
                    I.Add(c);
                    for (int j = 0; j <= cntJ; j++)
                    {
                        for (int k = 0; k <= cntK; k++)
                        {
                            if (dp[cntI - 1, j, k] <= N)
                            {
                                dp[cntI, j, k] = Math.Min(dp[cntI, j, k], next[dp[cntI - 1, j, k]][c - 'a']);
                            }
                            if (j > 0 && dp[cntI, j - 1, k] <= N)
                            {
                                dp[cntI, j, k] = Math.Min(dp[cntI, j, k], next[dp[cntI, j - 1, k]][J[j - 1] - 'a']);
                            }
                            if (k > 0 && dp[cntI, j, k - 1] <= N)
                            {
                                dp[cntI, j, k] = Math.Min(dp[cntI, j, k], next[dp[cntI, j, k - 1]][K[k - 1] - 'a']);
                            }
                        }
                    }
                }
                else if (ii == 2)
                {
                    cntJ++;
                    J.Add(c);
                    for (int i = 0; i <= cntI; i++)
                    {
                        for (int k = 0; k <= cntK; k++)
                        {
                            if (dp[i, cntJ - 1, k] <= N)
                                dp[i, cntJ, k] = Math.Min(dp[i, cntJ, k], next[dp[i, cntJ - 1, k]][c - 'a']);
                            if (i > 0 && dp[i - 1, cntJ, k] <= N)
                            {
                                dp[i, cntJ, k] = Math.Min(dp[i, cntJ, k], next[dp[i - 1, cntJ, k]][I[i - 1] - 'a']);
                            }
                            if (k > 0 && dp[i, cntJ, k - 1] <= N)
                            {
                                dp[i, cntJ, k] = Math.Min(dp[i, cntJ, k], next[dp[i, cntJ, k - 1]][K[k - 1] - 'a']);
                            }
                        }
                    }
                }
                else if (ii == 3)
                {
                    cntK++;
                    K.Add(c);
                    for (int i = 0; i <= cntI; i++)
                    {
                        for (int j = 0; j <= cntJ; j++)
                        {
                            if (dp[i, j, cntK - 1] <= N)
                                dp[i, j, cntK] = Math.Min(dp[i, j, cntK], next[dp[i, j, cntK - 1]][c - 'a']);
                            if (i > 0 && dp[i - 1, j, cntK] <= N)
                            {
                                dp[i, j, cntK] = Math.Min(dp[i, j, cntK], next[dp[i - 1, j, cntK]][I[i - 1] - 'a']);
                            }
                            if (j > 0 && dp[i, j - 1, cntK] <= N)
                            {
                                dp[i, j, cntK] = Math.Min(dp[i, j, cntK], next[dp[i, j - 1, cntK]][J[j - 1] - 'a']);
                            }
                        }
                    }
                }
            }
            else if (op == '-')
            {
                int ii = sc.NextInt();
                if (ii == 1)
                {
                    cntI--;
                    I.RemoveAt(cntI);
                    for (int j = 0; j <= cntJ; j++)
                    {
                        for (int k = 0; k <= cntK; k++)
                        {
                            dp[cntI + 1, j, k] = N + 1;
                        }
                    }
                }
                else if (ii == 2)
                {
                    cntJ--;
                    J.RemoveAt(cntJ);
                    for (int i = 0; i <= cntI; i++)
                    {
                        for (int k = 0; k <= cntK; k++)
                        {
                            dp[i, cntJ + 1, k] = N + 1;
                        }
                    }
                }
                else if (ii == 3)
                {
                    cntK--;
                    K.RemoveAt(cntK);
                    for (int i = 0; i <= cntI; i++)
                    {
                        for (int j = 0; j <= cntJ; j++)
                        {
                            dp[i, j, cntK + 1] = N + 1;
                        }
                    }
                }
            }

            Console.WriteLine(dp[cntI, cntJ, cntK] <= N ? "YES" : "NO");
        }

        Console.Out.Flush();

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
