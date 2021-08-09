using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N, K;
    private int[] H;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        H = sc.IntArray();

        /*
         * iの高さh_i
         *
         * H以上ならHになるように取る
         * 取った個数がK以下なら良い
         *
         * すべて同じ高さにするために最小の良い回数
         */
        Array.Sort(H, (l, r) => r.CompareTo(l));
        // 全部minに揃える
        int min = H[N - 1];

        var t = new long[N + 1];
        for (int i = 0; i < N; i++)
        {
            t[i + 1] = t[i] + H[i];
        }

        // 取り除いた個数
        long s = 0;
        int hMax = H[0];

        int ans = 0;
        while (hMax > min)
        {
            int ok = hMax;
            int ng = min - 1;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                // midに合わせる
                int ok2 = -1;
                int ng2 = N;
                while (ng2 - ok2 > 1)
                {
                    int mid2 = (ok2 + ng2) / 2;
                    if (H[mid2] >= mid) ok2 = mid2;
                    else ng2 = mid2;
                }

                // 合計 t[ng2]
                // はみ出してるぶん ng2 * mid
                // 
                if (t[ng2] - (long)ng2 * mid - s <= K)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }

            ans++;
            hMax = ok;

            int ok3 = -1;
            int ng3 = N;
            while (ng3 - ok3 > 1)
            {
                int mid3 = (ok3 + ng3) / 2;
                if (H[mid3] >= ok) ok3 = mid3;
                else ng3 = mid3;
            }

            s = t[ng3] - (long)ng3 * ok;
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