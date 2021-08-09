using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private long A, B, C, D;

    void Q(Scanner sc)
    {
        A = sc.NextInt();
        B = sc.NextInt();
        C = sc.NextInt();
        D = sc.NextInt();

        // 使う
        // Aダメージ
        // C秒連続で B回復

        // D秒クールタイム

        if (B * C < A)
        {
            Console.WriteLine("-1");
            return;
        }

        if (C <= D)
        {
            Console.WriteLine(Math.Max(A, 2 * A - C * B));
            return;
        }

        long ok = 0;
        long ng = C / D + 1;

        while (ng - ok > 1)
        {
            long mid = (ok + ng) / 2;
            if (F(mid) < F(mid + 1))
            {
                ok = mid;
            }
            else
            {
                ng = mid;
            }
        }

        // 11
        // 9
        // 6
        // 3
        Console.WriteLine(F(ng));
    }

    long F(long f)
    {
        if (f == 0) return 0;
        long p = A * f;
        long t = D * (f - 1);

        return p - B * (D + t) * (f - 1) / 2;
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