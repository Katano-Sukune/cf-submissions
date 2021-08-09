using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;

namespace Contest
{
    class Program
    {
        public void Solve()
        {
            var sc = new Scanner();
            int N = sc.NextInt();
            int M = sc.NextInt();
            var matrixA = new int[N][];
            var matrixB = new int[N][];
            for (int i = 0; i < N; i++)
            {
                matrixA[i] = sc.IntArray();
            }

            for (int i = 0; i < N; i++)
            {
                matrixB[i] = sc.IntArray();
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (matrixA[i][j] > matrixB[i][j])
                    {
                        int t = matrixA[i][j];
                        matrixA[i][j] = matrixB[i][j];
                        matrixB[i][j] = t;
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (i > 0)
                    {
                        if (matrixA[i - 1][j] >= matrixA[i][j])
                        {
                            Console.WriteLine("Impossible");
                            return;
                        }
                        if (matrixB[i - 1][j] >= matrixB[i][j])
                        {
                            Console.WriteLine("Impossible");
                            return;
                        }
                    }

                    if (j > 0)
                    {
                        if (matrixA[i][j - 1] >= matrixA[i][j])
                        {
                            Console.WriteLine("Impossible");
                            return;
                        }
                        if (matrixB[i][j - 1] >= matrixB[i][j])
                        {
                            Console.WriteLine("Impossible");
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("Possible");
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