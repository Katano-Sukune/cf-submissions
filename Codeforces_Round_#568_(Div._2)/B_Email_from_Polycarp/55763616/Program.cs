using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Contest
{
    struct S
    {
        public char C;
        public int Cnt;

        public S(char c, int i)
        {
            C = c;
            Cnt = i;
        }
    }
    class Program
    {
        private Scanner sc;

        public void Solve()
        {
            sc = new Scanner();
            int n = sc.NextInt();
            var sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                string a = sc.Next();
                string b = sc.Next();
                List<S> aa = Calc(a);
                List<S> bb = Calc(b);
                if (aa.Count != bb.Count)
                {
                    sb.AppendLine("NO");
                    continue;
                }

                var f = false;
                for (int j = 0; j < aa.Count; j++)
                {
                    if (aa[j].C != bb[j].C || aa[j].Cnt > bb[j].Cnt)
                    {
                        sb.AppendLine("NO");
                        f = true;
                        break;
                    }
                }

                if (!f)
                {
                    sb.AppendLine("YES");
                }

            }
            Console.Write(sb.ToString());
        }

        private List<S> Calc(string s)
        {
            List<S> res = new List<S>();
            res.Add(new S(s[0], 0));
            foreach (char c in s)
            {
                if (res[res.Count - 1].C != c)
                {
                    res.Add(new S(c, 0));
                }

                res[res.Count - 1] = new S(c, res[res.Count - 1].Cnt + 1);

            }

            return res;
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