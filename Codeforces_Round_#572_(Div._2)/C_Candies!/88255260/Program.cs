using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] s = sc.IntArray();
        int q = sc.NextInt();
        int[] l = new int[q];
        int[] r = new int[q];
        for (int i = 0; i < q; i++)
        {
            l[i] = sc.NextInt() - 1;
            r[i] = sc.NextInt();
        }

        long[] sum = new long[n + 1];
        for (int i = 0; i < n; i++)
        {
            sum[i + 1] = sum[i] + s[i];
        }

        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            long t = sum[r[i]] - sum[l[i]];
            sb.AppendLine((t / 10).ToString());
        }

        Console.Write(sb);
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