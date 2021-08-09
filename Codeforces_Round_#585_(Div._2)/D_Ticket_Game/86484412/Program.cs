using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private string S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();

        // 先手 Monocarp
        // 後手 Bicarp

        // Sの内の?を0~9で置き換える

        // 先頭n/2個の総和 = 後半 n/2の総和なら後手勝ち
        // 前半 - 後半

        // 先手のmin,maxを後手が相殺できるか?

        int f = 0;
        int b = 0;
        int d = 0;
        for (int i = 0; i < N / 2; i++)
        {
            if (S[i] == '?') f++;
            else d += S[i] - '0';

            if (S[i + N / 2] == '?') b++;
            else d -= S[i + N / 2] - '0';
        }

        for (int i = 0; i <= (f + b) / 2; i++)
        {
            if (f < i) continue;
            int j = (f + b) / 2 - i;
            if (b < j) continue;

            int min = d - 9 * j;
            int max = d + 9 * i;

            int ii = f - i;
            int jj = b - j;


            if (jj * (-9) <= -max && -min <= ii * 9)
            {
            }
            else
            {
                // Console.WriteLine($"{i} {j} {ii} {jj} {min} {max}");
                Console.WriteLine("Monocarp");
                return;
            }
        }

        Console.WriteLine("Bicarp");
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