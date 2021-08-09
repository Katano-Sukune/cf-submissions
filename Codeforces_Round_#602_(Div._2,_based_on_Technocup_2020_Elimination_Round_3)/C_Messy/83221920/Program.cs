using System;
using System.IO;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});

        for (int i = 0; i < t; i++)
        {
            Console.Write(Q(sc.NextInt(), sc.NextInt(), sc.Next()));
        }

        Console.Out.Flush();
    }

    string Q(int n, int k, string s)
    {
        // sの部分文字列を反転させる

        // sをk個の2以上括弧列連結にする

        // n回以下

        // k-1回 ()作る 残り (((...)))

        char[] t = s.ToCharArray();
        int m = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < k - 1; i++)
        {
            int ii = i * 2;
            // i*2に(を持ってくる
            if (t[ii] != '(')
            {
                for (int j = ii + 1; j < n; j++)
                {
                    if (t[j] == '(')
                    {
                        m++;
                        sb.AppendLine($"{ii + 1} {j + 1}");
                        Reverse(t, ii, j);
                        break;
                    }
                }
            }

            int jj = i * 2 + 1;

            if (t[jj] != ')')
            {
                for (int j = jj + 1; j < n; j++)
                {
                    if (t[j] == ')')
                    {
                        m++;
                        sb.AppendLine($"{jj + 1} {j + 1}");
                        Reverse(t, jj, j);
                        break;
                    }
                }
            }
        }

        // [(k-1)*2, n)を ((()))にする

        int len = n - (k - 1) * 2;

        for (int i = 0; i < len / 2; i++)
        {
            // (
            int ii = (k - 1) * 2 + i;
            if (t[ii] != '(')
            {
                for (int j = ii + 1; j < n; j++)
                {
                    if (t[j] == '(')
                    {
                        m++;
                        sb.AppendLine($"{ii + 1} {j + 1}");
                        Reverse(t, ii, j);
                        break;
                    }
                }
            }
        }

        return $"{m}\n{sb}";
    }

    void Reverse(char[] c, int l, int r)
    {
        int len = r - l + 1;
        for (int i = 0; i < len / 2; i++)
        {
            var tmp = c[l + i];
            c[l + i] = c[r - i];
            c[r - i] = tmp;
        }
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