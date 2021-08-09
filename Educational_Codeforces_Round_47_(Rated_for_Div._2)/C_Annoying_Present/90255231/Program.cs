using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] X, D;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        X = new int[M];
        D = new int[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt();
            D[i] = sc.NextInt();
        }

        /*
         * x,dを決める
         * iを選ぶ
         * x + d*|i-j| を A_jに加算
         * 
         * Aの平均最大
         */

        /*
         * 
         * 
         * d > 0 端に置くのが最大
         * 
         * d <= 0 真ん中 最大
         */

        // 端に置いたとき
        long s = 0;

        // まんなか
        long t = 0;
        for (int i = 0; i < N; i++)
        {
            s += i;
            t += Math.Abs(i - N / 2);
        }

        long sum = 0;
        for(int i = 0;i < M; i++)
        {
            if(D[i] > 0)
            {
                sum += D[i] * s;
            
            }
            else
            {
                sum += D[i] * t;
            }
            sum += X[i] * N;
        }

        Console.WriteLine((double) sum / N);
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
