using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] C;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        C = sc.IntArray();

        // n-順列 C

        /*
         * 2 <= k <= n
         * 空でないk個に分ける
         *
         * k個の順番を逆にする
         *
         * ソート
         *
         * n回まで
         */

        // trueなら前に
        // falseなら後ろにある
        int mid = N / 2 + 1;
        var q = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < N; i++)
        {
            var ls = new List<int>();
            if (i == 0)
            {
                int target = mid;
                int index = Array.IndexOf(C, target);
                if (index == 0) continue;
                ls.Add(1);
                ls.Add(index-1);
                ls.Add(N - index);
            }
            else if (i % 2 == 0)
            {
                int target = mid + (i + 1) / 2;
                int index = Array.IndexOf(C, target);
                ls.Add(index);
                ls.Add(N - index - i);
                ls.Add(i);
            }
            else
            {
                int target = mid - (i + 1) / 2;
                int index = Array.IndexOf(C, target);
                ls.Add(i);
                ls.Add(index + 1 - i);
                ls.Add(N - ls[0] - ls[1]);
            }

            ls.RemoveAll((num) => num == 0);
            q++;

            sb.AppendLine($"{ls.Count} {string.Join(" ", ls)}");

            C = Exec(C, ls);
            // Console.WriteLine(string.Join(" ", C));
        }

        Console.WriteLine(q);
        Console.Write(sb);
    }

    int[] Exec(int[] ar, List<int> l)
    {
        List<List<int>> ls = new List<List<int>>();
        int ptr = 0;
        for (int i = 0; i < l.Count; i++)
        {
            ls.Add(new List<int>());
            for (int j = 0; j < l[i]; j++)
            {
                ls[i].Add(ar[ptr++]);
            }
        }

        List<int> ans = new List<int>();
        for (int i = ls.Count - 1; i >= 0; i--)
        {
            ans.AddRange(ls[i]);
        }

        return ans.ToArray();
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