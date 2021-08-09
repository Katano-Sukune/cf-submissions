using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Contest
{
    class HashMap<K, V> : Dictionary<K, V>
    {
        new public V this[K i]
        {
            get
            {
                V v;
                return TryGetValue(i, out v) ? v : base[i] = default(V);
            }
            set { base[i] = value; }
        }
    }
    class Program
    {
        public void Solve()
        {
            var sc = new Scanner();
            int N = sc.NextInt();
            int M = sc.NextInt();
            int[] A, B;
            A = new int[M];
            B = new int[M];
            for (int i = 0; i < M; i++)
            {
                A[i] = sc.NextInt();
                B[i] = sc.NextInt();

            }
            List<int> ind = new List<int>();
            // A
            for (int i = 1; i < M; i++)
            {
                if (A[i] == A[0] || B[i] == A[0])
                {

                }
                else
                {
                    ind.Add(i);
                }
            }

            var map = new HashMap<int,int>();
            foreach (var i in ind)
            {
                map[A[i]]++;
                map[B[i]]++;
            }

            if (map.ContainsValue(ind.Count)||ind.Count == 0)
            {
                Console.WriteLine("YES");
                return;
            }
             ind = new List<int>();
            // B
            for (int i = 1; i < M; i++)
            {
                if (A[i] == B[0] || B[i] == B[0])
                {

                }
                else
                {
                    ind.Add(i);
                }
            }

            map = new HashMap<int, int>();
            foreach (var i in ind)
            {
                map[A[i]]++;
                map[B[i]]++;
            }

            if (map.ContainsValue(ind.Count) || ind.Count == 0)
            {
                Console.WriteLine("YES");
                return;
            }
            Console.WriteLine("NO");
        }

        static void Main() => new Program().Solve();
    }
}

class Scanner
{
    public Scanner()
    {
        _stream = new StreamReader(Console.OpenStandardInput());
        _pos = 0;
        _line = new string[0];
        _separator = ' ';
    }
    private char _separator;
    private StreamReader _stream;
    private int _pos;
    private string[] _line;
    #region get a element
    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = _stream.ReadLine().Split(_separator);
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
    #region convert array
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
    #region get array
    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = _stream.ReadLine().Split(_separator);
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