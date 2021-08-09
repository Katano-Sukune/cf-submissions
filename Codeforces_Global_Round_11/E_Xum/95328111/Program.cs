using System;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        long n = sc.NextInt();

        int q = 0;
        StringBuilder sb = new StringBuilder();
        if (n % 4 == 3)
        {
            // 下2桁 11
            q++;
            sb.AppendLine($"{n} + {n}");
            q++;
            sb.AppendLine($"{n} ^ {2 * n}");

            n = n ^ (2 * n);
        }

        // 01

        while (n > 1)
        {
            int lg = 0;
            long t = 1;
            while (t * 2 <= n)
            {
                lg++;
                t *= 2;
            }

            long m = n;
            for (int i = 0; i < lg; i++)
            {
                q++;
                sb.AppendLine($"{m} + {m}");
                m *= 2;
            }

            q++;
            sb.AppendLine($"{n} ^ {m}");
            long xor = n ^ m;

            q++;
            sb.AppendLine($"{n} + {m}");
            long plus = n + m;

            q++;
            sb.AppendLine($"{xor} ^ {plus}");
            long o = xor ^ plus;


            for (int i = 0; i < lg; i++)
            {
                if ((xor & o) > 0)
                {
                    q++;
                    sb.AppendLine($"{o} ^ {xor}");
                    xor ^= o;
                }

                if (i != lg - 1)
                {
                    q++;
                    sb.AppendLine($"{o} + {o}");
                    o *= 2;
                }
            }

            n = xor;
        }

        Console.WriteLine(q);
        Console.Write(sb);
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