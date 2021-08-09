using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        /*
        * 2 <= i <= Nついて
        *
        * (i,j)について i,jが互いに素ならa_i != a_j
        *
        * a_iの最大値を最小化
        *
        * aを出力
        */

        int[] ans = new int[N + 1];
        int k = 0;
        for (int i = 2; i <= N; i++)
        {
            if (ans[i] == 0)
            {
                ans[i] = ++k;
                for (int j = i + i; j <= N; j += i)
                {
                    if (ans[j] == 0) ans[j] = k;
                }
            }
        }

        Console.WriteLine(string.Join(" ", ans.Skip(2)));
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