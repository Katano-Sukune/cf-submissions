using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, M;
    int[][] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.IntArray();
        }

        int t = -1;
        for (int i = 0; i < N; i++)
        {
            // 2種類以外ある行
            var hs = new HashSet<int>();
            for (int j = 0; j < M; j++)
            {
                hs.Add(A[i][j]);
            }
            if (hs.Count >= 2)
            {
                t = i;
                break;
            }
        }
        var ans = new int[N];
        Array.Fill(ans, 1);
        int xor = 0;
        for (int i = 0; i < N; i++)
        {
            xor ^= A[i][0];
        }
        if (t == -1)
        {
            if (xor == 0)
            {
                Console.WriteLine("NIE");
                return;
            }

            Console.WriteLine("TAK");
            Console.WriteLine(string.Join(" ", ans));
            return;
        }

        if (xor == 0)
        {
            for (int j = 0; j < M; j++)
            {
                if (A[t][0] != A[t][j])
                {
                    ans[t] = j + 1;
                    break;
                }
            }
        }

        Console.WriteLine("TAK");
        Console.WriteLine(string.Join(" ", ans));
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
