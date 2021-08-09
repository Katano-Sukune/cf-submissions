using System;
using System.IO;
using System.Linq;

namespace Contest
{
    class Program
    {


        public void Solve()
        {
            var sc = new Scanner();
            int N = sc.NextInt();
            char[] S = sc.Next().ToArray();
            int[] A = sc.IntArray();

            int start = 0;
            for (start = 0; start < N; start++)
            {
                if (A[ToInt(S[start]) - 1] > ToInt(S[start]))
                {
                    break;
                }
            }

            int end = start;
            for (end = start; end < N; end++)
            {

                if (A[ToInt(S[end]) - 1] < ToInt(S[end]))
                {
                    break;
                }
            }

            for (int i = start; i < end; i++)
            {
                S[i] = (char)(A[ToInt(S[i]) - 1] + '0');
            }
            Console.WriteLine(new string(S));
        }

        private int ToInt(char c)
        {
            return c - '0';
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