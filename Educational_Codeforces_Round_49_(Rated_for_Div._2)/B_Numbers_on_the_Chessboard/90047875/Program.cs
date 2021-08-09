using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    long N, Q;
    int[] X, Y;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        X = new int[Q];
        Y = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
        }

        /*
         * n*nチェス盤
         * 
         * ceil(n^2/2) まではx+y が偶数のいちに以降は奇数の位置に書き込まれる
         * 
         * (x_i,y_i)に書かれた数
         */

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < Q; i++)
        {
            long s = X[i] + Y[i];
            long t = X[i] * N + Y[i];
            if (s % 2 == 0)
            {
                Console.WriteLine(t / 2 + 1);
            }
            else
            {
                var u = t / 2;
                var v = (N * N+1) / 2;
                Console.WriteLine(u+v+1);
            }
        }

        Console.Out.Flush();

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
