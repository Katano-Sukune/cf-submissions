
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            var sb = new StringBuilder();
            for (int i = 0; i < N; i++)
            {
                string S = sc.Next();
                var q = new Query(S);
                sb.AppendLine(q.ans);
            }
            Console.Write(sb.ToString());
        }

        class Query
        {
            private string S;
            public string ans;
            public char[] c;
            private int[] cnt = new int[255];
            private bool[]b = new bool[255];
            private int cc = 0;
            public Query(string s)
            {
                S = s;

                foreach (char c1 in s)
                {
                    cnt[c1]++;
                    b[c1] = true;
                }

                cc = b.Count(bb => bb);
                c = new char[cc];

                if (Q(0))
                {
                    ans = "";
                    foreach (var VARIABLE in c)
                    {
                        ans += new string(VARIABLE,cnt[VARIABLE]);
                    }
                }
                else
                {
                    ans = "No answer";
                }
            }

            public bool Q(int index)
            {
                if (index == cc)
                {
                    return true;
                }
                for (char i = 'a'; i <= 'z'; i++)
                {
                    if (!b[i]) continue;
                    if (index > 0)
                    {
                        if (Math.Abs(c[index - 1] - i) == 1)
                        {
                            continue;
                        }
                    }
                    c[index] = i;
                    b[i] = false;
                    if (Q(index + 1))
                    {
                        return true;
                    }

                    b[i] = true;
                }

                return false;
            }
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