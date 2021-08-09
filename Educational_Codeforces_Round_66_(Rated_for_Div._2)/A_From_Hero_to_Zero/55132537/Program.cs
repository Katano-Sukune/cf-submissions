using System;
using System.IO;
using System.Text;

namespace Contest
{
    class Program
    {
        public void Solve()
        {
            var sc = new Scanner();

            int T = sc.NextInt();
            var sb = new StringBuilder();
            for (int i = 0; i < T; i++)
            {
                sb.AppendLine(Calc(sc.NextLong(), sc.NextLong()).ToString());
            }
            Console.Write(sb.ToString());

        }

        private long Calc(long n, long k)
        {
            long ans = 0;
            while (n > 0)
            {
                if (n % k == 0)
                {
                    ans++;
                    n /= k;
                }
                else
                {
                    ans += n % k;
                    n -= n % k;
                }
            }

            return ans;
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