using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();

        // 2番目
        int second = Q(1, n);

        if (second != 1 && Q(1, second) == second)
        {
            // [1,second]に最大がある
            int ok = 1;
            int ng = second;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (Q(mid, second) == second) ok = mid;
                else ng = mid;
            }

            Console.WriteLine($"! {ok}");
        }
        else
        {
            // [second,n]に最大がある
            int ok = n;
            int ng = second;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (Q(second, mid) == second) ok = mid;
                else ng = mid;
            }

            Console.WriteLine($"! {ok}");
        }
    }

    int Q(int l, int r)
    {
        Console.WriteLine($"? {l} {r}");
        return int.Parse(Console.ReadLine());
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