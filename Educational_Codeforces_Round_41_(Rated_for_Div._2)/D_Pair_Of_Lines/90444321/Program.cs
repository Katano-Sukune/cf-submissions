using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    Vec[] P;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = new Vec[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = new Vec(sc.NextInt(), sc.NextInt());
        }

        /*
         * n個点
         * 
         * 2本の直線引く
         * すべての点がどちらかの直線上にあるか?
         */
        // 0と1が同じ p_0,p_1上に無い頂点で直線できるか? no
        // 0 2
        // 可能、上2つがfalseかつ1,2が同じ

        if (N <= 2)
        {
            Console.WriteLine("YES");
            return;
        }

        Console.WriteLine((C(0, 1) || C(0, 2) || C(1, 2)) ? "YES" : "NO");
    }

    bool C(int s, int t)
    {
        long dx = P[t].X - P[s].X;
        long dy = P[t].Y - P[s].Y;
        //　直線st上に無い点リスト
        var ls = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (i == s || i == t) continue;
            if (dx == 0)
            {
                if (P[i].X != P[s].X)
                {
                    ls.Add(i);
                }
            }
            else if (dy == 0)
            {
                if (P[i].Y != P[s].Y)
                {
                    ls.Add(i);
                }
            }
            else
            {
                long dx2 = P[i].X - P[s].X;
                long dy2 = P[i].Y - P[s].Y;

                // dy2/dx2 = dy/dx
                // dy2dx = dydx2
                if (dy2 * dx != dy * dx2)
                {
                    ls.Add(i);
                }
            }
        }

        if (ls.Count <= 2) return true;

        long dx3 = P[ls[0]].X - P[ls[1]].X;
        long dy3 = P[ls[0]].Y - P[ls[1]].Y;
        for (int i = 2; i < ls.Count; i++)
        {
            if (dx3 == 0)
            {
                if (P[ls[i]].X != P[ls[0]].X)
                {
                    return false;
                }
            }
            else if (dy3 == 0)
            {
                if (P[ls[i]].Y != P[ls[0]].Y)
                {
                    return false;
                }
            }
            else
            {
                long dx4 = P[ls[i]].X - P[ls[0]].X;
                long dy4 = P[ls[i]].Y - P[ls[0]].Y;

                if (dy4 * dx3 != dy3 * dx4)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct Vec
{
    public long X, Y;
    public Vec(long x, long y)
    {
        X = x;
        Y = y;
    }
}

namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
