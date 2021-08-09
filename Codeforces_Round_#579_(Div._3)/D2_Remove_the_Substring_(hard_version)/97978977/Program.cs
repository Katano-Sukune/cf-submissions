using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private string S, T;
    private int N, M;

    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        T = sc.Next();
        N = S.Length;
        M = T.Length;

        int[] front = new int[N + 1];
        for (int i = 0; i < N; i++)
        {
            front[i + 1] = front[i];
            if (front[i] < M && T[front[i]] == S[i]) front[i + 1]++;
        }

        int[] back = new int[N + 1];
        for (int i = N; i >= 1; i--)
        {
            back[i - 1] = back[i];
            if (back[i] < M && T[M - 1 - back[i]] == S[i - 1]) back[i - 1]++;
        }

        int ans = 0;
        for (int i = 0; i <= N; i++)
        {
            int ok = i;
            int ng = N + 1;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (front[i] + back[mid] >= M) ok = mid;
                else ng = mid;
            }

            // Console.WriteLine($"{i} {front[i]} {ok} {back[ok]}");
            if (ok <= N) ans = Math.Max(ans, ok - i);
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