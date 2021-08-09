using System;
using System.Linq;
using System.Text;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();


        // 隣り合う要素　選ぶ
        // 片方を diff足す or 引く
        // 操作 1足す
        // 2引く

        var sb = new StringBuilder();
        int cnt = 0;

        var map = new HashMap<int, int>();

        foreach (int i in A)
        {
            map[i]++;

        }
        int v = -1;
        int max = 0;
        foreach (var p in map)
        {
            if (p.Value > max)
            {
                max = p.Value;
                v = p.Key;
            }
        }

        int l = -1;

        for (int i = 0; i < N; i++)
        {
            if (A[i] == v)
            {
                l = i;
                for (int j = i - 1; j >= 0 && A[j] != v; j--)
                {
                    cnt++;
                    int t = A[j] < v ? 1 : 2;
                    sb.AppendLine($"{t} {j + 1} {j + 2}");
                }
            }
        }

        for (int j = l + 1; j < N; j++)
        {
            cnt++;
            int t = A[j] < v ? 1 : 2;
            sb.AppendLine($"{t} {j + 1} {j}");
        }

        Console.WriteLine(cnt);
        Console.Write(sb);
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections
{
    using System.Collections.Generic;
    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue o;
                return TryGetValue(key, out o) ? o : default(TValue);
            }
            set { base[key] = value; }
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
