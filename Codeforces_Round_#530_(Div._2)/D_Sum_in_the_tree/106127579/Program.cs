using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N;
    int[] P, S;

    List<int>[] C;


    bool Flag;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = new int[N];
        for (int i = 1; i < N; i++)
        {
            P[i] = sc.NextInt() - 1;
        }
        S = sc.IntArray();

        /*
         * n頂点木
         * 1根
         * 
         * 
         * 頂点iから根までの総和 S_i
         * iの深さ h_i
         * h_iが偶数の頂点のS_i消した
         * 
         * 頂点総和が最小になるように
         */

        C = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            C[i] = new List<int>();
        }

        for (int i = 1; i < N; i++)
        {
            C[P[i]].Add(i);
        }

        Go(0, 1);
        Console.WriteLine(Go2(0));
    }

    long Go2(int v)
    {
        long ans = v == 0 ? S[0] : S[v] - S[P[v]];
        if (ans < 0) return -1;
        foreach (var to in C[v])
        {
            long tmp = Go2(to);
            if (tmp == -1) return -1;
            ans += tmp;
        }
        return ans;
    }

    void Go(int v, int h)
    {
        if (h % 2 == 0)
        {
            int min = int.MaxValue;
            foreach (var to in C[v])
            {
                min = Math.Min(min, S[to]);
                Go(to, h + 1);
            }
            if (min == int.MaxValue) S[v] = S[P[v]];
            else S[v] = min;
        }
        else
        {
            foreach (var to in C[v])
            {
                Go(to, h + 1);
            }
        }

    }

    // public static void Main(string[] args) => new Program().Solve();
    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
