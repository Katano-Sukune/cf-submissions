using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private decimal[] A;

    private const int D = 100000;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new decimal[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextDecimal();
        }

        long[] tA = new long[N];
        for (int i = 0; i < N; i++)
        {
            tA[i] = (long) (A[i] * D);
        }

        long sum = 0;
        var idx = new List<int>();
        for (int i = 0; i < N; i++)
        {
            // 整数
            if (tA[i] % D == 0)
            {
                sum += tA[i];
            }
            else
            {
                // 大きい方に丸める
                if (tA[i] > 0)
                {
                    long d = tA[i] / D;
                    sum += (d + 1) * D;
                    tA[i] = (d + 1) * D;
                }
                else
                {
                    long d = tA[i] / D;
                    sum += d * D;
                    tA[i] = d * D;
                }

                idx.Add(i);
            }
        }

        long cnt = sum / D;
        for (int i = 0; i < cnt; i++)
        {
            tA[idx[i]] -= D;
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(tA[i] / D);
        }

        Console.Out.Flush();
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