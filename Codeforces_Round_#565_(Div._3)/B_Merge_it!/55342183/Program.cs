using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace Contest
{
    class Program
    {
        private Scanner sc;
        public void Solve()
        {
            sc = new Scanner();
            int Q = sc.NextInt();
            var sb = new StringBuilder();
            for (int i = 0; i < Q; i++)
            {
                long N = sc.NextLong();
                int[] A = sc.IntArray();
                var cnt = new int[3];

                foreach (var i1 in A)
                {
                    cnt[i1 % 3]++;
                }
                // Console.WriteLine(string.Join(" ", cnt));
                int ans = 0;
                ans += cnt[0];
                int p = Math.Min(cnt[1], cnt[2]);
                ans += p;
                //Console.WriteLine(ans);
                cnt[1] -= p;
                cnt[2] -= p;
                ans += cnt[1] / 3;
                ans += cnt[2] / 3;
                //Console.WriteLine(string.Join(" ",cnt));
                sb.AppendLine(ans.ToString());
            }
            Console.Write(sb.ToString());

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