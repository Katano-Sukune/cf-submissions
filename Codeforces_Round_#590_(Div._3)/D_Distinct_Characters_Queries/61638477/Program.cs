using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithm;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        var s = sc.Next().ToCharArray();
        int n = s.Length;
        var sum = new SegTree[26];

        for (char c = 'a'; c <= 'z'; c++)
        {
            sum[c - 'a'] = new SegTree(n);
        }

        for (int i = 0; i < n; i++)
        {
            char c = s[i];
            sum[c - 'a'][i] = 1;
        }

        int q = sc.NextInt();

        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            int type = sc.NextInt();
            if (type == 1)
            {
                int index = sc.NextInt() - 1;
                char c = sc.Next()[0];
                sum[s[index] - 'a'][index] = 0;
                sum[c - 'a'][index] = 1;
                s[index] = c;
            }
            else
            {
                int l = sc.NextInt()-1;
                int r = sc.NextInt();
                int cnt = 0;
                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (sum[c - 'a'].Query(l, r) > 0) cnt++;
                }

                sb.AppendLine(cnt.ToString());
            }
        }

        Console.Write(sb.ToString());
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

namespace Algorithm
{
    using N = Int32;

    class SegTree
    {
        private readonly N[] Array;
        private readonly int len;

        public SegTree(int size)
        {
            len = 1;
            while (len < size)
            {
                len *= 2;
            }

            Array = new N[len * 2];
        }

        public void Update(N item, int index)
        {
            index += len;
            Array[index] = item;
            while (index > 1)
            {
                index /= 2;
                Array[index] = Array[index * 2] + Array[index * 2 + 1];
            }
        }

        private N Q(int left, int right, int k, int l, int r)
        {
            if (left <= l && r <= right)
            {
                return Array[k];
            }

            if (r <= left || right <= l)
            {
                return 0;
            }

            return Q(left, right, k * 2, l, (l + r) / 2) + Q(left, right, k * 2 + 1, (l + r) / 2, r);
        }

        public N Query(int left, int right)
        {
            return Q(left, right, 1, 0, len);
        }

        public N this[int i]
        {
            get { return Array[i + len]; }
            set { Update(value, i); }
        }
    }
}

class Scanner
{
    public Scanner()
    {
        _pos = 0;
        _line = new string[0];
    }

    const char Separator = ' ';
    private int _pos;
    private string[] _line;

    #region スペース区切りで取得

    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = Console.ReadLine().Split(Separator);
            _pos = 0;
        }

        return _line[_pos++];
    }

    public int NextInt()
    {
        return int.Parse(Next());
    }

    public long NextLong()
    {
        return long.Parse(Next());
    }

    public double NextDouble()
    {
        return double.Parse(Next());
    }

    #endregion

    #region 型変換

    private int[] ToIntArray(string[] array)
    {
        var result = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = int.Parse(array[i]);
        }

        return result;
    }

    private long[] ToLongArray(string[] array)
    {
        var result = new long[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = long.Parse(array[i]);
        }

        return result;
    }

    private double[] ToDoubleArray(string[] array)
    {
        var result = new double[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = double.Parse(array[i]);
        }

        return result;
    }

    #endregion

    #region 配列取得

    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = Console.ReadLine().Split(Separator);

        _pos = _line.Length;
        return _line;
    }

    public int[] IntArray()
    {
        return ToIntArray(Array());
    }

    public long[] LongArray()
    {
        return ToLongArray(Array());
    }

    public double[] DoubleArray()
    {
        return ToDoubleArray(Array());
    }

    #endregion
}