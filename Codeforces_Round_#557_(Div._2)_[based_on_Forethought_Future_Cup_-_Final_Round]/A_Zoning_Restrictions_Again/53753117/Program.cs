using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace Contest
{
    class Program
    {
        private int N, H, M;
        private int[] L, R, X;
        public void Solve()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            H = sc.NextInt();
            M = sc.NextInt();
            L = new int[M];
            R = new int[M];
            X = new int[M];
            for (int i = 0; i < M; i++)
            {
                L[i] = sc.NextInt() - 1;
                R[i] = sc.NextInt();
                X[i] = sc.NextInt();
            }

            var ans = new int[N];
            for (int i = 0; i < N; i++)
            {
                ans [i]= H;
            }

            for (int i = 0; i < M; i++)
            {
                for (int j = L[i]; j < R[i]; j++)
                {
                    ans[j] = Math.Min(ans[j], X[i]);
                }
            }

            int a = 0;
            for (int i = 0; i < N; i++)
            {
                a += ans[i] * ans[i];
            }
            Console.WriteLine(a);
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