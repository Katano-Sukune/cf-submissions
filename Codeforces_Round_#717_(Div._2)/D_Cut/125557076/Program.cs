using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{
    int N, Q;
    int[] A;

    const int MaxA = 100000;
    List<int>[] P;
    int[] T;
    List<int>[] Ls;
    public void Solve()
    {
        // 素因数
        P = new List<int>[MaxA + 1];
        for (int i = 1; i <= MaxA; i++)
        {
            P[i] = new List<int>();
            int cp = i;
            for (int p = 2; p * p <= cp; p++)
            {
                if (cp % p == 0)
                {
                    P[i].Add(p);
                    while (cp % p == 0) cp /= p;
                }
            }
            if (cp != 1) P[i].Add(cp);
        }

        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        A = sc.IntArray();

        // iからどれだけ伸ばせるか?
        T = new int[N];
        int left = 0;
        int right = 0;
        int[] ar = new int[MaxA + 1];
        int cnt2 = 0;

        while (right < N)
        {
            foreach (int d in P[A[right]])
            {
                if (ar[d]++ == 1) cnt2++;
            }
            if (cnt2 > 0)
            {
                while (cnt2 > 0)
                {
                    T[left] = right;
                    foreach (int d in P[A[left]])
                    {
                        if (--ar[d] == 1) cnt2--;
                    }
                    left++;
                }
            }
            right++;
        }
        for (; left < N; left++) T[left] = N;

        // Console.WriteLine(string.Join(" ", T));

        // iの2^j個後
        Ls = new List<int>[N + 1];
        for (int i = N - 1; i >= 0; i--)
        {
            Ls[i] = new List<int>();
            Ls[i].Add(T[i]);
            for (int j = 1; ; j++)
            {
                int t1 = Ls[i][j - 1];
                if (t1 >= N) break;
                if (Ls[t1].Count < j) break;
                Ls[i].Add(Ls[t1][j - 1]);
            }
        }

        // for (int i = 0; i < N; i++)
        // {
        //     Console.WriteLine(string.Join(" ", Ls[i]));
        // }


#if !DEBUG
        System.Console.SetOut(new System.IO.StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        for (int i = 0; i < Q; i++)
        {
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();
            int cnt = 0;
            for (int j = Ls[l].Count - 1; j >= 0; j--)
            {
                if (j >= Ls[l].Count) continue;
                if (Ls[l][j] < r)
                {
                    l = Ls[l][j];
                    cnt += 1 << j;
                }
            }
            Console.WriteLine(cnt + 1);
        }

        System.Console.Out.Flush();

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
