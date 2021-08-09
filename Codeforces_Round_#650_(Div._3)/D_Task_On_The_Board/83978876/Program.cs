using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});

        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    private string S;
    private int M;
    private int[] B;

    private int[] Cnt;

    private bool[] F;
    private char[] Ans;

    void Q(Scanner sc)
    {
        S = sc.Next();
        N = S.Length;
        M = sc.NextInt();
        B = sc.IntArray();

        // sが与えられる
        // sの部分文字列 t

        // t_i < t_j なる i,j の |i-j|の総和 B_i

        // tを出力

        // 0のところ 最大
        // 複数0がある 同じ文字かつ最大

        // これがなくなったら b

        Cnt = new int[26];
        for (int i = 0; i < N; i++)
        {
            Cnt[S[i] - 'a']++;
        }

        F = new bool[M];
        Ans = new char[M];
        Search('z');
    }

    bool Search(char c)
    {
        List<int> z = new List<int>();
        for (int i = 0; i < M; i++)
        {
            if (F[i]) continue;
            if (B[i] == 0)
            {
                z.Add(i);
            }
        }

        if (z.Count == 0)
        {
            Console.WriteLine(new string(Ans));
            return true;
        }

        foreach (int i in z)
        {
            F[i] = true;
        }

        foreach (int i in z)
        {
            for (int j = 0; j < M; j++)
            {
                if (F[j]) continue;
                B[j] -= Math.Abs(j - i);
            }
        }


        for (char d = c; d >= 'a'; d--)
        {
            if (Cnt[d - 'a'] >= z.Count)
            {
                foreach (int i in z)
                {
                    Ans[i] = d;
                }

                if (Search((char) (d - 1))) return true;
            }
        }

        foreach (int i in z)
        {
            for (int j = 0; j < M; j++)
            {
                if (F[j]) continue;
                B[j] += Math.Abs(j - i);
            }
        }

        foreach (int i in z)
        {
            F[i] = false;
        }

        return false;
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