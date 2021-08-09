using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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

        // 5個の積最大

        List<int> plus = new List<int>();
        List<int> minus = new List<int>();
        foreach (var i in a)
        {
            if (i >= 0) plus.Add(i);
            else minus.Add(i);
        }

        plus.Sort((l, r) => r.CompareTo(l));
        minus.Sort();

        long ans = long.MinValue;
        // minus偶数個
        for (int m = 0; m <= 5; m += 2)
        {
            int p = 5 - m;
            if (m > minus.Count || p > plus.Count) continue;
            long tmp = 1;
            for (int i = 0; i < m; i++)
            {
                tmp *= minus[i];
            }
            for (int i = 0; i < p; i++)
            {
                tmp *= plus[i];
            }


            ans = Math.Max(ans, tmp);
        }

        // 無い
        // minus奇数個
        for (int m = 1; m <= 5; m += 2)
        {
            int p = 5 - m;
            // Console.WriteLine($"{m} {p}");
            if (m > minus.Count || p > plus.Count) continue;
     
            long tmp = 1;
            for (int i = 0; i < m; i++)
            {
                tmp *= minus[minus.Count - 1 - i];
            }
            for (int i = 0; i < p; i++)
            {
                tmp *= plus[plus.Count - 1 - i];
            }


            ans = Math.Max(ans, tmp);
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
