using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    int M;
    int[] B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        M = sc.NextInt();
        B = sc.IntArray();

        /*
         * 配列A,B
         * 
         * 配列
         * 連続部分列を取って部分列の総和に置き換える
         * 
         * A,Bに操作してA=Bにする
         * 
         * 最大長
         */

        int iA = 0;
        int iB = 0;
        long sumA = 0;
        long sumB = 0;
        int len = 0;
        while (iA < N || iB < M)
        {
            if (iA < N && iB < M && sumA == 0 && sumB == 0)
            {
                sumA += A[iA++];
                sumB += B[iB++];
            }
            else if (iA < N && sumA < sumB)
            {
                sumA += A[iA++];
            }
            else if (iB < M && sumB < sumA)
            {
                sumB += B[iB++];
            }
            else
            {
                Console.WriteLine("-1");
                return;
            }

            if (sumA == sumB)
            {
                len++;
                sumA = 0;
                sumB = 0;
            }
        }

        Console.WriteLine(sumA == 0 && sumB == 0 ? len : -1);
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
