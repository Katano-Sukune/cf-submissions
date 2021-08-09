using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace Contest
{
    class Program
    {
        public void Solve()
        {

            var sc = new Scanner();
            int N = sc.NextInt();
            int[] A = sc.IntArray();
            int ans = 0;
            string inf = "Infinite";
            for (int i = 0; i < N - 1; i++)
            {
                switch (A[i])
                {
                    case 1:
                        switch (A[i + 1])
                        {
                            case 1:
                                Console.WriteLine(inf);
                                return;
                            case 2:
                                ans += 3;
                                if (i > 0 && A[i - 1] == 3)
                                {
                                    ans--;
                                }
                                break;
                            case 3:
                                ans += 4;
                                break;
                        }
                        break;
                    case 2:
                        switch (A[i + 1])
                        {
                            case 1:
                                ans += 3;
                                break;
                            case 2:
                                Console.WriteLine(inf);
                                return;
                            case 3:
                                Console.WriteLine(inf);
                                return;
                        }
                        break;
                    case 3:
                        switch (A[i + 1])
                        {
                            case 1:
                                ans += 4;
                                break;
                            case 2:
                                Console.WriteLine(inf);
                                return;
                            case 3:
                                Console.WriteLine(inf);
                                return;
                        }
                        break;
                }
            }
            Console.WriteLine("Finite");
            Console.WriteLine(ans);
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