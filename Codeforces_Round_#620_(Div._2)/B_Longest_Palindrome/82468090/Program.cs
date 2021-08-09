using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }
        List<string> ls = new List<string>();
        bool[] f = new bool[n];
        for (int i = 0; i < n; i++)
        {
            if (f[i]) continue;
            for (int j = i + 1; j < n; j++)
            {
                bool flag = true;
                for (int k = 0; k < m; k++)
                {
                    flag &= s[i][k] == s[j][m - 1 - k];
                }

                if (flag)
                {
                    ls.Add(s[i]);
                    f[i] = true;
                    f[j] = true;
                    break;
                }
            }
        }


        string mm = "";
        for (int i = 0; i < n; i++)
        {
            if (f[i]) continue;
            bool flag = true;
            for (int j = 0; j < m / 2; j++)
            {
                flag &= s[i][j] == s[i][m - 1 - j];
            }
            if (flag)
            {
                mm = s[i];
                break;
            }
        }
        var sb = new StringBuilder();
        for (int i = 0; i < ls.Count; i++)
        {
            sb.Append(ls[i]);
        }
        sb.Append(mm);
        for (int i = ls.Count - 1; i >= 0; i--)
        {
            sb.Append(new string(ls[i].Reverse().ToArray()));
        }
        Console.WriteLine(sb.Length);
        Console.WriteLine(sb);

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
