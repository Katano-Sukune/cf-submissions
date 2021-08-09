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
        string s = sc.Next();
        string t = sc.Next();

        List<int> ab = new List<int>();
        List<int> ba = new List<int>();

        for (int i = 0; i < n; i++)
        {
            if (s[i] == 'a' && t[i] == 'b') ab.Add(i);
            if (s[i] == 'b' && t[i] == 'a') ba.Add(i);
        }

        if (ab.Count % 2 != ba.Count % 2)
        {
            Console.WriteLine("-1");
            return;
        }

        List<string> ans = new List<string>();
        for (int i = 0; i < ab.Count / 2; i++)
        {
            ans.Add($"{ab[i * 2] + 1} {ab[i * 2 + 1] + 1}");
        }

        for (int i = 0; i < ba.Count / 2; i++)
        {
            ans.Add($"{ba[i * 2] + 1} {ba[i * 2 + 1] + 1}");
        }

        if (ab.Count % 2 == 1)
        {
            ans.Add($"{ab[^1] + 1} {ab[^1] + 1}");
            ans.Add($"{ab[^1] + 1} {ba[^1] + 1}");
        }

        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join("\n", ans));
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