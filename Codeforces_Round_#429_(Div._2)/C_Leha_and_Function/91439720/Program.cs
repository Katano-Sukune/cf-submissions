using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int m = sc.NextInt();
        int[] a = sc.IntArray();
        int[] b = sc.IntArray();

        /*
         * F(n,k)
         * [1,2,3,4....n]からk個選ぶ
         * 
         * 最小値の期待値
         * 
         * 配列A,B
         * A,B
         * 
         * Aを並び替えて
         * F(A_i,B_i)の総和が最大
         * 
         * A
         */

        var sortedB = new int[m];
        for (int i = 0; i < m; i++)
        {
            sortedB[i] = i;
        }

        Array.Sort(sortedB, (l, r) => b[r].CompareTo(b[l]));

        Array.Sort(a);

        var ans = new int[m];
        for (int i = 0; i < m; i++)
        {
            ans[sortedB[i]] = a[i];
        }

        Console.WriteLine(string.Join(" ", ans));
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
