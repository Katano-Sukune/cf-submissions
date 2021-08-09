using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] u = sc.IntArray();

        int[] cnt = new int[100001];

        var map = new Dictionary<int, int>();

        int ans = 1;
        for (int x = 0; x < n; x++)
        {

            if (cnt[u[x]] != 0)
            {
                map[cnt[u[x]]]--;
                if (map[cnt[u[x]]] == 0) map.Remove(cnt[u[x]]);
            }
            cnt[u[x]]++;
            int o;
            if (!map.TryGetValue(cnt[u[x]], out o)) map[cnt[u[x]]] = 0;
            map[cnt[u[x]]]++;

            if (map.Count == 2)
            {
                var ar = map.ToArray();
                if (ar[0].Key > ar[1].Key)
                {
                    var t = ar[0];
                    ar[0] = ar[1];
                    ar[1] = t;
                }

                if ((ar[0].Key == 1 && ar[0].Value == 1) || (ar[0].Key + 1 == ar[1].Key && ar[1].Value == 1))
                {
                    ans = x + 1;
                }
            }
        }

        if (map.Count == 1)
        {
            var a = map.ToArray();
            if (a[0].Value == 1 || a[0].Key == 1)
            {
                ans = n;
            }
        }

        Console.WriteLine(ans);
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
