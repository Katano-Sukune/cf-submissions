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
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        var map = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            int o;
            int e;
            if (map.TryGetValue(a[i] - 1, out o))
            {
                map.Remove(a[i] - 1);
                e = o + 1;
            }
            else
            {
                e = 1;
            }

            map.TryGetValue(a[i], out o);
            map[a[i]] = Math.Max(o, e);
        }

        int max = -1;
        int end = -1;
        foreach (var pair in map)
        {
            if (max < pair.Value)
            {
                max = pair.Value;
                end = pair.Key;
            }
        }

        Console.WriteLine(max);
        int tmp = end - max + 1;
        var ls = new List<int>(max);
        for (int i = 0; i < n; i++)
        {
            if (a[i] == tmp)
            {
                ls.Add(i + 1);
                tmp++;
            }
        }

        Console.WriteLine(string.Join(" ", ls));
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