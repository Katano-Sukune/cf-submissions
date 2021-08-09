using CompLib.Util;
using System;
using System.Linq;
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
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.Next(), sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, int m, string s, int[] p)
    {
        Array.Sort(p);

        var cnt = new int[26];
        var ans = new int[26];
        int index = 0;
        for (int i = 0; i < n; i++)
        {
            cnt[s[i] - 'a']++;
            if (index < m && p[index] == i + 1)
            {
                while (index < m && p[index] == i + 1)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        ans[j] += cnt[j];
                    }
                    index++;
                }
            }
        }
        for(int i = 0; i < 26; i++)
        {
            ans[i] += cnt[i];
        }
        return string.Join(" ", ans);
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