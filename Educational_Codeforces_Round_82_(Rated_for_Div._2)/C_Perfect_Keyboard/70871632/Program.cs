using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.Next()));
        }

        Console.Write(sb.ToString());
    }

    private String Q(string s)
    {
        // sを入力したい
        // 英小文字の順列　キーボード
        // キーボードの隣あってるやつを押すだけで入力できるか?
        bool[] used = new bool[26];
        int index = 0;
        var map = new Dictionary<char, int>();
        var hs = new HashSet<int>();
        map[s[0]] = 0;
        hs.Add(0);

        used[s[0] - 'a'] = true;
        for (int i = 1; i < s.Length; i++)
        {
            used[s[i] - 'a'] = true;
            int o;
            if (map.TryGetValue(s[i], out o))
            {
                // あるなら隣接してるか
                if (Math.Abs(o - index) == 1)
                {
                    index = o;
                }
                else
                {
                    return "NO";
                }
            }
            else
            {
                // 無い
                // 一個となりが埋まってるか
                if (hs.Contains(index + 1))
                {
                    if (hs.Contains(index - 1))
                    {
                        return "NO";
                    }
                    else
                    {
                        map[s[i]] = --index;
                        hs.Add(index);
                    }
                }
                else
                {
                    map[s[i]] = ++index;
                    hs.Add(index);
                }
            }
        }

        int min = hs.Min();
        char[] ans = new char[26];
        foreach (var pair in map)
        {
            ans[pair.Value - min] = pair.Key;
        }
        int l = hs.Max() - hs.Min() + 1;
        for (char c = 'a'; c <= 'z'; c++)
        {
            if (!used[c - 'a'])
            {
                ans[l++] = c;
            }
        }

        return $"YES\n{new string(ans)}";
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