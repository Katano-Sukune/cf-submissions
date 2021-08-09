using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int[] p = sc.IntArray();


        // 区間 [l,r]の中央値がmになる(l,r)の個数


        // 区間に m未満が　(len-1)/2
        // m超 len/2
        // 偶数長 大きい-小さい=1
        // 奇数 大きい-小さい= 0

        // 大きい-小さい+200000
        var front = new int[400001];
        var back = new int[400001];

        int index = Array.IndexOf(p, m);
        back[200000]++;
        int tmp = 200000;
        for (int i = index + 1; i < n; i++)
        {
            tmp += p[i] > m ? 1 : -1;
            back[tmp]++;
        }
        front[200000]++;
        tmp = 200000;
        for (int i = index - 1; i >= 0; i--)
        {
            tmp += p[i] > m ? 1 : -1;
            front[tmp]++;
        }
        long ans = 0;
        for (int i = 0; i <= 400000; i++)
        {
            // i+j == 400000 
            int j = 400000 - i;
            if (0 <= j && j <= 400000) ans += (long)front[i] * back[j];
            j = 400001 - i;
            if (0 <= j && j <= 400000) ans += (long)front[i] * back[j];
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
