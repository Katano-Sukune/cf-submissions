using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    string[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        // カッコ列
        // S_i S_jがカッコ列のペア

        // min_iが非負end_i + min_jも非負
        // end_i + end_j = 0

        var map = new List<int>[300001];
        var map2 = new List<int>[300001];
        for (int i = 0; i <= 300000; i++)
        {
            map[i] = new List<int>();
            map2[i] = new List<int>();
        }
        foreach (var str in S)
        {
            int min = 0;
            int cnt = 0;
            foreach (var c in str)
            {
                if (c == '(') cnt++;
                else cnt--;
                min = Math.Min(min, cnt);
            }

            if (cnt >= 0)
            {
                map[cnt].Add(min);

            }
            else
            {
                map2[-cnt].Add(min);
            }

            // Console.WriteLine($"{str} {cnt} {min}");
        }

        long ans = 0;
        for (int i = 0; i <= 300000; i++)
        {

            long c = map[i].Count(num => num >= 0);
            if (i == 0)
            {
                ans += c * c;
            }
            else
            {
                long d = map2[i].Count(num => num >= -i);
                ans += c * d;
            }
        }
        Console.WriteLine(ans);
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
