using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{
    // 頂点
    const int Vertices = 8;
    const int Permutation = 6;
    public void Solve()
    {
        var sc = new Scanner();
        int[][] p = new int[Vertices][];
        for (int i = 0; i < Vertices; i++)
        {
            p[i] = sc.IntArray();
        }


        int[][] a = {
            new int[]{0,1,2},
            new int[]{0,2,1},
            new int[]{1,0,2},
            new int[]{1,2,0},
            new int[]{2,0,1},
            new int[]{2,1,0},
        };

        int t = 1;
        for (int i = 0; i < Vertices - 1; i++)
        {
            t *= Permutation;
        }

        for (int b = 0; b < t; b++)
        {
            var tmp = new int[Vertices][];
            var cp = b;
            for (int i = 0; i < Vertices - 1; i++)
            {
                tmp[i] = new int[3];
                int mod = cp % Permutation;
                for (int j = 0; j < 3; j++)
                {
                    tmp[i][j] = p[i][a[mod][j]];
                }

                cp /= Permutation;
            }

            tmp[Vertices - 1] = p[Vertices - 1];

            if (IsCube(tmp))
            {
                Console.WriteLine("YES");
                for (int i = 0; i < Vertices; i++)
                {
                    Console.WriteLine(string.Join(" ", tmp[i]));

                }
                return;
            }
        }
        Console.WriteLine("NO");
    }

    bool IsCube(int[][] p)
    {
        // 6^7=279936
        // 
        // 1点からの距離
        // 0,1,1,1,√2,√2,√2,√3

        // 距離1のやつ距離2乗
        long dist1 = -1;
        bool flag = true;
        for (int i = 0; flag && i < Vertices; i++)
        {
            var ls = new List<long>(Vertices - 1);
            for (int j = 0; j < Vertices; j++)
            {
                if (i == j) continue;
                ls.Add(Dist2(p[i], p[j]));
            }

            ls.Sort();

            if (i == 0)
            {
                dist1 = ls[0];
            }

            for (int j = 0; j < 3; j++)
            {
                flag &= dist1 == ls[j];
            }

            for (int j = 3; j < 6; j++)
            {
                flag &= dist1 * 2 == ls[j];
            }

            flag &= dist1 * 3 == ls[6];
        }

        return flag && dist1 != 0;
    }

    long Dist2(int[] a, int[] b)
    {
        long ret = 0;
        for (int i = 0; i < 3; i++) ret += (long)(a[i] - b[i]) * (a[i] - b[i]);
        return ret;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
