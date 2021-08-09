using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long[] L, R;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        L = new long[N];
        R = new long[N];
        for (int i = 0; i < N; i++)
        {
            L[i] = sc.NextLong();
            R[i] = sc.NextLong();
        }

        // ひとつ削除
        // 残りのsegmentの交差　最大値

        // [0,i]の交点
        var front = new Segment[N];
        front[0] = new Segment(L[0], R[0]);
        for (int i = 1; i < N; i++)
        {
            front[i] = Segment.Intersect(front[i - 1], new Segment(L[i], R[i]));
        }

        // [i,N)の交点
        var back = new Segment[N];
        back[N - 1] = new Segment(L[N - 1], R[N - 1]);
        for (int i = N - 2; i >= 0; i--)
        {
            back[i] = Segment.Intersect(back[i + 1], new Segment(L[i], R[i]));
        }

        long ans = 0;

        // iを抜く
        for (int i = 0; i < N; i++)
        {
            Segment s;
            if (i == 0) s = back[1];
            else if (i == N - 1) s = front[N - 2];
            else s = Segment.Intersect(front[i - 1], back[i + 1]);
            ans = Math.Max(ans, s.Len);
        }
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}

// 半開区間
struct Segment
{
    public long L, R;
    public Segment(long l, long r)
    {
        L = l;
        R = r;
    }


    public static Segment Intersect(Segment l, Segment r)
    {
        if (l.R > r.R) return Intersect(r, l);
        if (l.R <= r.L) return new Segment(0, 0);
        if (r.L <= l.L) return l;
        return new Segment(r.L, l.R);
    }

    public long Len => R - L;
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
