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

        /*
         * a_iを選ぶ
         * a_{i+1} a_{i-1}にa_i加算
         * 
         * a_i削除
         * 
         * 全部同じにする
         */

        long sum = 0;
        foreach (var i in a)
        {
            sum += i;
        }

        long ans = long.MaxValue;

        for (long i = 1; i * i <= sum; i++)
        {
            if (sum % i != 0) continue;
            long j = sum / i;
            // iごと
            {
                long tmp = 0;
                bool f = true;
                for (int k = 0; f && k < n; k++)
                {
                    tmp += a[k];
                    if (tmp == i) tmp = 0;
                    if (tmp > i) f = false;
                }

                if (f) ans = Math.Min(ans, n - j);
            }
            {
                long tmp = 0;
                bool f = true;
                for (int k = 0; f && k < n; k++)
                {
                    tmp += a[k];
                    if (tmp == j) tmp = 0;
                    if (tmp > j) f = false;
                }

                if (f) ans = Math.Min(ans, n - i);
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
