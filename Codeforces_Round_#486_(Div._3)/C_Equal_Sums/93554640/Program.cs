using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int k = sc.NextInt();
        int[] n = new int[k];
        int[][] a = new int[k][];
        for (int i = 0; i < k; i++)
        {
            n[i] = sc.NextInt();
            a[i] = new int[n[i]];
            for (int j = 0; j < n[i]; j++)
            {
                a[i][j] = sc.NextInt();
            }
        }

        var map = new Dictionary<long, (int i, int x)>();
        for (int j = 0; j < k; j++)
        {
            long sum = 0;
            foreach (var num in a[j])
            {
                sum += num;
            }

            var add = new long[n[j]];
            for (int y = 0; y < n[j]; y++)
            {
                long tmp = sum - a[j][y];
                (int i, int x) o;
                if (map.TryGetValue(tmp, out o))
                {
                    Console.WriteLine("YES");
                    Console.WriteLine($"{o.i + 1} {o.x + 1}");
                    Console.WriteLine($"{j + 1} {y + 1}");
                    return;
                }
                add[y] = tmp;
            }

            for (int y = 0; y < n[j]; y++)
            {
                map[add[y]] = (j, y);
            }
        }

        Console.WriteLine("NO");
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
