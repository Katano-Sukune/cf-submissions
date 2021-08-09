using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, Q;
    private int[] R, C;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        R = new int[Q];
        C = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            R[i] = sc.NextInt() - 1;
            C[i] = sc.NextInt() - 1;
        }

        /*
         * 2*N迷路
         *
         * 1,1から2,Nに移動
         *
         * iに r_i,c_iの状態が反転
         *
         * 
         */
        /*
         * *.
         * *.
         *
         * *.
         * .*
         *
         * .*
         * *.
         * 
         */


        var f = new bool[2, N];
        int cnt = 0;
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < Q; i++)
        {
            if (f[R[i], C[i]])
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (C[i] + j < 0) continue;
                    if (C[i] + j >= N) continue;
                    if (f[(R[i] + 1) % 2, C[i] + j]) cnt--;
                }
            }
            else
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (C[i] + j < 0) continue;
                    if (C[i] + j >= N) continue;
                    if (f[(R[i] + 1) % 2, C[i] + j]) cnt++;
                }
            }

            f[R[i], C[i]] ^= true;
            Console.WriteLine(cnt == 0?"Yes":"No");
        }


        Console.Out.Flush();
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