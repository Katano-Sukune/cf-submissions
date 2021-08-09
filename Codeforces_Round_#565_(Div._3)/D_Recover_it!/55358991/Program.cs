using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        private Scanner sc;

        private bool[] IsnotPrime;
        private Dictionary<int, int> Map;
        private int maxP = 2750131;
        public void Solve()
        {
            sc = new Scanner();
            int N = sc.NextInt();
            int[] B = sc.IntArray();

            IsnotPrime = new bool[maxP + 1];
            IsnotPrime[0] = true;
            IsnotPrime[1] = true;
            for (int i = 2; i <= maxP; i++)
            {
                if (!IsnotPrime[i])
                {
                    for (int j = i + i; j <= maxP; j += i)
                    {
                        IsnotPrime[j] = true;
                    }
                }
            }
            Map = new Dictionary<int, int>();
            int cnt = 0;
            for (int i = 2; i <= maxP; i++)
            {
                if (IsPrime(i))
                {
                    Map[i] = ++cnt;
                }
            }

            var hs = new HashMap<int, int>();
            foreach (var i in B)
            {
                hs[i]++;
            }
            List<int> ans = new List<int>();

            Array.Sort(B, (a, b) => -a.CompareTo(b));
            foreach (var i in B)
            {
                if (hs[i] > 0)
                {
                    int c;
                    if (IsPrime(i))
                    {
                        c = Map[i];
                        ans.Add(c);
                        if (hs[c] > 0)
                        {
                            hs[c]--;
                            hs[i]--;
                        }
                    }
                    else
                    {
                        c = C(i);
                        ans.Add(i);
                        if (hs[c] > 0)
                        {
                            hs[c]--;
                            hs[i]--;
                        }
                    }

                }
            }
            Console.WriteLine(string.Join(" ", ans));
        }

        private int C(int i)
        {
            for (int j = 2; j * j <= i; j++)
            {
                if (i % j == 0)
                {
                    return i / j;
                }
            }

            return -1;
        }

        private bool IsPrime(int i)
        {
            return !IsnotPrime[i];
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