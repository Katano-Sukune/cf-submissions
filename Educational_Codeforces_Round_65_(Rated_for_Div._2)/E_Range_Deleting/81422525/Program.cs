using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, X;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.NextInt();
        A = sc.IntArray();

        // lを固定
        // l未満が昇順
        // l未満右端 < j超左端
        // j超が昇順
        // なjを探す

        // [j,X] は条件を満たす

        // iの左、右端 left[i] = -1なら存在しない
        int[] left = new int[X + 1];
        int[] right = new int[X + 1];
        {
            for (int i = 0; i <= X; i++)
            {
                left[i] = -1;
            }

            for (int i = 0; i < N; i++)
            {
                right[A[i]] = i;
                left[A[N - i - 1]] = N - i - 1;
            }
        }
        // i未満が昇順か?
        bool[] flagL = new bool[X + 2];

        // i未満の右端
        int[] ll = new int[X + 2];
        {
            ll[1] = -1;
            flagL[1] = true;
            int tmp = -1;
            for (int i = 1; i <= X; i++)
            {
                if (left[i] == -1)
                {
                    flagL[i + 1] = true;
                    ll[i + 1] = ll[i];
                }
                else if (tmp < left[i])
                {
                    flagL[i + 1] = true;
                    tmp = right[i];
                    ll[i + 1] = right[i];
                }
                else
                {
                    break;
                }
            }
        }
        // i超が昇順か?
        bool[] flagR = new bool[X + 1];
        int[] rr = new int[X + 1];
        {
            rr[X] = N;
            flagR[X] = true;
            int tmp = N;
            for (int i = X; i >= 1; i--)
            {
                if (left[i] == -1)
                {
                    flagR[i - 1] = true;
                    rr[i - 1] = rr[i];
                }
                else if (tmp > right[i])
                {
                    flagR[i - 1] = true;
                    tmp = left[i];
                    rr[i - 1] = left[i];
                }
                else
                {
                    break;
                }
            }
        }
        long ans = 0;
        for (int l = 1; l <= X; l++)
        {
            if (!flagL[l]) break;
            int ng = l - 1;
            int ok = X;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (flagR[mid] && ll[l] < rr[mid])
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }

            // [ok,X]
            // Console.WriteLine($"{l} {ok}");
            ans += X - ok + 1;
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
