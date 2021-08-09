using System;
using CompLib.Util;

public class Program
{
    private int N;
    private long Z;
    private long[] X;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Z = sc.NextLong();
        X = sc.LongArray();
        Array.Sort(X);
        int ok = 0;
        int ng = N / 2 + 1;
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            if (C(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(ok);
    }

    // k個作れるか
    bool C(int k)
    {
        for (int i = 0; i < k; i++)
        {
            // 小さい方 X_i
            // 大きい方 X_{N-k+i}
            if (X[N - k + i] - X[i] < Z) return false;
        }

        return true;
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