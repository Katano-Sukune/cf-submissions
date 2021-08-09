using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    int M;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        M = sc.NextInt();

        /*
         * n-順列 pの転倒
         * 
         * [l,r]を逆順にする
         * 転倒数の偶奇
         */

        /*
         * 偶数長反転
         * iについて
         * i以前のi超過減る
         * 
         * i以降のi超過増える
         * 
         * i超過の個数偶数変わらない
         * 奇数　変わる
         * 
         * 
         */

        /*
         * 最初転倒数
         */

        bool odd = false;
        for (int i = 0; i < N; i++)
        {
            for (int j = i + 1; j < N; j++)
            {
                if (A[i] > A[j]) odd ^= true;
            }
        }

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < M; i++)
        {
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();

            int len = r - l;
            // gkgkg
            // %4 = 1変わらない
            // kgkg
            // %4 = 0

            if (len % 4 == 2 || len % 4 == 3)
            {
                odd ^= true;
            }
            Console.WriteLine(odd ? "odd" : "even");
        }

        Console.Out.Flush();

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
