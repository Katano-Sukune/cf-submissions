using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private long K;
    private int[] F;
    private int[] W;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextLong();
        F = sc.IntArray();
        W = sc.IntArray();

        /*
         * 頂点i -> 頂点F_i W_iの辺
         *
         * s_i
         * iから始まる長さkのパスのWの総和
         *
         * m_i iから始まる長さkのパスのWの最小値
         */

        int[] to = new int[N];
        long[] sum = new long[N];
        int[] min = new int[N];
        for (int i = 0; i < N; i++)
        {
            to[i] = F[i];
            sum[i] = W[i];
            min[i] = W[i];
        }

        long[] s = new long[N];
        int[] m = new int[N];
        int[] cur = new int[N];
        for (int i = 0; i < N; i++)
        {
            m[i] = int.MaxValue;
            cur[i] = i;
        }

        while (K > 0)
        {
            if (K % 2 == 1)
            {
                // Console.WriteLine(string.Join(" ", cur));
                for (int i = 0; i < N; i++)
                {
                    s[i] += sum[cur[i]];
                    m[i] = Math.Min(m[i], min[cur[i]]);
                    cur[i] = to[cur[i]];
                }

                // Console.WriteLine(string.Join(" ", cur));
            }

            var nSum = new long[N];
            var nMin = new int[N];
            var nTo = new int[N];
            for (int i = 0; i < N; i++)
            {
                nSum[i] = sum[i] + sum[to[i]];
                nMin[i] = Math.Min(min[i], min[to[i]]);
                nTo[i] = to[to[i]];
            }

            sum = nSum;
            min = nMin;
            to = nTo;

            K /= 2;
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine($"{s[i]} {m[i]}");
        }

        Console.Out.Flush();
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