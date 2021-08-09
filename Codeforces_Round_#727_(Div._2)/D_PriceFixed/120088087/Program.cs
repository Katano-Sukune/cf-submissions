using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    long[] A, B;
    int[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        A = new long[N];
        B = new long[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextLong();
            B[i] = sc.NextLong();
        }
        S = new int[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = i;
        }

        Array.Sort(S, (l, r) => B[r].CompareTo(B[l]));

        // 各製品 2
        // どれでもB_i個買ったら iの価格が1になる

        // iはA_i個必要
        // コスト最小

        long sum = 0;
        foreach (long i in A)
        {
            sum += i;
        }

        long ok = sum;
        long ng = -1;
        while (ok - ng > 1)
        {
            long mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(2 * ok + (sum - ok));
    }

    bool F(long k)
    {
        long sum = 0;
        long[] cpA = new long[N];
        Array.Copy(A, cpA, N);
        foreach (int i in S)
        {
            long t = Math.Min(k, cpA[i]);
            cpA[i] -= t;
            k -= t;
            sum += t;
        }

        for (int q = N - 1; q >= 0; q--)
        {
            int i = S[q];
            if (cpA[i] <= 0) break;
            if (B[i] > sum) return false;
            sum += cpA[i];
        }

        return true;
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
