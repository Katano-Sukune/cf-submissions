using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int[] D;
    private const int MaxA = 10000000;

    public void Solve()
    {
        D = new int[MaxA + 1];
        for (int i = 2; i <= MaxA; i++)
        {
            if (D[i] != 0) continue;
            for (int j = i; j <= MaxA; j += i)
            {
                D[j] = i;
            }
        }

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
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();


        int ans = 0;
        var hs = new HashSet<List<int>>(new Cmp());

        foreach (int i in a)
        {
            int tmp = i;
            var ls = new List<int>();
            while (tmp > 1)
            {
                int p = D[tmp];
                int c = 0;
                while (tmp % p == 0)
                {
                    c++;
                    tmp /= p;
                }

                if (c % 2 == 1) ls.Add(p);
            }

            if (!hs.Add(ls))
            {
                ans++;
                hs.Clear();
                hs.Add(ls);
            }
        }

        ans++;
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Cmp : EqualityComparer<List<int>>
{
    public override bool Equals(List<int> x, List<int> y)
    {
        if (x.Count != y.Count) return false;
        for (int i = 0; i < x.Count; i++)
        {
            if (x[i] != y[i]) return false;
        }

        return true;
    }

    public override int GetHashCode(List<int> obj)
    {
        const int tmp = 998244353;
        int result = 1;
        foreach (int i in obj)
        {
            result = (int) ((long) result * i % tmp);
        }

        return result;
    }
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