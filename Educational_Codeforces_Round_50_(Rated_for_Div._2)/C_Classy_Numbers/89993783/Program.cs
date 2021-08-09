using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < q; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        long L = sc.NextLong();
        long R = sc.NextLong();

        /*
         * 十進表記で 0以外の桁が3つ以下 classy
         * 
         * [L,R]のうちclassyな整数の個数
         */

        Console.WriteLine(Classy(R) - Classy(L - 1));
    }

    long Classy(long l)
    {
        var str = l.ToString();
        var dp1 = new long[str.Length + 1, 4];
        dp1[0, 0] = 1;
        var dp2 = new long[str.Length + 1, 4];
        dp2[0, 0] = 1;
        for (int i = 0; i < str.Length; i++)
        {
            int di = str[str.Length - i - 1] - '0';
            for (int j = 0; j <= 3; j++)
            {
                // 0
                dp2[i + 1, j] += dp2[i, j];
                // 1~9
                if (j + 1 <= 3) dp2[i + 1, j + 1] += dp2[i, j] * 9;


                if (di == 0)
                {
                    // 0
                    dp1[i + 1, j] += dp1[i, j];
                }
                else
                {
                    // 0
                    dp1[i + 1, j] += dp2[i, j];


                    if (j + 1 <= 3)
                    {
                        // 1~di-1
                        dp1[i + 1, j + 1] += dp2[i, j] * (di - 1);

                        // di
                        dp1[i + 1, j + 1] += dp1[i, j];
                    }
                }
            }
        }

        return dp1[str.Length, 0] + dp1[str.Length, 1] + dp1[str.Length, 2] + dp1[str.Length, 3];
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
