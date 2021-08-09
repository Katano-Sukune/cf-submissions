using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.String;

public class Program
{
    private int N, M;
    private string[] S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }


        /*
         * 部分行列
         * 部分行列の各行の文字を入れ替えて行、列が全て回文にできるならOK
         *
         * 個数
         *
         * 
         */

        long ans = 0;
        for (int l = 0; l < M; l++)
        {
            int[][] cnt = new int[N][];
            for (int i = 0; i < N; i++)
            {
                cnt[i] = new int[26];
            }

            for (int r = l; r < M; r++)
            {
                for (int i = 0; i < N; i++)
                {
                    cnt[i][S[i][r] - 'a']++;
                }

                var tmp = new int[2 * N - 1][];
                int p = 0;
                for (int i = 0; i < N; i++)
                {
                    tmp[2 * i] = new int[26];
                    int cnt1 = 0;
                    for (int j = 0; j < 26; j++)
                    {
                        if (cnt[i][j] % 2 == 1) cnt1++;
                    }

                    if (cnt1 >= 2)
                    {
                        tmp[2 * i] = new[] {p++};
                    }
                    else
                    {
                        Array.Copy(cnt[i], tmp[2 * i], 26);
                    }
                }

                var mc = MyString.ManaChar(tmp, new CmpIntArray());
                // Console.WriteLine($"{l} {r}");
                // Console.WriteLine(string.Join(" ", mc));
                // for (int i = 0; i < N; i++)
                // {
                //     Console.WriteLine(string.Join("},{", cnt[i]));
                // }

                for (int i = 0; i < 2 * N - 1; i++)
                {
                    if (i % 2 == 0 && tmp[i].Length == 1) continue;
                    ans += (mc[i] - (i % 2) + 1) / 2;
                }
            }
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class CmpIntArray : EqualityComparer<int[]>
{
    public override bool Equals(int[] x, int[] y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;
        if (x.Length != y.Length) return false;
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i]) return false;
        }

        return true;
    }

    public override int GetHashCode(int[] obj)
    {
        const int mod = (int) 1e9 + 7;
        long hash = 0;
        foreach (var i in obj)
        {
            hash *= 256;
            hash += i;
            hash %= mod;
        }

        return (int) hash;
    }
}

namespace CompLib.String
{
    using System.Linq;
    using System.Collections.Generic;

    public static partial class MyString
    {
        public static int[] ManaChar(string s)
        {
            return ManaChar(s.ToArray(), EqualityComparer<char>.Default);
        }

        public static int[] ManaChar<T>(T[] ar)
        {
            return ManaChar(ar, EqualityComparer<T>.Default);
        }

        public static int[] ManaChar<T>(T[] ar, EqualityComparer<T> cmp)
        {
            int i = 0, j = 0;
            int[] r = new int[ar.Length];
            while (i < ar.Length)
            {
                while (i - j >= 0 && i + j < ar.Length && cmp.Equals(ar[i - j], ar[i + j])) ++j;
                r[i] = j;
                int k = 1;
                while (i - k >= 0 && k + r[i - k] < j)
                {
                    r[i + k] = r[i - k];
                    ++k;
                }

                i += k;
                j -= k;
            }

            return r;
        }
    }
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