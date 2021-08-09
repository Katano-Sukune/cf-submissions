using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, Q;
    char[] S;
    const int MaxWordLen = 250;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        S = sc.NextCharArray();
        // q個クエリ
        // 1,2,3に + cを追加 - ラスト削除
        // sの各文字に重ならない部分文字列1,2,3が作れるか?
        int[] last = new int[26];
        for (int i = 0; i < 26; i++)
        {
            last[i] = N + 1;
        }
        int[,] next = new int[N, 26];
        for (int i = N - 1; i >= 0; i--)
        {

            last[S[i] - 'a'] = i + 1;
            for (int j = 0; j < 26; j++)
            {
                next[i, j] = last[j];
            }
        }

        // Sの何もじめまで 1,2,3のi文字目まで作る 
        int[,,] dp = new int[MaxWordLen + 1, MaxWordLen + 1, MaxWordLen + 1];

        for (int i = 0; i <= MaxWordLen; i++)
        {
            for (int j = 0; j <= MaxWordLen; j++)
            {
                for (int k = 0; k <= MaxWordLen; k++)
                {
                    dp[i, j, k] = int.MaxValue;
                }
            }
        }

        dp[0, 0, 0] = 0;

        List<char>[] word = new List<char>[3];
        for (int i = 0; i < 3; i++)
        {
            word[i] = new List<char>(MaxWordLen);
        }

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int w = 0; w < Q; w++)
        {
            char t = sc.NextChar();
            if (t == '+')
            {
                int idx = sc.NextInt() - 1;
                char c = sc.NextChar();
                int iMin = idx == 0 ? word[0].Count : 0;
                int iMax = idx == 0 ? word[0].Count + 1 : word[0].Count;

                int jMin = idx == 1 ? word[1].Count : 0;
                int jMax = idx == 1 ? word[1].Count + 1 : word[1].Count;

                int kMin = idx == 2 ? word[2].Count : 0;
                int kMax = idx == 2 ? word[2].Count + 1 : word[2].Count;

                word[idx].Add(c);
                for (int i = iMin; i <= iMax; i++)
                {
                    for (int j = jMin; j <= jMax; j++)
                    {
                        for (int k = kMin; k <= kMax; k++)
                        {
                            int cur = dp[i, j, k];
                            if (cur >= N) continue;
                            if (i + 1 <= iMax)
                            {
                                dp[i + 1, j, k] = Math.Min(dp[i + 1, j, k], next[cur, word[0][i] - 'a']);
                            }

                            if (j + 1 <= jMax)
                            {
                                dp[i, j + 1, k] = Math.Min(dp[i, j + 1, k], next[cur, word[1][j] - 'a']);
                            }

                            if (k + 1 <= kMax)
                            {
                                dp[i, j, k + 1] = Math.Min(dp[i, j, k + 1], next[cur, word[2][k] - 'a']);
                            }
                        }
                    }
                }

            }
            else
            {
                int idx = sc.NextInt() - 1;
                int iMin = idx == 0 ? word[0].Count : 0;
                int iMax = word[0].Count;

                int jMin = idx == 1 ? word[1].Count : 0;
                int jMax = word[1].Count;

                int kMin = idx == 2 ? word[2].Count : 0;
                int kMax = word[2].Count;

                word[idx].RemoveAt(word[idx].Count - 1);
                for (int i = iMin; i <= iMax; i++)
                {
                    for (int j = jMin; j <= jMax; j++)
                    {
                        for (int k = kMin; k <= kMax; k++)
                        {
                            dp[i, j, k] = int.MaxValue;
                        }
                    }
                }
            }

            // Console.WriteLine(dp[word[0].Count, word[1].Count, word[2].Count]);
            Console.WriteLine(dp[word[0].Count, word[1].Count, word[2].Count] <= N ? "YES" : "NO");
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
