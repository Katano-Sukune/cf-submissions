using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string a = sc.Next();
        List<int> l = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (a[i] == '0') continue;
            l.Add(a[i] - '0');
        }

        int sum = 0;
        foreach (int i in l)
        {
            sum += i;
        }

        if (sum == 0)
        {
            Console.WriteLine("YES");
            return;
        }

        var div = new List<int>();
        for (int i = 1; i * i <= sum; i++)
        {
            if (sum % i == 0)
            {
                div.Add(i);
                if (sum / i != i) div.Add(sum / i);
            }
        }

        int[] t = new int[l.Count + 1];
        for (int i = 0; i < l.Count; i++) t[i + 1] = t[i] + l[i];
        foreach (int d in div)
        {
            if (sum / d == 1) continue;
            int cnt = 0;
            for (int i = 1; i <= l.Count; i++)
            {
                if (t[i] % d == 0) cnt++;
            }

            if (cnt == sum / d)
            {
                Console.WriteLine("YES");
                return;
            }
        }

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