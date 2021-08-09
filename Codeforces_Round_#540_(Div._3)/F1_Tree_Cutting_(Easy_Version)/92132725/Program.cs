using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    int N;
    int[] A;
    List<int>[] E;


    int Red, Blue;

    int Ans;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int v = sc.NextInt() - 1;
            int u = sc.NextInt() - 1;
            E[v].Add(u);
            E[u].Add(v);
        }

        /*
         * n頂点　木
         * 
         * 辺を消す
         * 
         * 辺を消したら赤の連結成分　と青の連結成分に分かれる辺の本数
         */

        Red = A.Count(col => col == 1);
        Blue = A.Count(col => col == 2);
        Ans = 0;

        Go(0, -1);

        Console.WriteLine(Ans);
    }

    (int r, int b) Go(int cur, int par)
    {
        int cntR = A[cur] == 1 ? 1 : 0;
        int cntB = A[cur] == 2 ? 1 : 0;
        foreach (int to in E[cur])
        {
            if (to == par) continue;
            (int tR, int tB) = Go(to, cur);

            if ((tR == 0 && tB == Blue) || (tR == Red && tB == 0)) Ans++;
            cntR += tR;
            cntB += tB;
        }

        return (cntR, cntB);
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
