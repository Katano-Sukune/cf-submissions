using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // 配列a
        // 2つの部分列に分割
        // 2つの和が同じになるような分割が無い 良い

        int min = int.MaxValue;
        foreach (int i in A)
        {
            int tmp = 1;
            while ((i & tmp) == 0) tmp <<= 1;
            min = Math.Min(min, tmp);
        }
        for (int i = 0; i < N; i++)
        {
            A[i] /= min;
        }

        int sum = 0;
        bool[] dp = new bool[200001];
        dp[0] = true;
        foreach (int i in A)
        {
            for (int j = sum; j >= 0; j--)
            {
                if (dp[j]) dp[j + i] = true;
            }
            sum += i;
        }

        if (sum % 2 == 1 || !dp[sum / 2])
        {
            Console.WriteLine("0");
            return;
        }

        Console.WriteLine("1");
        for (int i = 0; i < N; i++)
        {
            if (A[i] % 2 == 1)
            {
                Console.WriteLine(i+1);
                return;
            }
        }
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
