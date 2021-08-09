using System;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();
        var map = new int[26 * 26];
        var t = s[0] - 'A';
        for (int i = 1; i < s.Length; i++)
        {
            t *= 26;
            t += s[i] - 'A';
            map[t]++;
            t %= 26;
        }

        int index = 0;
        int max = int.MinValue;
        for (int i = 0; i < 26 * 26; i++)
        {
            if (max < map[i])
            {
                max = map[i];
                index = i;
            }
        }

        char a = (char)('A' + index / 26);
        char b = (char)('A' + index % 26);
        Console.WriteLine($"{a}{b}");
    }

    public static void Main(string[] args) => new Program().Solve();
}



namespace CompLib.Collections
{
    using System.Collections.Generic;
    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue o;
                return TryGetValue(key, out o) ? o : default(TValue);
            }
            set { base[key] = value; }
        }
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
