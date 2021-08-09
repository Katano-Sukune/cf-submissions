using System;
using System.Collections.Generic;
using System.IO;

namespace Contest
{
    class Program
    {


        public void Solve()
        {
            var sc = new Scanner();
            int N = sc.NextInt();
            int[] A = sc.IntArray();
            int left = 0;
            int right = N - 1;

            int cnt = 0;
            List<char> ans = new List<char>();
            int last = int.MinValue;
            while (right >= left)
            {
                int r = A[right];
                int l = A[left];
                if (r < last)
                {
                    if (l < last)
                    {
                        break;
                    }
                    else
                    {
                        last = l;
                        left++;
                        ans.Add('L');
                    }
                }
                else
                {
                    if (l < last)
                    {
                        last = r;
                        right--;
                        ans.Add('R');
                    }
                    else
                    {
                        if (r < l)
                        {
                            last = r;
                            right--;
                            ans.Add('R');
                        }

                        else
                        {
                            last = l;
                            left++;
                            ans.Add('L');
                        }
                    }
                }

                cnt++;
            }
            Console.WriteLine(cnt);
            Console.WriteLine(new string(ans.ToArray()));
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