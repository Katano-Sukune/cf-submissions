using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Algorithm;
using CompLib.Util;

public class Program
{
    int N;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        var hs = new HashSet<int>();
        for (int i = 1; i * i <= N; i++)
        {
            hs.Add(i * i);
        }

        int ans = int.MaxValue;
        string str = N.ToString();
        int len = str.Length;
        int l = 1 << len;
        for (int i = 0; i < l; i++)
        {
            // 立ってるbit 消す
            int t = 0;
            bool f = true;
            for (int j = 0; j < len && f; j++)
            {
                if ((i & (1 << j)) != 0) continue;
                if (t == 0 && str[j] == '0') f = false;
                t *= 10;
                t += str[j] - '0';
            }
            if (!f) continue;

            if (hs.Contains(t))
            {
                ans = Math.Min(ans, Algorithm.BitCount(i));
            }
        }

        if(ans == int.MaxValue)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Algorithm
{
    static class Algorithm
    {
        /// <summary>
        /// nの立っているbitの数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
       　public static int BitCount(long n)
        {
            n = (n & 0x5555555555555555) + (n >> 1 & 0x5555555555555555);
            n = (n & 0x3333333333333333) + (n >> 2 & 0x3333333333333333);
            n = (n & 0x0f0f0f0f0f0f0f0f) + (n >> 4 & 0x0f0f0f0f0f0f0f0f);
            n = (n & 0x00ff00ff00ff00ff) + (n >> 8 & 0x00ff00ff00ff00ff);
            n = (n & 0x0000ffff0000ffff) + (n >> 16 & 0x0000ffff0000ffff);
            return (int)((n & 0x00000000ffffffff) + (n >> 32 & 0x00000000ffffffff));
        }
    }
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
