using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N, K;
    private long[] T;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        T = new long[N + 1];
        A = new int[N + 1];
        B = new int[N + 1];
        for (int i = 0; i < N; i++)
        {
            T[i] = sc.NextInt();
            A[i] = sc.NextInt();
            B[i] = sc.NextInt();
        }

        T[N] = int.MaxValue;

        // n冊の本

        // 読むのにT_iかかる
        // アリスが好きならA_iは1
        // ボブ　　　　　　B_i

        // n冊から何冊か選ぶ
        // アリス,ボブはそのうちk冊以上好き
        // Tの総和最小

        List<int> alice = new List<int>();
        List<int> bob = new List<int>();
        List<int> both = new List<int>();

        for (int i = 0; i < N; i++)
        {
            if (A[i] == 1)
            {
                if (B[i] == 1)
                {
                    both.Add(i);
                }
                else
                {
                    alice.Add(i);
                }
            }
            else
            {
                if (B[i] == 1)
                {
                    bob.Add(i);
                }
            }
        }

        alice.Add(N);
        bob.Add(N);
        both.Add(N);
        
        alice.Sort((l, r) => T[l].CompareTo(T[r]));
        bob.Sort((l, r) => T[l].CompareTo(T[r]));
        both.Sort((l, r) => T[l].CompareTo(T[r]));

        if (alice.Count + both.Count -2 < K || bob.Count + both.Count -2 < K)
        {
            Console.WriteLine("-1");
            return;
        }
        
        int i1 = 0;
        int i2 = 0;
        long ans = 0;
        for (int i = 0; i < K; i++)
        {
            
            if (T[alice[i1]] + T[bob[i1]] < T[both[i2]])
            {
                ans += T[alice[i1]] + T[bob[i1]];
                i1++;
            }
            else
            {
                ans += T[both[i2]];
                i2++;
            }
        }

        Console.WriteLine(ans);
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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