using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] C, A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        C = sc.IntArray();
        A = sc.IntArray();
        for (int i = 0; i < N; i++)
        {
            A[i]--;
        }
        /*
         * 部屋iにトラップ置く
         * コストC_i
         * 
         * ネズミ
         * iにいる時、A_iに移動
         * 
         * どこにネズミいても捕まえられるようにトラップ置く
         * 
         * コスト最小
         */

        /*
         * ネズミの挙動
         * ループまで移動 -a -> ループ -b
         * 
         * b最小に置く
         * 
         */
        long ans = 0;
        int[] f = new int[N];
        int t = 1;
        for (int i = 0; i < N; i++)
        {
            if (f[i] != 0) continue;
            int cur = i;
            while (f[cur] == 0)
            {
                f[cur] = t;
                cur = A[cur];
            }
            if (f[cur] == t)
            {
                int min = C[cur];
                int cur2 = A[cur];
                while (cur2 != cur)
                {
                    min = Math.Min(min, C[cur2]);
                    cur2 = A[cur2];
                }

                ans += min;
            }
            t++;
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
