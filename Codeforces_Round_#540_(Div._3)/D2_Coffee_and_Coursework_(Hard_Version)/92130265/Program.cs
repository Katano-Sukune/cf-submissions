using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long M;
    long[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        A = sc.LongArray();

        Array.Sort(A, (l, r) => r.CompareTo(l));

        /*
         * mページ書く
         * 
         * n個コーヒー
         * 
         * iはa_iカフェイン
         * 
         * k杯飲む
         * 1杯目 a_i
         * 2    a_i-1
         * ...
         * 
         * 最小日数
         */

        int ok = N + 1;
        int ng = 0;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        if(ok <= N)
        {
            Console.WriteLine(ok);
        }
        else
        {
            Console.WriteLine("-1");
        }
    }

    // d日
    bool F(int d)
    {
        // 
        long t = 0;
        for (int i = 0; i < N; i++)
        {
            t += Math.Max(0, A[i] - i / d);
        }

        return t >= M;

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
