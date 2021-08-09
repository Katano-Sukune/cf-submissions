using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int M, N;
    int[] K;
    int[][] S;
    public void Solve()
    {
        var sc = new Scanner();
        M = sc.NextInt();
        N = sc.NextInt();

        K = new int[M];
        S = new int[M][];
        for (int i = 0; i < M; i++)
        {
            K[i] = sc.NextInt();
            S[i] = new int[K[i]];
            for (int j = 0; j < K[i]; j++)
            {
                S[i][j] = sc.NextInt() - 1;
            }
        }

        var f = new bool[M, N];
        for (int i = 0; i < M; i++)
        {
            foreach (var j in S[i])
            {
                f[i, j] = true;
            }
        }

        /*
         * 1~nの店
         * 
         * 店iではa_iを売っている
         * 
         * m日間 dora いくつかの店で整数を1つずつ買う
         * 
         * 同じ日に swiper doraが買わなかった店から整数を買う
         * 
         * i日目にdora 買った数のLCM > swiper のLCM なら勝ち
         * 
         * a_iがわからない
         * 
         * m日間doraが買った店があたえられる
         * m日間毎日勝っていた
         * 
         * aが存在するか?
         */

        for (int i = 0; i < M; i++)
        {
            for (int j = i + 1; j < M; j++)
            {
                bool flag = false;
                // i日、j日に共通で行く店がある
                foreach (var k in S[i])
                {
                    if (f[j, k])
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    Console.WriteLine("impossible");
                    return;
                }
            }
        }
        Console.WriteLine("possible");
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
