using System;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(),sc.Next()));
        }

        Console.Write(sb.ToString());
    }

    private string Q(int n,string s)
    {
        int ansK = 1;
        char[] ans = s.ToCharArray();
        for (int k = 2; k <= n; k++)
        {
            char[] t = new char[n];
            for (int i = 0; i < n - k + 1; i++)
            {
                t[i] = s[i + k - 1];
            }

            if ((n - k) % 2 == 1)
            {
                int index = 0;
                for (int i = n - k + 1; i < n; i++)
                {
                    t[i] = s[index++];
                }
            }
            else
            {
                int index = k - 2;
                for (int i = n - k + 1; i < n; i++)
                {
                    t[i] = s[index--];
                }
            }

            int comp = 0;
            for (int i = 0; i < n; i++)
            {
                if (ans[i] != t[i])
                {
                    comp = ans[i].CompareTo(t[i]);
                    break;
                }
            }

            if (comp > 0)
            {
                ansK = k;
                ans = t;
            }
        }

        return $"{new string(ans)}\n{ansK}";
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}