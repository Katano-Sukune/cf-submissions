using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int K;
    private string[] S;

    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextInt();
        S = sc.ReadLine().Split(' ', '-');
        int ng = 0;
        int ok = int.MaxValue;

        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(ok);
    }

    bool F(int l)
    {
        int cnt = 1;
        int tmp = 0;
        for (int i = 0; i < S.Length; i++)
        {
            int len = i == S.Length - 1 ? S[i].Length : S[i].Length + 1;
            if (tmp + len <= l)
            {
                tmp += len;
            }
            else
            {
                cnt++;
                tmp = len;
                if (tmp > l) return false;
            }
        }

        return cnt <= K;
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