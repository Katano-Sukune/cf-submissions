using System;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace Contest
{
    class Program
    {
        private Scanner sc;
        public void Solve()
        {
            sc = new Scanner();
            int q = sc.NextInt();

            var sb = new StringBuilder();
            for (int i = 0; i < q; i++)
            {
                sb.AppendLine(Query().ToString());
            }
            Console.Write(sb.ToString());
        }

        private int Query()
        {
            int n = sc.NextInt();
            int k = sc.NextInt();
            int min = -1;
            int max = int.MaxValue;
            int[] a = sc.IntArray();
            for (int i = 0; i < n; i++)
            {
                int nMin = a[i] - k;
                int nMax = a[i] + k;

                if (nMin > max || min > nMax)
                {
                    return -1;
                }

                min = Math.Max(min, nMin);
                max = Math.Min(max, nMax);
            }

            return max;
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
    private readonly char _separator;
    private readonly StreamReader _stream;
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