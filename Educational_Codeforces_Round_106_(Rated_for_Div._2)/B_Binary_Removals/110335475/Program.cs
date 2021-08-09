using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        string s = sc.Next();
        int n = s.Length;

        bool[] f = new bool[n + 1];
        f[0] = true;
        for (int i = 0; i < n; i++)
        {
            if (s[i] == '0')
            {
                f[i] = true;
            }
            else
            {
                if (i == 0 || s[i - 1] == '0')
                {
                    f[i] = true;
                }
                else
                {
                    break;
                }
            }
        }

        bool[] b = new bool[n + 1];
        b[n] = true;
        for (int i = n - 1; i >= 0; i--)
        {
            if (s[i] == '1')
            {
                b[i] = true;
            }
            else
            {
                if (i == n - 1 || s[i + 1] == '1')
                {
                    b[i] = true;
                }
                else
                {
                    break;
                }
            }
        }

        for (int i = 0; i <= n; i++)
        {
            if (f[i] && b[i])
            {
                Console.WriteLine("YES");
                return;
            }
        }

        // Console.WriteLine(string.Join(" ", f.Select(bo => bo ? 1 : 0)));
        // Console.WriteLine(string.Join(" ", b.Select(bo => bo ? 1 : 0)));
        Console.WriteLine("NO");
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