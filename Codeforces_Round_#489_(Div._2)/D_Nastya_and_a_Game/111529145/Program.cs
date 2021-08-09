using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, K;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();

        // iから始めて1の個数
        int[] cnt1 = new int[N + 1];
        for (int i = N - 1; i >= 0; i--)
        {
            if (A[i] == 1)
            {
                cnt1[i] = cnt1[i + 1] + 1;
            }
        }

        long ans = 0;
        const long INF = (long) 1e18;
        for (int l = 0; l < N; l++)
        {
            long sum = 0;
            long mul = 1;
            for (int r = l; r < N;)
            {
                if (A[r] == 1)
                {
                    // sum+1 to sum+cnt1[r]
                    if (mul % K == 0 && sum + 1 <= mul / K && mul / K <= sum + cnt1[r]) ans++;
                    sum += cnt1[r];
                    r += cnt1[r];
                }
                else
                {
                    if (A[r] > INF / mul) break;
                    mul *= A[r];
                    sum += A[r];
                    if (mul % sum == 0 && mul / sum == K) ans++;
                    r++;
                }
            }
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