using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    string S, T;
    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        T = sc.Next();

        // 端消す 

        // 中消す

        // [i,N)はTの後ろ何文字と一致するか?
        int[] back = new int[S.Length + 1];
        for (int i = S.Length - 1; i >= 0; i--)
        {
            back[i] = back[i + 1];
            if (back[i + 1] < T.Length && S[i] == T[T.Length - 1 - back[i + 1]]) back[i]++;
        }
        int ans = int.MinValue;
        // Tの前何文字と一致するか?

        {
            // back[i] = T 最大
            int ok = 0;
            int ng = S.Length + 1;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (back[mid] >= T.Length)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            ans = ok;
        }
        int cnt = 0;

        for (int i = 0; i < S.Length; i++)
        {
            if (cnt < T.Length && S[i] == T[cnt])
            {
                cnt++;
                // back[i] = T-cnt i最大
                int ok = 0;
                int ng = S.Length + 1;
                while (ng - ok > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (back[mid] >= T.Length - cnt)
                    {
                        ok = mid;
                    }
                    else
                    {
                        ng = mid;
                    }
                }
                ans = Math.Max(ans, S.Length - (i + 1) - (S.Length - ok));
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
