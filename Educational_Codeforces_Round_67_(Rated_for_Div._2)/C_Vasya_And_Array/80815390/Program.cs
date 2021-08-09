using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Graph;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        var one = new List<Segment>();
        var zero = new List<Segment>();
        for (int i = 0; i < m; i++)
        {
            int t = sc.NextInt();
            var s = new Segment(sc.NextInt() - 1, sc.NextInt() - 1);
            if (t == 0) zero.Add(s);
            else one.Add(s);
        }

        // t_i = 1 [l,r]は広義単調増加

        // t_i = 0 [l,r]は広義単調増加ではない

        one.Sort((l, r) => r.R.CompareTo(l.R));

        var uf = new UnionFind(n);
        int index = n - 1;
        foreach (var s in one)
        {
            for (; index >= s.L; index--)
            {
                if (index + 1 <= s.R) uf.Unite(index, index + 1);
            }
        }

        foreach (var s in zero)
        {
            if(uf.Same(s.L,s.R))
            {
                Console.WriteLine("NO");
                return;
            }
        }

        var ans = new int[n];
        int tmp = 1;
        ans[n - 1] = 1;
        for (int i = n - 2; i >= 0; i--)
        {
            if (!uf.Same(i, i + 1)) tmp++;
            ans[i] = tmp;
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}



struct Segment
{
    public int L, R;
    public Segment(int l, int r)
    {
        L = l;
        R = r;
    }
}


namespace CompLib.Graph
{
    class UnionFind
    {
        private readonly int[] _parent, _size;

        public UnionFind(int size)
        {
            _parent = new int[size];
            _size = new int[size];
            for (int i = 0; i < size; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        /// <summary>
        /// iが含まれる木の根を調べる
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Find(int i) => _parent[i] == i ? i : Find(_parent[i]);

        /// <summary>
        /// x,yが同じグループに含まれるか?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Same(int x, int y) => Find(x) == Find(y);

        /// <summary>
        /// xとyを同じグループにする 元々同じグループならfalseを返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Unite(int x, int y)
        {
            x = Find(x);
            y = Find(y);
            if (x == y) return false;

            // データ構造をマージする一般的なテク
            if (_size[x] > _size[y])
            {
                _parent[y] = x;
                _size[x] += _size[y];
            }
            else
            {
                _parent[x] = y;
                _size[y] += _size[x];
            }

            return true;
        }

        /// <summary>
        /// iが含まれるグループのサイズ
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetSize(int i) => _size[Find(i)];
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
