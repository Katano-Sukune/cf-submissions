using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

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
        long x = sc.NextLong();
        long y = sc.NextLong();
        long[] c = sc.LongArray();
        int[] dx = new int[] {1, 0, -1, -1, 0, 1};
        int[] dy = new int[] {1, 1, 0, -1, -1, 0};
        /*
         * 高さ合わせる
         * 
         */
        long ans = long.MaxValue;

        for (int i = 0; i < 6; i++)
        {
            for (int j = i + 1; j < 6; j++)
            {
                if (i + 3 == j) continue;

                // dx[i] * a + dx[j] * b = x
                // dy[i] * a + dy[j] * b = y

                if (dx[i] == 0)
                {
                    if (x == 0 || Math.Sign(x) == Math.Sign(dx[j]))
                    {
                        long b = Math.Abs(x);
                        long ty = y - dy[j] * b;
                        if (ty == 0 || Math.Sign(ty) == Math.Sign(dy[i]))
                        {
                            long a = Math.Abs(ty);
                            ans = Math.Min(ans, a * c[i] + b * c[j]);
                        }
                    }
                }
                else if (dx[j] == 0)
                {
                    if (x == 0 || Math.Sign(x) == Math.Sign(dx[i]))
                    {
                        long a = Math.Abs(x);
                        long ty = y - dy[i] * a;
                        if (ty == 0 || Math.Sign(ty) == Math.Sign(dy[j]))
                        {
                            long b = Math.Abs(ty);
                            ans = Math.Min(ans, a * c[i] + b * c[j]);
                        }
                    }
                }
                else if (dy[i] == 0)
                {
                    if (y == 0 || Math.Sign(y) == Math.Sign(dy[j]))
                    {
                        long b = Math.Abs(y);
                        long tx = x - dx[j] * b;
                        if (tx == 0 || Math.Sign(tx) == Math.Sign(dx[i]))
                        {
                            long a = Math.Abs(tx);
                            ans = Math.Min(ans, a * c[i] + b * c[j]);
                        }
                    }
                }
                else if (dy[j] == 0)
                {
                    if (y == 0 || Math.Sign(y) == Math.Sign(dy[i]))
                    {
                        long a = Math.Abs(y);
                        long tx = x - dx[i] * a;
                        if (tx == 0 || Math.Sign(tx) == Math.Sign(dx[j]))
                        {
                            long b = Math.Abs(tx);
                            ans = Math.Min(ans, a * c[i] + b * c[j]);
                        }
                    }
                }
            }
        }

        Console.WriteLine(ans);
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