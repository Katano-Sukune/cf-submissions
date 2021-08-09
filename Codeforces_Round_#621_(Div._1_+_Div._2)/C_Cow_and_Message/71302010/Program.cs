using CompLib.Util;
using System;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        string S = sc.Next();

        // 取るindexが等差数列になるように部分文字列を選ぶ
        // 多く出る部分文字列はいくつ出るか?

        // 長さ3以上が最大 -> 前2つだけ　同じ数

        // 2以下

        var cnt = new int[26];

        var len2 = new long[26 * 26];

        for (int i = 0; i < S.Length; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                len2[j * 26 + S[i] - 'a'] += cnt[j];
            }
            cnt[S[i] - 'a']++;
        }

        Console.WriteLine(Math.Max(len2.Max(), cnt.Max()));
    }

    public static void Main(string[] args) => new Program().Solve();
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