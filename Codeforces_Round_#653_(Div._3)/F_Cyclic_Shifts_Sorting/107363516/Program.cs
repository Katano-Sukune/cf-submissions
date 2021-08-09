using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    private int[] A;

    private List<int> L;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        A = sc.IntArray();

        // a_i, a_{i+1}, a_{i+2}を選ぶ
        // a_{i+2}, a_i, a_{i+1}にする

        // N^2回以下でソートする
        L = new List<int>();
        if (!F(0))
        {
            Console.WriteLine("-1");
        }
    }

    bool F(int p)
    {
        if (p >= N - 2)
        {
            if (A[N - 2] <= A[N - 1])
            {
                Console.WriteLine(L.Count);
                Console.WriteLine(string.Join(" ", L));
                return true;
            }
            else
            {
                return false;
            }
        }

        int len = L.Count;
        // 最小を探す
        int min = int.MaxValue;
        for (int i = p; i < N; i++) min = Math.Min(min, A[i]);

        for (int i = p; i < N; i++)
        {
            if (A[i] != min) continue;
            int tmp = i;
            while (p < tmp)
            {
                if (tmp - p >= 2)
                {
                    L.Add(tmp - 2 + 1);
                    (A[tmp - 2], A[tmp - 1], A[tmp]) = (A[tmp], A[tmp - 2], A[tmp - 1]);
                    tmp -= 2;
                }
                else
                {
                    L.Add(tmp - 1 + 1);
                    (A[tmp - 1], A[tmp], A[tmp + 1]) = (A[tmp + 1], A[tmp - 1], A[tmp]);
                    L.Add(tmp - 1 + 1);
                    (A[tmp - 1], A[tmp], A[tmp + 1]) = (A[tmp + 1], A[tmp - 1], A[tmp]);
                    tmp -= 1;
                }
            }

            if (F(p + 1)) return true;
            // Console.WriteLine(string.Join(" ", A));
            while (L.Count > len)
            {
                int idx = L.Count - 1;
                int q = L[idx] - 1;
                (A[q], A[q + 1], A[q + 2]) = (A[q + 1], A[q + 2], A[q]);
                L.RemoveAt(idx);
            }
        }

        return false;
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