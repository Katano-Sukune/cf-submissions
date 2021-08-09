using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        string S = sc.Next();
        int cntR = 0;
        int cntB = 0;
        char[] ans = new char[N];
        for (int i = 0; i < N; i++)
        {
            if (S[i] == '(')
            {
                if (cntR <= cntB)
                {
                    cntR++;
                    ans[i] = '0';
                }
                else
                {
                    cntB++;
                    ans[i] = '1';
                }
            }
            else
            {
                if (cntR >= cntB)
                {
                    cntR--;
                    ans[i] = '0';
                }
                else
                {
                    cntB--;
                    ans[i] = '1';
                }
            }
        }

        Console.WriteLine(new string(ans));
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
