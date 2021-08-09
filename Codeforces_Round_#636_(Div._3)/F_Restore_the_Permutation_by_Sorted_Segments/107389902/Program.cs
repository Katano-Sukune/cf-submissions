using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    private int[] K;
    private int[][] A;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        K = new int[N - 1];
        A = new int[N - 1][];
        for (int i = 0; i < N - 1; i++)
        {
            K[i] = sc.NextInt();
            A[i] = new int[K[i]];
            for (int j = 0; j < K[i]; j++)
            {
                A[i][j] = sc.NextInt();
            }
        }

        List<int>[] ls = new List<int>[N + 1];
        for (int i = 1; i <= N; i++)
        {
            ls[i] = new List<int>();
        }

        int[] cnt = new int[N + 1];
        for (int i = 0; i < N - 1; i++)
        {
            foreach (int j in A[i])
            {
                ls[j].Add(i);
                cnt[j]++;
            }
        }

        // 一番最後
        for (int l = 1; l <= N; l++)
        {
            if (cnt[l] != 1) continue;
            // 一番最初
            for (int f = 1; f <= N; f++)
            {
                if (l == f) continue;
                int[] ans = new int[N];
                int[] cp = new int[N + 1];
                Array.Copy(cnt, cp, N + 1);
                var hs = new HashSet<int>[N + 1];
                for (int i = 1; i <= N; i++) hs[i] = new HashSet<int>(ls[i]);

                int[] r = new int[N];

                int p = l;
                bool flag = true;
                for (int i = N - 1; flag && i >= 1; i--)
                {
                    ans[i] = p;
                    // pを持つやつ
                    int j = hs[p].ToArray()[0];
                    r[i] = j;
                    int np = -1;
                    foreach (int k in A[j])
                    {
                        cp[k]--;
                        hs[k].Remove(j);
                        if (k != f && cp[k] == 1) np = k;
                    }

                    if (i > 1 && np == -1) flag = false;
                    else p = np;
                }

                if (!flag) continue;
                ans[0] = f;
                // 条件に合ってるか?
                bool flag2 = true;
                for (int i = 1; flag2 && i < N; i++)
                {
                    var tmp = new int[K[r[i]]];
                    for (int j = 0; j < K[r[i]]; j++)
                    {
                        tmp[j] = ans[i - j];
                    }

                    Array.Sort(tmp);
                    for (int j = 0; j < K[r[i]] && flag2; j++)
                    {
                        flag2 &= tmp[j] == A[r[i]][j];
                    }
                }
                
                if(!flag2) continue;

                Console.WriteLine(string.Join(" ", ans));
                return;
            }
        }

        Console.WriteLine("-1");
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