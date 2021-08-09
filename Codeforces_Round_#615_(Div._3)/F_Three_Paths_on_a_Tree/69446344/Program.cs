using System;
using System.Collections;
using System.Collections.Generic;

public class Program
{
    private int N;
    private List<int>[] Edges;

    private int A, B, C;

    // Aからの距離
    private int[] Dist;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Edges = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edges[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            Edges[a].Add(b);
            Edges[b].Add(a);
        }

        /*
         * 直径と一つ最大になるように点を選べばいい
         */
        long ans = 0;

        #region A探す

        {
            var zeroDist = new int[N];
            for (int i = 0; i < N; i++)
            {
                zeroDist[i] = -1;
            }

            var stack = new Stack<int>();
            stack.Push(0);
            zeroDist[0] = 0;
            while (stack.Count > 0)
            {
                int p = stack.Pop();
                foreach (int i in Edges[p])
                {
                    if (zeroDist[i] == -1)
                    {
                        zeroDist[i] = zeroDist[p] + 1;
                        stack.Push(i);
                    }
                }
            }

            A = 0;
            int max = 0;

            for (int i = 0; i < N; i++)
            {
                if (max < zeroDist[i])
                {
                    max = zeroDist[i];
                    A = i;
                }
            }
        }

        #endregion

        #region B探す

        {
            Dist = new int[N];
            for (int i = 0; i < N; i++)
            {
                Dist[i] = -1;
            }

            var s = new Stack<int>();
            Dist[A] = 0;
            s.Push(A);

            while (s.Count > 0)
            {
                var p = s.Pop();
                foreach (var i in Edges[p])
                {
                    if (Dist[i] == -1)
                    {
                        Dist[i] = Dist[p] + 1;
                        s.Push(i);
                    }
                }
            }

            int max = 0;
            B = A;
            for (int i = 0; i < N; i++)
            {
                if (max < Dist[i])
                {
                    max = Dist[i];
                    B = i;
                }
            }

            ans += max;
        }

        #endregion

        #region C探す

        {
            // {A,B}上のnodeは0
            var d = new int[N];
            for (int i = 0; i < N; i++)
            {
                d[i] = -1;
            }

            var stack = new Stack<int>();
            int p = B;
            while (p != A)
            {
                d[p] = 0;
                stack.Push(p);
                foreach (var i in Edges[p])
                {
                    if (Dist[i] == Dist[p] - 1)
                    {
                        p = i;
                        break;
                    }
                }
            }

            d[A] = 0;
            stack.Push(A);

            while (stack.Count > 0)
            {
                var tmp = stack.Pop();
                foreach (var i in Edges[tmp])
                {
                    if (d[i] == -1)
                    {
                        d[i] = d[tmp] + 1;
                        stack.Push(i);
                    }
                }
            }

            int max = -1;
            C = -1;
            for (int i = 0; i < N; i++)
            {
                if (i == A || i == B) continue;
                if (max < d[i])
                {
                    max = d[i];
                    C = i;
                }
            }

            ans += max;
        }

        #endregion

        Console.WriteLine(ans);
        Console.WriteLine($"{A + 1} {B + 1} {C + 1}");
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

class Scanner
{
    public Scanner()
    {
        _pos = 0;
        _line = new string[0];
    }

    const char Separator = ' ';
    private int _pos;
    private string[] _line;

    #region スペース区切りで取得

    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = Console.ReadLine().Split(Separator);
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

    #region 型変換

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

    #region 配列取得

    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = Console.ReadLine().Split(Separator);

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