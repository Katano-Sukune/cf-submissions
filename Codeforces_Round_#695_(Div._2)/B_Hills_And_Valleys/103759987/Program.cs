using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        int h = 0;
        int v = 0;

        for (int i = 1; i < n - 1; i++)
        {
            if (a[i - 1] < a[i] && a[i] > a[i + 1]) h++;
            if (a[i - 1] > a[i] && a[i] < a[i + 1]) v++;
        }

        int ans = h + v;
        // iを変える
        for (int i = 0; i < n; i++)
        {
            if (i + 1 < n)
            {
                int tmp = h + v;
                // i+1に揃える
                if (i + 2 < n)
                {
                    if (F(a[i], a[i + 1], a[i + 2])) tmp--;

                }
                if (i - 1 >= 0)
                {
                    if (F(a[i - 1], a[i], a[i + 1])) tmp--;
                }
                if (i - 2 >= 0)
                {
                    if (F(a[i - 2], a[i - 1], a[i]) && !F(a[i - 2], a[i - 1], a[i + 1]))
                    {
                        // if (i == 2) Console.WriteLine("aaa");
                        tmp--;
                    }
                    if (!F(a[i - 2], a[i - 1], a[i]) && F(a[i - 2], a[i - 1], a[i + 1]))
                    {
                        // if (i == 2) Console.WriteLine("aaa");
                        tmp++;
                    }
                }

                //                 Console.WriteLine($"{i} {tmp}");
                ans = Math.Min(ans, tmp);
            }

            if (i - 1 >= 0)
            {
                int tmp = h + v;
                // i-1に揃える
                if (i - 2 >= 0)
                {
                    if (F(a[i - 2], a[i - 1], a[i])) tmp--;
                }
                if (i + 1 < n)
                {
                    if (F(a[i - 1], a[i], a[i + 1])) tmp--;
                }
                if (i + 2 < n)
                {
                    if (F(a[i], a[i + 1], a[i + 2]) && !F(a[i - 1], a[i + 1], a[i + 2])) tmp--;
                    if (!F(a[i], a[i + 1], a[i + 2]) && F(a[i - 1], a[i + 1], a[i + 2])) tmp++;
                }


                ans = Math.Min(ans, tmp);
            }
        }

        Console.WriteLine(ans);
    }

    bool F(int p, int c, int n)
    {
        if (p < c && c > n) return true;
        if (p > c && c < n) return true;
        return false;
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
