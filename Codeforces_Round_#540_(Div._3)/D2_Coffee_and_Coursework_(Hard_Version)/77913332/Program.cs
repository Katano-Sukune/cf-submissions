using System;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();


        /*
         * n個カップ
         * i個目にはa[i]カフェイン
         * 
         * i日にk個 (a_1,a_2...a_k)を飲む
         * 
         * 1杯目 a_1
         * 2    a_2 -1
         * n    a_n - (n-1)
         * 
         * mを終わらせるために何日必要か?
         */

        long sum = 0;
        foreach (int l in A)
        {
            sum += l;
        }

        // n日で終わらせる
        // 
        if (sum < M)
        {
            Console.WriteLine("-1");
            return;
        }

        Array.Sort(A, (l, r) => r.CompareTo(l));
        int ng = 0;
        int ok = N;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            // mid日で終わらせられるか?
            if (C(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(ok);
    }

    bool C(int d)
    {
        // dを分散

        long s = 0;
        for (int i = 0; i < N; i++)
        {
            long l = i / d;
            s += Math.Max(0, A[i] - i / d);
        }

        return s >= M;
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
