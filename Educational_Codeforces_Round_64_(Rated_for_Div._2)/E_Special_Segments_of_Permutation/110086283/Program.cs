using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] P;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = sc.IntArray();
        int[] idx = new int[N + 1];
        for (int i = 0; i < N; i++)
        {
            idx[P[i]] = i;
        }

        List<int>[] lsIdx = new List<int>[N];
        HashSet<int>[] hsNums = new HashSet<int>[N];
        int[] leader = new int[N];
        for (int i = 0; i < N; i++)
        {
            lsIdx[i] = new List<int>() {i};
            hsNums[i] = new HashSet<int>() {P[i]};
            leader[i] = i;
        }

        long ans = 0;
        for (int max = 1; max <= N; max++)
        {
            int c = idx[max];

            bool prev = c - 1 >= 0 && P[c - 1] < max;
            bool next = c + 1 < N && P[c + 1] < max;

            if (prev && next)
            {
                int ll = leader[c - 1];
                int lr = leader[c + 1];
                if (lsIdx[ll].Count <= lsIdx[lr].Count)
                {
                    foreach (var pl in hsNums[ll])
                    {
                        if (hsNums[lr].Contains(max - pl)) ans++;
                    }

                    foreach (var pl in hsNums[ll])
                    {
                        hsNums[lr].Add(pl);
                    }

                    foreach (int i in lsIdx[ll])
                    {
                        leader[i] = lr;
                        lsIdx[lr].Add(i);
                    }

                    leader[c] = lr;
                    lsIdx[lr].Add(c);
                    hsNums[lr].Add(max);
                }
                else
                {
                    foreach (var pr in hsNums[lr])
                    {
                        if (hsNums[ll].Contains(max - pr)) ans++;
                    }

                    foreach (var pr in hsNums[lr])
                    {
                        hsNums[ll].Add(pr);
                    }

                    foreach (int i in lsIdx[lr])
                    {
                        leader[i] = ll;
                        lsIdx[ll].Add(i);
                    }

                    leader[c] = ll;
                    lsIdx[ll].Add(c);
                    hsNums[ll].Add(max);
                }
            }
            else if (next)
            {
                int lr = leader[c + 1];
                leader[c] = lr;
                lsIdx[lr].Add(c);
                hsNums[lr].Add(max);
            }
            else if (prev)
            {
                int ll = leader[c - 1];
                leader[c] = ll;
                lsIdx[ll].Add(c);
                hsNums[ll].Add(max);
            }
        }

        Console.WriteLine(ans);
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