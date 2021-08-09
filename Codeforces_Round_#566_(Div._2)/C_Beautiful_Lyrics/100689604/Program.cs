using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private string[] S;

    private readonly char[] Boin = {'a', 'e', 'o', 'i', 'u'};

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        (int cnt, char last)[] ar = new (int cnt, char last)[N];
        int max = 0;
        for (int i = 0; i < N; i++)
        {
            var s = S[i];
            int cnt = 0;
            char last = 'a';
            foreach (char c in s)
            {
                if (Boin.Contains(c))
                {
                    cnt++;
                    last = c;
                }
            }

            max = Math.Max(max, cnt);
            ar[i] = (cnt, last);
        }

        var ls = new List<int>[max + 1, 256];
        for (int i = 0; i <= max; i++)
        {
            foreach (char c in Boin)
            {
                ls[i, c] = new List<int>();
            }
        }

        for (int i = 0; i < N; i++)
        {
            ls[ar[i].cnt, ar[i].last].Add(i);
        }

        // 個数、最後同じ
        int pair = 0;
        for (int i = 0; i <= max; i++)
        {
            foreach (char c in Boin)
            {
                pair += ls[i, c].Count / 2;
            }
        }

        // 個数同じ
        int pair2 = 0;
        for (int i = 0; i <= max; i++)
        {
            int cnt = 0;
            foreach (char c in Boin)
            {
                cnt += ls[i, c].Count;
            }

            pair2 += cnt / 2;
        }

        int m = Math.Min(pair2 / 2, pair);
        List<string> a1 = new List<string>(m);
        List<string> b1 = new List<string>(m);
        List<string> a2 = new List<string>(m);
        List<string> b2 = new List<string>(m);
        var flag = new bool[N];
        int tmp1 = 0;
        for (int i = 0; i <= max && tmp1 < m; i++)
        {
            for (int j = 0; j < Boin.Length && tmp1 < m; j++)
            {
                char c = Boin[j];
                for (int k = 1; k < ls[i, c].Count && tmp1 < m; k += 2)
                {
                    b1.Add(S[ls[i, c][k - 1]]);
                    b2.Add(S[ls[i, c][k]]);
                    flag[ls[i, c][k - 1]] = true;
                    flag[ls[i, c][k]] = true;
                    tmp1++;
                }
            }
        }

        int tmp2 = 0;
        for (int i = 0; i <= max && tmp2 < m; i++)
        {
            var ls2 = new List<int>();
            foreach (char c in Boin)
            {
                foreach (int j in ls[i, c])
                {
                    if (flag[j]) continue;
                    ls2.Add(j);
                }
            }

            for (int j = 1; j < ls2.Count && tmp2 < m; j += 2)
            {
                a1.Add(S[ls2[j - 1]]);
                a2.Add(S[ls2[j]]);
                tmp2++;
            }
        }

        var sb = new StringBuilder();
        sb.AppendLine(m.ToString());
        for (int i = 0; i < m; i++)
        {
            sb.AppendLine($"{a1[i]} {b1[i]}");
            sb.AppendLine($"{a2[i]} {b2[i]}");
        }

        Console.Write(sb);
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