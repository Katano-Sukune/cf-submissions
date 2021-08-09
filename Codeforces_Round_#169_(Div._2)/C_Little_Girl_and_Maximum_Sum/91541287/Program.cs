using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int q = sc.NextInt();
        int[] a = sc.IntArray();
        int[] l = new int[q];
        int[] r = new int[q];
        for (int i = 0; i < q; i++)
        {
            l[i] = sc.NextInt() - 1;
            r[i] = sc.NextInt();
        }

        var imos = new int[n + 1];
        for (int i = 0; i < q; i++)
        {
            imos[l[i]]++;
            imos[r[i]]--;
        }

        for (int i = 0; i < n; i++)
        {
            imos[i + 1] += imos[i];
        }

        var sorted = new int[n];
        for (int i = 0; i < n; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (x, y) => imos[y].CompareTo(imos[x]));


        Array.Sort(a, (x, y) => y.CompareTo(x));

        long ans = 0;
        for (int i = 0; i < n; i++)
        {
            ans += (long)a[i] * imos[sorted[i]];
        }
        Console.WriteLine(ans);

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
