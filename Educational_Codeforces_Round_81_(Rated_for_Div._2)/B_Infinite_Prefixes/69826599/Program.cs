using Complib.Generic;
using CompLib.Util;
using System;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextLong(), sc.NextLong(), sc.Next()));
        }

        Console.Write(sb.ToString());
    }

    private string Q(long n, long x, string s)
    {
        int tmp = 0;
        var map = new HashMap<long, int>();
        for (int i = 0; i < n; i++)
        {
            if (s[i] == '0') tmp++;
            else if (s[i] == '1') tmp--;
            map[tmp]++;
        }

        long min = long.MaxValue;
        long max = long.MinValue;
        foreach (var p in map)
        {
            min = Math.Min(min, p.Key);
            max = Math.Max(max, p.Key);
        }
        long ans = 0;
        if (x == 0) ans++;
        if (tmp == 0)
        {
            if (min <= x && x <= max)
            {
                return "-1";
            }
            else
            {
                return "0";
            }
        }
        else if (tmp > 0)
        {
            long a = Math.Max(0, (x - max) / tmp);
            for (; ; a++)
            {
                long t = a * tmp + min;
                if (x < t)
                {
                    break;
                }
                ans += map[x - a * tmp];
            }
        }
        else //if(tmp < 0)
        {
            long a = Math.Max(0, (x - min) / tmp );
            for (; ; a++)
            {
                long t = a * tmp + max;
                if (x > t)
                {
                    break;
                }
                ans += map[x - a * tmp];
            }
        }
        return ans.ToString();
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace Complib.Generic
{
    using System.Collections.Generic;
    class HashMap<K, V> : Dictionary<K, V>
    {
        public new V this[K key]
        {
            set { base[key] = value; }
            get
            {
                V o;
                return TryGetValue(key, out o) ? o : base[key] = default(V);
            }
        }
    }
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}