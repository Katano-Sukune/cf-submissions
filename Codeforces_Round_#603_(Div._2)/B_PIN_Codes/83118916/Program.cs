using System;
using System.Linq;
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
            int n = sc.NextInt();
            string[] p = new string[n];
            for (int j = 0; j < n; j++)
            {
                p[j] = sc.Next();
            }
            sb.AppendLine(Q(n, p));
        }

        Console.Write(sb);
    }

    string Q(int n, string[] p)
    {
        int cnt = 0;
        char[][] ans = new char[n][];

        for (int i = 0; i < n; i++)
        {
            bool flag = false;
            for (int j = 0; j < i; j++)
            {
                bool f = true;
                for (int k = 0; k < 4; k++)
                {
                    f &= p[i][k] == ans[j][k];
                }
                flag |= f;
            }
            ans[i] = p[i].ToCharArray();
            if (flag)
            {
                bool[] f = new bool[10];
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    if (j < i) f[ans[j][0] - '0'] = true;
                    if (j > i) f[p[j][0] - '0'] = true;
                }

                for (int k = 0; k < 10; k++)
                {
                    if (!f[k])
                    {
                        ans[i][0] = (char)(k + '0');
                        break;
                    }

                }
                cnt++;
            }
        }

        return $"{cnt}\n{string.Join("\n", ans.Select(ar => new string(ar)))}";
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
