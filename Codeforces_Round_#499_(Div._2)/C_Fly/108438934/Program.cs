using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        A = sc.IntArray();
        B = sc.IntArray();

        double ok = long.MaxValue;
        double ng = 0;
        for (int i = 0; i < 1000; i++)
        {
            double mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        if (ok > 1e18)
        {
            Console.WriteLine("-1");
            return;
        }
        // 10
        // 22 
        
        Console.WriteLine($"{ok:F12}");
    }

    bool F(double f)
    {
        for (int i = 0; i < N; i++)
        {
            // 離陸
            double fuel = (M + f) / A[i];
            if (fuel > f) return false;
            f -= fuel;

            double fuel2 = (M + f) / (B[(i + 1) % N]);
            if (fuel2 > f) return false;
            f -= fuel2;
        }

        return true;
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