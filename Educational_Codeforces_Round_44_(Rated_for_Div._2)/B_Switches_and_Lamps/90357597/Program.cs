using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[][] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.Next().Select(c => c - '0').ToArray();
        }

        // n個スイッチ
        // m個ランプ

        // iを押すと a_i_j = 1なランプが点く
        // 全部つけるのに必要ないランプがあるか?

        int[] cnt = new int[M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (A[i][j] == 1) cnt[j]++;
            }
        }

        for (int i = 0; i < N; i++)
        {
            bool f = true;
            for (int j = 0; j < M; j++)
            {
                if(A[i][j] == 1 && cnt[j] == 1)
                {
                    f = false;
                    break;
                }
            }
            if (f)
            {
                Console.WriteLine("YES");
                return;
            }
        }
        Console.WriteLine("NO");
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
