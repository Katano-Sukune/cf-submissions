using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    const long Inf = long.MaxValue / 2;
    const int MaxDigit = 10;
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int size = 1;
        while (size < n) size <<= 1;
        int m = sc.NextInt();
        var a = sc.IntArray();
        var st = new SegmentTree[MaxDigit];
        for (int i = 0; i < MaxDigit; i++)
        {
            st[i] = new SegmentTree(size);
        }

        for (int i = 0; i < n; i++)
        {
            int tmp = a[i];
            for (int j = 0; j < MaxDigit; j++)
            {
                if (tmp % 10 != 0) st[j].Update(i, a[i]);
                tmp /= 10;
            }
        }
        var p = new long[MaxDigit];
        p[0] = 2;
        for (int i = 1; i < MaxDigit; i++)
        {
            p[i] = p[i - 1] * 10;
        }
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

        for (int q = 0; q < m; q++)
        {
            int t = sc.NextInt();
            if (t == 1)
            {
                int i = sc.NextInt() - 1;
                int x = sc.NextInt();
                int tmp = x;
                for (int j = 0; j < MaxDigit; j++)
                {
                    st[j].Update(i, tmp % 10 == 0 ? Inf : x);
                    tmp /= 10;
                }
            }
            else
            {
                int l = sc.NextInt() - 1;
                int r = sc.NextInt();
                long ans = Inf;
                for (int j = 0; j < MaxDigit && ans >= p[j]; j++)
                {
                    ans = Math.Min(ans, st[j].Query(l, r));
                }
                Console.WriteLine(ans == Inf ? -1 : ans);
            }
        }
        Console.Out.Flush();
    }

    public static void Main(string[] args) => new Program().Solve();
}

class SegmentTree
{
    const long Inf = long.MaxValue / 2;
    int Size;
    private readonly Pair[] Node;
    public SegmentTree(int s)
    {
        Size = s;
        Node = new Pair[Size * 2];
        for (int i = 1; i < Size * 2; i++)
        {
            Node[i] = new Pair(Inf, Inf);
        }
    }

    public Pair Q(int left, int right, int k, int l, int r)
    {
        if (left <= l && r <= right) return Node[k];
        if (r <= left || right <= l) return new Pair(Inf, Inf);
        return Merge(Q(left, right, k * 2, l, (l + r) / 2), Q(left, right, k * 2 + 1, (l + r) / 2, r));
    }
    public long Query(int left, int right)
    {
        var p = Q(left, right, 1, 0, Size);
        return p.First + p.Second;
    }

    public void Update(int i, long x)
    {
        i += Size;
        Node[i] = new Pair(x, Inf);
        while (i > 1)
        {
            i >>= 1;
            Node[i] = Merge(Node[i * 2], Node[i * 2 + 1]);
        }
    }

    Pair Merge(Pair l, Pair r)
    {
        if (l.First <= r.First)
        {
            return new Pair(l.First, Math.Min(l.Second, r.First));
        }
        else
        {
            return new Pair(r.First, Math.Min(l.First, r.Second));
        }
    }
    public struct Pair
    {
        // 一番小さい、二番目に小さい
        public long First, Second;
        public Pair(long f, long s)
        {
            First = f;
            Second = s;
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
