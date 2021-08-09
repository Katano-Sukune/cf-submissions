using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        string s = sc.Next();
        int n = s.Length;
        int[] cnt = new int[26];
        foreach (var c in s)
        {
            cnt[c - 'a']++;
        }

        // n偶数
        // 各文字 偶数個
        // 奇数のやつ
        // 後ろ半分 前半分にする

        // n奇数
        // 1個だけ奇数　のこり偶数
        // 奇数のやつ
        // 2番目のこして小さいやつに集める

        var odd = new List<int>();
        for (int i = 0; i < 26; i++)
        {
            if (cnt[i] % 2 == 1) odd.Add(i);
        }

        for (int i = 0; i < odd.Count / 2; i++)
        {
            cnt[odd[i]]++;
            cnt[odd[odd.Count - i - 1]]--;
        }

        char[] ans = new char[n];
        int ptr = 0;
        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < cnt[i] / 2; j++)
            {
                ans[ptr] = ans[n - ptr - 1] = (char)(i + 'a');
                ptr++;
            }
        }
        if (n % 2 == 1) ans[ptr] = (char)(odd[odd.Count / 2] + 'a');
        Console.WriteLine(new string(ans));
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
