using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int l = sc.NextInt();
        int r = sc.NextInt();

        /*
         * l <= a,b <= r
         *
         * a + b = a xor bを満たすa,bはいくつあるか?
         * 
         */


        Console.WriteLine(Calc(r, r) - Calc(r, l - 1) * 2 + Calc(l - 1, l - 1));
    }

    long Calc(int maxA, int maxB)
    {
        // i桁目まで見る、aが超えたか? bが超えたか?
        var dp = new long[32, 2, 2];
        dp[0, 0, 0] = 1;
        for (int i = 1; i <= 31; i++)
        {
            int ba = maxA % 2;
            int bb = maxB % 2;
            for (int ca = 0; ca < 2; ca++)
            {
                for (int cb = 0; cb < 2; cb++)
                {
                    if (ca == 1 && cb == 1) continue;
                    for (int pa = 0; pa < 2; pa++)
                    {
                        for (int pb = 0; pb < 2; pb++)
                        {
                            int na = ca < ba ? 0 : (ca == ba ? pa : 1);
                            int nb = cb < bb ? 0 : (cb == bb ? pb : 1);
                            dp[i, na, nb] += dp[i - 1, pa, pb];
                        }
                    }
                }
            }


            maxA /= 2;
            maxB /= 2;
        }

        return dp[31, 0, 0];
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