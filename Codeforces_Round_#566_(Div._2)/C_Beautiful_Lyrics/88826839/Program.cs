using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    private const string Boin = "aiueo";

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }

        var map = new Dictionary<(int cnt, char last), List<int>>();
        var map2 = new Dictionary<int, List<int>>();
        for (int i = 0; i < n; i++)
        {
            char last = '\0';
            int cnt = 0;
            foreach (char c in s[i])
            {
                if (Boin.Contains(c))
                {
                    cnt++;
                    last = c;
                }
            }

            List<int> o;
            if (!map.TryGetValue((cnt, last), out o))
            {
                o = new List<int>();
                map[(cnt, last)] = o;
            }

            o.Add(i);

            if (!map2.TryGetValue(cnt, out o))
            {
                o = new List<int>();
                map2[cnt] = o;
            }

            o.Add(i);
        }

        /*
         * 全部2
         * 
         */

        // 母音個数同じペア最大
        int p1 = 0;
        foreach (var pair in map2)
        {
            p1 += pair.Value.Count / 2;
        }

        // 母音個数、最後母音一致ペア最大
        int p2 = 0;
        foreach (var pair in map)
        {
            p2 += pair.Value.Count / 2;
        }

        int m = Math.Min(p1 / 2, p2);

        bool[] used = new bool[n];
        var t = new int[m, 2, 2];
        int tmp = 0;
        foreach (var pair in map)
        {
            if (tmp >= m) break;
            for (int i = 1; i < pair.Value.Count && tmp < m; i += 2)
            {
                t[tmp, 0, 1] = pair.Value[i - 1];
                t[tmp, 1, 1] = pair.Value[i];
                used[pair.Value[i - 1]] = true;
                used[pair.Value[i]] = true;
                tmp++;
            }
        }

        tmp = 0;
        foreach (var pair in map2)
        {
            if(tmp >= m) break;
            var ls = pair.Value.Where(i => !used[i]).ToArray();
            for (int i = 1; i < ls.Length && tmp < m; i += 2)
            {
                t[tmp, 0, 0] = ls[i - 1];
                t[tmp, 1, 0] = ls[i];
                used[ls[i - 1]] = true;
                used[ls[i]] = true;
                tmp++;
            }
        }
        
        var sb = new StringBuilder();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                sb.AppendLine($"{s[t[i, j, 0]]} {s[t[i, j, 1]]}");
            }
        }

        Console.WriteLine(m);
        Console.Write(sb);
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