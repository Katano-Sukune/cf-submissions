using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // N個の数が円周上にならんでいる

        // iを選ぶ
        // i-1, i+1の和に置き換える
        // i-1,i+1を消す
        // 最後に残った値最大

        // 隣接しないfloor(N/2)個最小
        long tmp = 0;
        var ls = new List<int>();
        int l = 0;
        for (int i = 0; i < N / 2; i++)
        {
            ls.Add(l);
            tmp += A[l];
            l += 2;
        }

        long min = tmp;

        for (int i = 0; i < N; i++)
        {
            ls.Add(l);
            tmp += A[l];
            tmp -= A[ls[i]];
            min = Math.Min(min, tmp);
            l += 2;
            l %= N;
        }

        long sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += A[i];
        }

        Console.WriteLine(sum - min);
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