using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.Next(), sc.Next()));
        }
        Console.Write(sb.ToString());
    }

    private string Q(string s, string t)
    {
        /*
         * zは最初空
         * sの部分文字列を最後に追加
         * tに一致させる
         */
        var list = new List<int>[256];
        for (char c = 'a'; c <= 'z'; c++)
        {
            list[c] = new List<int>();
        }

        for (int i = 0; i < s.Length; i++)
        {
            list[s[i]].Add(i);
        }

        int cnt = 1;
        int pos = -1;

        foreach (char c in t)
        {
            if (list[c].Count == 0) return "-1";

            // pos強の探す
            // 無い
            if (list[c][list[c].Count - 1] <= pos)
            {
                cnt++;
                pos = list[c][0];
            }
            else
            {
                int ok = list[c].Count - 1;
                int ng = -1;

                while (ok - ng > 1)
                {
                    int med = (ok + ng) / 2;
                    if (list[c][med] > pos)
                    {
                        ok = med;
                    }
                    else
                    {
                        ng = med;
                    }
                }

                pos = list[c][ok];
            }
        }

        return cnt.ToString();
    }

    public static void Main() => new Program().Solve();
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