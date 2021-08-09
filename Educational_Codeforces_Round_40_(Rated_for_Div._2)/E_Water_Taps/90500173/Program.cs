using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long T;
    W[] Ar;

    long[] MinA;
    long[] MinT;

    long[] MaxA;
    long[] MaxT;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        T = sc.NextLong();
        Ar = new W[N];

        {
            var a = sc.LongArray();
            var t = sc.LongArray();
            for (int i = 0; i < N; i++)
            {
                Ar[i] = new W(a[i], t[i]);
            }
            Array.Sort(Ar, (l, r) => l.T.CompareTo(r.T));
        }
        {
            MinA = new long[N + 1];
            MinT = new long[N + 1];
            for (int i = 0; i < N; i++)
            {
                MinA[i + 1] += MinA[i] + Ar[i].A;
                MinT[i + 1] += MinT[i] + Ar[i].T * Ar[i].A;
            }
        }
        {
            MaxA = new long[N + 1];
            MaxT = new long[N + 1];
            for (int i = 0; i < N; i++)
            {
                MaxA[i + 1] += MaxA[i] + Ar[N - i - 1].A;
                MaxT[i + 1] += MaxT[i] + Ar[N - i - 1].T * Ar[N - i - 1].A;
            }
        }

        decimal ok = 0;
        decimal ng = 0;
        for (int i = 0; i < N; i++)
        {
            ng += Ar[i].A;
        }

        for (int q = 0; q < 500; q++)
        {
            decimal mid = (ok + ng) / 2;
            if (F(mid))
            {
                ok = mid;
            }
            else
            {
                ng = mid;
            }
        }

        Console.WriteLine($"{ok:F20}");
    }

    bool F(decimal w)
    {
        decimal min;
        {
            if (w < Ar[0].A)
            {
                if (T < Ar[0].T) return false;
                min = decimal.MinValue;
            }
            else
            {
                int ok = 0;
                int ng = N + 1;
                while (ng - ok > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (MinA[mid] <= w) ok = mid;
                    else ng = mid;
                }

                min = MinT[ok];
                if (ok < N)
                {
                    min += Ar[ok].T * (w - MinA[ok]);
                }
            }
        }

        decimal max;
        {
            if (w < Ar[N - 1].A)
            {
                if (Ar[N - 1].T < T) return false;
                max = decimal.MaxValue;
            }
            else
            {
                int ok = 0;
                int ng = N + 1;
                while (ng - ok > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (MaxA[mid] <= w) ok = mid;
                    else ng = mid;
                }

                max = MaxT[ok];
                if (ok < N)
                {
                    max += Ar[N - ok - 1].T * (w - MaxA[ok]);
                }

            }
        }

        return min <= T * w && T * w <= max;
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct W
{
    public long A, T;
    public W(long a, long t)
    {
        A = a;
        T = t;
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
