using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, M;
    int[] T;
    string[] S;

    bool[,] F;

    int[] Memo;

    int Ans;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        T = new int[N];
        S = new string[N];
        var friends = new HashSet<string>(M, StringComparer.Ordinal);

        for (int i = 0; i < N; i++)
        {
            T[i] = sc.NextInt();
            if (T[i] == 2)
            {
                S[i] = sc.Next();
                friends.Add(S[i]);
            }
        }
        var ar = friends.ToArray();
        var map = new Dictionary<string, int>(M, StringComparer.Ordinal);
        for (int i = 0; i < ar.Length; i++)
        {
            map[ar[i]] = i;
        }

        // iとj両方は不可能
        F = new bool[M, M];
        var hs = new HashSet<string>(M, StringComparer.Ordinal);
        for (int i = 0; i < N; i++)
        {
            if (T[i] == 1)
            {
                hs.Clear();
            }
            else
            {
                if (hs.Contains(S[i])) continue;
                foreach (var t in hs)
                {
                    int ss = map[S[i]];
                    int tt = map[t];
                    F[ss, tt] = true;
                    F[tt, ss] = true;
                }
                hs.Add(S[i]);
            }
        }

        Ans = 0;
        Memo = new int[1 << (M - M / 2)];
        Array.Fill(Memo, -1);
        Go(0, new List<int>());

        Console.WriteLine(Ans);
    }

    void Go(int i, List<int> ls)
    {
        if (i >= M / 2)
        {
            int tmp = 0;
            for (int j = 0; j < M - (M / 2); j++)
            {
                bool f = true;
                foreach (var k in ls)
                {
                    if (F[k, j + M / 2])
                    {
                        f = false;
                        break;
                    }
                }
                if (f) tmp |= 1 << j;
            }
            Ans = Math.Max(Ans, ls.Count + Go2(tmp));
        }
        else
        {
            Go(i + 1, ls);
            bool f = true;
            foreach (int j in ls)
            {
                if (F[i, j])
                {
                    f = false;
                    break;
                }
            }
            if (f)
            {
                ls.Add(i);
                Go(i + 1, ls);
                ls.RemoveAt(ls.Count - 1);
            }
        }
    }

    int Go2(int n)
    {
        if (Memo[n] != -1) return Memo[n];
        bool f = true;
        int cnt = 0;
        for (int i = 0; f && i < M - M / 2; i++)
        {
            if ((n & (1 << i)) == 0) continue;
            cnt++;
            for (int j = i + 1; f && j < M - M / 2; j++)
            {
                if ((n & (1 << j)) == 0) continue;
                if (F[i + M / 2, j + M / 2]) f = false;
            }
        }
        if (f)
        {
            Memo[n] = cnt;
            return cnt;
        }
        else
        {
            for (int i = 0; i < M - M / 2; i++)
            {
                if ((n & (1 << i)) == 0) continue;
                Memo[n] = Math.Max(Memo[n], Go2(n ^ (1 << i)));
            }

            return Memo[n];
        }
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
