using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, K;
    int[] Ans;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();

        // マージソート
        // mergesort(a,l,r)再帰

        // 呼び出し回数 k回になるa

        /*
         * 1 1
         * 2 1 3
         * 3 1 3 5
         * 4 1 3 5 7
         * 5 1 3 5 7 9
         */

        // 2n-1以下奇数OK

        if (K > 2 * N - 1)
        {
            Console.WriteLine("-1");
            return;
        }

        if (K % 2 != 1)
        {
            Console.WriteLine("-1");
            return;
        }

        // 分けない 1
        // 分ける 右 + 左 + 1
        Ans = new int[N];
        Calc(0, N, 1, K);

        Console.WriteLine(string.Join(" ", Ans));
    }

    // [begin,end)を最小min,最大 min+(end-begin)-1
    // k回になるようにする
    void Calc(int begin, int end, int min, int k)
    {
        if (k == 1)
        {
            for (int i = 0; i < end - begin; i++)
            {
                Ans[begin + i] = min + i;
            }
            return;
        }

        int mid = (begin + end) / 2;
        int leftLen = mid - begin;
        int leftK = Math.Min(k - 2, leftLen * 2 - 1);
        Calc(begin, mid, min + end - mid, leftK);
        Calc(mid, end, min, k - 1 - leftK);
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
