using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string c = sc.Next();
        string a = sc.Next();

        var ls = new List<int>[4];
        for (int i = 0; i < 4; i++)
        {
            ls[i] = new List<int>();
        }
        for (int i = 0; i < n; i++)
        {
            if (c[i] == '1')
            {
                if (a[i] == '1')
                {
                    ls[3].Add(i);
                }
                else
                {
                    ls[1].Add(i);
                }
            }
            else
            {
                if (a[i] == '1')
                {
                    ls[2].Add(i);
                }
                else
                {
                    ls[0].Add(i);
                }
            }
        }

        // 両方できる人1回にi人
        for (int i = 0; i <= ls[3].Count; i++)
        {
            // cだけの人1にj人
            for (int j = 0; j <= ls[1].Count; j++)
            {

                // aだけの人2にk人
                int k = (i + j) - (ls[3].Count - i);
                if (k < 0) continue;
                if (k > ls[2].Count) continue;

                int l = ls[2].Count - k;

                // どっちもできない人1にm人
                int m = n / 2 - (i + j + l);
                if (m < 0 || m > ls[0].Count) continue;
                // Console.WriteLine($"{i} {j} {k}");
                var ans = new List<int>(n + 1);
                for (int t = 0; t < i; t++)
                {
                    ans.Add(ls[3][t] + 1);
                }
                for (int t = 0; t < j; t++)
                {
                    ans.Add(ls[1][t] + 1);
                }
                for (int t = 0; t < l; t++)
                {
                    ans.Add(ls[2][t] + 1);
                }
                for (int t = 0; t < m; t++)
                {
                    ans.Add(ls[0][t] + 1);
                }
                ans.Sort();
                Console.WriteLine(string.Join(" ", ans));
                return;
            }
        }
        Console.WriteLine("-1");
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
