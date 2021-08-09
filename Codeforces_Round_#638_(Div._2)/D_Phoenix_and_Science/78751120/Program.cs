using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();


        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Q(long n)
    {
        long cnt = 1;
        long sum = 1;
        bool f = true;
        var l = new List<long>();
        while (sum < n)
        {
            // 増やさない cnt増える
            // 増やす i増やす 0 <= i <= cnt
            // cnt + i増える
            long d = n - sum;

            // 増やす
            if (cnt <= d && d <= cnt * 2)
            {
                l.Add(d - cnt);
                cnt = d;
            }
            else if (d > (cnt * 2) * 2)
            {
                l.Add(cnt);
                cnt += cnt;
            }
            else
            {
                // cnt*2 < d <= cnt*2*2
                l.Add(d / 2 - cnt);
                cnt += d / 2 - cnt;
                sum += cnt;
                if (d % 2 == 1)
                {
                    l.Add(1);
                    cnt++;
                }
                else
                {
                    l.Add(0);
                }
            }
            sum += cnt;
        }
        return $"{l.Count}\n{string.Join(" ", l)}";
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}

