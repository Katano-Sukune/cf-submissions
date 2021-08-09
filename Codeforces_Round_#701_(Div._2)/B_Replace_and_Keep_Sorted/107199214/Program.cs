using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int q = sc.NextInt();
        int k = sc.NextInt();

        int[] a = sc.IntArray();

#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif

        long[] t = new long[n];
        for (int i = 1; i < n - 1; i++)
        {
            t[i] = (a[i + 1] - 1) - (a[i - 1] + 1) ;
        }

        long[] sum = new long[n + 1];
        for (int i = 0; i < n; i++)
        {
            sum[i + 1] = sum[i] + t[i];
        }

        for (int i = 0; i < q; i++)
        {
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();

            int len = r - l;


            if (len == 1)
            {
                Console.WriteLine(k - 1);
                continue;
            }

            long ans = sum[r - 1] - sum[l + 1];
            // Console.WriteLine(ans);
            ans += a[l + 1] - 2;
            ans += k - a[r - 2] - 1;
            Console.WriteLine(ans);
            
            // 2 + 2 + 1 + 3
        }

        Console.Out.Flush();
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