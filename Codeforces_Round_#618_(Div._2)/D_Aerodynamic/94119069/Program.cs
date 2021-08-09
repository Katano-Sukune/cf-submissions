using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private (int X, int Y)[] P;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = new (int X, int Y)[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = (sc.NextInt(), sc.NextInt());
        }

        if (N % 2 != 0)
        {
            Console.WriteLine("NO");
            return;
        }

        for (int i = 0; i < N / 2; i++)
        {
            var p1 = P[i];
            var p2 = P[i + 1];
            var p3 = P[i + N / 2];
            var p4 = P[(i + N / 2 + 1) % N];

            if (p2.X - p1.X != p3.X - p4.X)
            {
                Console.WriteLine("NO");
                return;
            }

            if (p2.Y - p1.Y != p3.Y - p4.Y)
            {
                Console.WriteLine("NO");
                return;
            }
        }

        Console.WriteLine("YES");
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