using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, K;
    private string S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();

        bool[] f = new bool[N];
        for (int i = 0; i < N; i++)
        {
            f[i] = S[i] == S[(i + 1) % N] || S[i] == S[(i + N - 1) % N];
        }

        if (!f.Contains(true))
        {
            char[] t = new char[N];
            for (int i = 0; i < N; i++)
            {
                t[i] = S[(i + K) % 2];
            }

            Console.WriteLine(new string(t));
            return;
        }

        // 近い方
        int[] fDist = new int[N];
        int[] bDist = new int[N];
        char[] ff = new char[N];
        char[] bb = new char[N];
        int b = Array.IndexOf(f, true);
        for (int i = 0; i < N; i++)
        {
            if (f[(b + i) % N])
            {
                fDist[(b + i) % N] = 0;
                ff[(b + i) % N] = S[(b + i) % N];
            }
            else
            {
                fDist[(b + i) % N] = fDist[(b + i + N - 1) % N] + 1;
                ff[(b + i) % N] = ff[(b + i + N - 1) % N];
            }

            if (f[(b + N - i) % N])
            {
                bDist[(b + N - i) % N] = 0;
                bb[(b + N - i) % N] = S[(b + N - i) % N];
            }
            else
            {
                bDist[(b + N - i) % N] = bDist[(b + N - i + 1) % N] + 1;
                bb[(b + N - i) % N] = bb[(b + N - i + 1) % N];
            }
        }

        char[] ans = new char[N];
        for (int i = 0; i < N; i++)
        {
            if (fDist[i] <= bDist[i] && fDist[i] <= K) ans[i] = ff[i];
            else if (bDist[i] < fDist[i] && bDist[i] <= K) ans[i] = bb[i];
            else if (S[i] == 'W') ans[i] = K % 2 == 0 ? 'W' : 'B';
            else ans[i] = K % 2 == 0 ? 'B' : 'W';
        }

        Console.WriteLine(new string(ans));
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