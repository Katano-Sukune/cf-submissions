using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    private int[] A, B;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        /*
         * 都市iにはA_i世帯ある
         *
         * ネットワークステーションjは 都市i, i+1に送れる
         *
         * 全世帯に送れるか?
         */

        // 局0は都市1に全部送る

        var tmp1 = new int[N];
        Array.Copy(A, tmp1, N);

        tmp1[1] -= Math.Min(tmp1[1], B[0]);
        for (int i = 1; i < N; i++)
        {
            if (B[i] < tmp1[i])
            {
                Console.WriteLine("NO");
                return;
            }

            int l = tmp1[i];
            int r = Math.Min(tmp1[(i + 1) % N], B[i] - l);

            tmp1[i] -= l;
            tmp1[(i + 1) % N] -= r;
        }

        // 足りない分
        int m = tmp1[0];

        if (B[0] < m)
        {
            Console.WriteLine("NO");
            return;
        }

        var tmp2 = new int[N];
        Array.Copy(A, tmp2, N);
        tmp2[0] -= m;
        tmp2[1] -= Math.Min(tmp2[1], B[0] - m);

        for (int i = 1; i < N; i++)
        {
            if (B[i] < tmp2[i])
            {
                Console.WriteLine("NO");
                return;
            }

            int l = tmp2[i];
            int r = Math.Min(tmp2[(i + 1) % N], B[i] - l);
            tmp2[i] -= l;
            tmp2[(i + 1) % N] -= r;
        }

        if (tmp2[0] != 0)
        {
            Console.WriteLine("NO");
            return;
        }

        Console.WriteLine("YES");
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