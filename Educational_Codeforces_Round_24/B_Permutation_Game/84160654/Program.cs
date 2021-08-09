using System;
using CompLib.Util;

public class Program
{
    private int N, M;
    private int[] L;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        L = sc.IntArray();

        // Mステップ　ゲーム
        // リーダー i
        // A 順列

        // 次のA_i番目を指定して リーダーにする
        // リーダーになった順 Lが与えられる 


        // Aを出力 or 存在しない -1

        bool[] used = new bool[N + 1];
        int[] a = new int[N];
        for (int i = 0; i < M - 1; i++)
        {
            int d = (L[i + 1] - L[i] + N) % N;
            if (d == 0) d = N;
            if (a[L[i] - 1] == 0)
            {
                if (used[d])
                {
                    Console.WriteLine("-1");
                    return;
                }

                used[d] = true;
                a[L[i] - 1] = d;
            }
            else
            {
                if (a[L[i] - 1] != d)
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }

        int index = 1;
        for (int i = 0; i < N; i++)
        {
            if (a[i] == 0)
            {
                while (used[index])
                {
                    index++;
                }

                a[i] = index;
                used[index] = true;
            }
        }

        Console.WriteLine(string.Join(" ", a));
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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