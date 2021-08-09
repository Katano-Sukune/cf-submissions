using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long A, B, C;
    private long X1, Y1, X2, Y2;

    public void Solve()
    {
        var sc = new Scanner();
        A = sc.NextInt();
        B = sc.NextInt();
        C = sc.NextInt();
        X1 = sc.NextInt();
        Y1 = sc.NextInt();
        X2 = sc.NextInt();
        Y2 = sc.NextInt();

        double ans = Math.Abs(X1 - X2) + Math.Abs(Y1 - Y2);

        for (int i = 0; i < 2; i++)
        {
            // スタートから x,y座標合わせる
            double tx1, ty1;
            if (i == 0)
            {
                // x合わせる
                // y変えない
                if (A == 0) continue;
                ty1 = Y1;
                // ax + b*Y1 + c = 0
                // ax = -b*Y1 - c
                tx1 = -(double) (B * Y1 + C) / A;
            }
            else
            {
                if (B == 0) continue;
                tx1 = X1;
                ty1 = -(double) (A * X1 + C) / B;
            }

            for (int j = 0; j < 2; j++)
            {
                double tx2, ty2;
                if (j == 0)
                {
                    if (A == 0) continue;
                    ty2 = Y2;
                    tx2 = -(double) (B * Y2 + C) / A;
                }
                else
                {
                    if (B == 0) continue;
                    tx2 = X2;
                    ty2 = -(double) (A * X2 + C) / B;
                }

                double distA = Math.Abs(X1 - tx1) + Math.Abs(Y1 - ty1);
                double distB = Math.Sqrt((tx1 - tx2) * (tx1 - tx2) + (ty1 - ty2) * (ty1 - ty2));
                double distC = Math.Abs(X2 - tx2) + Math.Abs(Y2 - ty2);
                ans = Math.Min(ans, distA + distB + distC);
            }
        }

        Console.WriteLine($"{ans:F12}");
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