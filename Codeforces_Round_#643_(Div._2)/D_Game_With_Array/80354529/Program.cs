using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.NextInt();

        if (N == 1)
        {
            if (S == 1)
            {
                Console.WriteLine("NO");
                return;
            }
            else
            {
                Console.WriteLine("YES");
                Console.WriteLine(S);
                Console.WriteLine(S - 1);
            }
        }
        else if (N == 2)
        {
            if (S == 2 || S == 3)
            {
                Console.WriteLine("NO");
                return;
                // 1 1
                // 1 2
            }
            else
            {
                Console.WriteLine("YES");
                Console.WriteLine($"2 {S - 2}");
                Console.WriteLine(1);
                return;
                // 2 S-2
                // K = 1
            }
        }
        else
        {
            if (N * 2 <= S)
            {
                // 22222....
                var ans = new int[N];
                var d = S / N;
                var m = S % N;
                for (int i = 0; i < N; i++)
                {
                    if (i < m)
                    {
                        ans[i] = d + 1;
                    }
                    else
                    {
                        ans[i] = d;
                    }
                }
                Console.WriteLine("YES");
                Console.WriteLine(string.Join(" ", ans));
                Console.WriteLine("1");
                return;
            }
            else
            {
                Console.WriteLine("NO");
                return;
            }
        }
    }

    public static void Main(string[] args) => new Program().Solve();
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
