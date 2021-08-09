using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private int N, K;
    private char[][] S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = new char[N][];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next().ToCharArray();
        }

        // ここにつくまでに通るa以外 最小
        var dp = new int[N * N];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i == 0)
                {
                    if (j == 0)
                    {
                        dp[C(i, j)] = 0;
                    }
                    else
                    {
                        dp[C(i, j)] = dp[C(i, j - 1)] + (S[i][j - 1] == 'a' ? 0 : 1);
                    }
                }
                else
                {
                    if (j == 0)
                    {
                        dp[C(i, j)] = dp[C(i - 1, j)] + (S[i - 1][j] == 'a' ? 0 : 1);
                    }
                    else
                    {
                        dp[C(i, j)] = Math.Min(dp[C(i, j - 1)] + (S[i][j - 1] == 'a' ? 0 : 1),
                            dp[C(i - 1, j)] + (S[i - 1][j] == 'a' ? 0 : 1));
                    }
                }
            }
        }

        // 全部aにしてもKが余るか
        if (dp[C(N - 1, N - 1)] + (S[N - 1][N - 1] == 'a' ? 0 : 1) <= K)
        {
            Console.WriteLine(new string('a', N + N - 1));
            return;
        }

        // dp[i][j] が Kかつ一番遠いところからスタート

        // 一番遠いところの距離
        int max = 0;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (dp[C(i, j)] == K)
                {
                    max = Math.Max(max, i + j);
                    // c = (char) Math.Min(c, S[i][j]);
                }
            }
        }

        // 
        char c = char.MaxValue;
        for (int i = 0; i < N; i++)
        {
            int j = max - i;
            if (j < 0 || j >= N) continue;
            if (dp[C(i, j)] == K)
            {
                c = Min(c, S[i][j]);
            }
        }


        var ans = new char[N + N - 1];


        for (int i = 0; i < max; i++)
        {
            ans[i] = 'a';
        }

        var list = new List<int>();
        for (int i = 0; i < N; i++)
        {
            int j = max - i;
            if (j < 0 || j >= N) continue;
            if (dp[C(i, j)] == K && S[i][j] == c)
            {
                list.Add(C(i, j));
            }
        }

        for (int t = max; t < N + N - 1 - 1; t++)
        {
            ans[t] = c;

            // 次
            c = char.MaxValue;
            foreach (int p in list)
            {
                int y = p / N;
                int x = p % N;
                if (x + 1 < N)
                {
                    c = Min(c, S[y][x + 1]);
                }

                if (y + 1 < N)
                {
                    c = Min(c, S[y + 1][x]);
                }
            }

            var next = new HashSet<int>();
            foreach (int p in list)
            {
                int y = p / N;
                int x = p % N;
                if (x + 1 < N && S[y][x + 1] == c)
                {
                    next.Add(C(y, x + 1));
                }

                if (y + 1 < N && S[y + 1][x] == c)
                {
                    next.Add(C(y + 1, x));
                }
            }

            list = next.ToList();
        }

        ans[N + N - 1 - 1] = c;

        Console.WriteLine(new string(ans));
    }

    private char Min(char a, char b)
    {
        return a < b ? a : b;
    }

    public int C(int i, int j)
    {
        return i * N + j;
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

class Scanner
{
    public Scanner()
    {
        _pos = 0;
        _line = new string[0];
    }

    const char Separator = ' ';
    private int _pos;
    private string[] _line;

    #region スペース区切りで取得

    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = Console.ReadLine().Split(Separator);
            _pos = 0;
        }

        return _line[_pos++];
    }

    public int NextInt()
    {
        return int.Parse(Next());
    }

    public long NextLong()
    {
        return long.Parse(Next());
    }

    public double NextDouble()
    {
        return double.Parse(Next());
    }

    #endregion

    #region 型変換

    private int[] ToIntArray(string[] array)
    {
        var result = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = int.Parse(array[i]);
        }

        return result;
    }

    private long[] ToLongArray(string[] array)
    {
        var result = new long[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = long.Parse(array[i]);
        }

        return result;
    }

    private double[] ToDoubleArray(string[] array)
    {
        var result = new double[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = double.Parse(array[i]);
        }

        return result;
    }

    #endregion

    #region 配列取得

    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = Console.ReadLine().Split(Separator);

        _pos = _line.Length;
        return _line;
    }

    public int[] IntArray()
    {
        return ToIntArray(Array());
    }

    public long[] LongArray()
    {
        return ToLongArray(Array());
    }

    public double[] DoubleArray()
    {
        return ToDoubleArray(Array());
    }

    #endregion
}