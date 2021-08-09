using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // 桁数一致 3つ
        // 上2つxor

        int[] d = new int[N];
        for (int i = 0; i < N; i++)
        {
            int tmp = A[i];
            while (tmp > 0)
            {
                d[i]++;
                tmp /= 2;
            }
        }

        for (int i = 0; i + 3 <= N; i++)
        {
            if (d[i] == d[i + 1] && d[i + 1] == d[i + 2])
            {
                Console.WriteLine("1");
                return;
            }
        }

        int ans = int.MaxValue;
        // 高々60個
        for (int i = 0; i < N - 1; i++)
        {
            // i,i+1を比較
            int[] f = new int[i + 1];
            f[0] = A[i];
            for (int j = 1; j <= i; j++)
            {
                f[j] = f[j - 1] ^ A[i - j];
            }

            int[] b = new int[N - i - 1];
            b[0] = A[i + 1];
            for (int j = 1; j < N - i - 1; j++)
            {
                b[j] = b[j - 1] ^ A[i + j + 1];
            }

            for (int j = 0; j < f.Length; j++)
            {
                for (int k = 0; k < b.Length; k++)
                {
                    if (f[j] > b[k])
                    {
                        ans = Math.Min(ans, j + k);
                    }
                }
            }
        }

        if (ans == int.MaxValue)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(ans);
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