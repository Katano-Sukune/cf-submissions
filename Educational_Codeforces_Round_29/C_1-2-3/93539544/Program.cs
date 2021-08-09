using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    long K;
    int a, b;
    int[,] A, B;
    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextLong();
        a = sc.NextInt() - 1;
        b = sc.NextInt() - 1;
        A = new int[3, 3];
        B = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                A[i, j] = sc.NextInt() - 1;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                B[i, j] = sc.NextInt() - 1;
            }
        }

        // 123じゃんけん
        // 3 勝ち 2負け
        // 2 1
        // 1 3

        // 最初 alice a bob b出す

        // A_i_j
        // 前回 alice i bob b出したとき、alice A出す

        // B_i_j
        // 〃 bobの手

        // K回勝負

        long[,] scoreA = new long[3, 3];
        long[,] scoreB = new long[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if ((j + 1) % 3 == i) scoreA[i, j] = 1;
                if ((i + 1) % 3 == j) scoreB[i, j] = 1;
            }
        }

        long alice = 0;
        long bob = 0;
        while (K > 0)
        {
            if (K % 2 == 1)
            {
                alice += scoreA[a, b];
                bob += scoreB[a, b];

                (a, b) = (A[a, b], B[a, b]);
            }

            var nextScoreA = new long[3, 3];
            var nextScoreB = new long[3, 3];
            var nextA = new int[3, 3];
            var nextB = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int nA = A[i, j];
                    int nB = B[i, j];
                    nextScoreA[i, j] = scoreA[i, j] + scoreA[nA, nB];
                    nextScoreB[i, j] = scoreB[i, j] + scoreB[nA, nB];
                    nextA[i, j] = A[nA, nB];
                    nextB[i, j] = B[nA, nB];
                }
            }

            scoreA = nextScoreA;
            scoreB = nextScoreB;
            A = nextA;
            B = nextB;
            K /= 2;
        }

        Console.WriteLine($"{alice} {bob}");
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
