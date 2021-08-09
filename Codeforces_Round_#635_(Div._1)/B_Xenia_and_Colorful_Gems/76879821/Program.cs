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
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextInt(), sc.LongArray(), sc.LongArray(), sc.LongArray()));
        }

        Console.Write(sb);
    }

    string Q(int nr, int ng, int nb, long[] r, long[] g, long[] b)
    {
        Array.Sort(r);
        Array.Sort(g);
        Array.Sort(b);
        long ans = long.MaxValue;

        foreach (long i in r)
        {
            #region g <= r <= b

            {
                int gOk = -1;
                int gNg = ng;
                while (gNg - gOk > 1)
                {
                    int mid = (gNg + gOk) / 2;
                    if (g[mid] <= i) gOk = mid;
                    else gNg = mid;
                }

                int bOk = nb;
                int bNg = -1;
                while (bOk - bNg > 1)
                {
                    int mid = (bNg + bOk) / 2;
                    if (b[mid] >= i) bOk = mid;
                    else bNg = mid;
                }

                if (gOk >= 0 && bOk < nb)
                {
                    ans = Math.Min(ans,
                        (i - g[gOk]) * (i - g[gOk]) + (i - b[bOk]) * (i - b[bOk]) +
                        (g[gOk] - b[bOk]) * (g[gOk] - b[bOk]));
                }
            }

            #endregion


            #region b <= r <= g

            {
                int gOk = ng;
                int gNg = -1;
                while (gOk - gNg > 1)
                {
                    int mid = (gNg + gOk) / 2;
                    if (g[mid] >= i) gOk = mid;
                    else gNg = mid;
                }

                int bOk = -1;
                int bNg = nb;
                while (bNg - bOk > 1)
                {
                    int mid = (bNg + bOk) / 2;
                    if (b[mid] <= i) bOk = mid;
                    else bNg = mid;
                }

                if (gOk < ng && bOk >= 0)
                {
                    ans = Math.Min(ans,
                        (i - g[gOk]) * (i - g[gOk]) + (i - b[bOk]) * (i - b[bOk]) +
                        (g[gOk] - b[bOk]) * (g[gOk] - b[bOk]));
                }
            }

            #endregion
        }

        foreach (long i in g)
        {
            #region r <= g <= b

            {
                int rOk = -1;
                int rNg = nr;
                while (rNg - rOk > 1)
                {
                    int mid = (rNg + rOk) / 2;
                    if (r[mid] <= i) rOk = mid;
                    else rNg = mid;
                }

                int bOk = nb;
                int bNg = -1;
                while (bOk - bNg > 1)
                {
                    int mid = (bNg + bOk) / 2;
                    if (b[mid] >= i) bOk = mid;
                    else bNg = mid;
                }

                if (rOk >= 0 && bOk < nb)
                {
                    ans = Math.Min(ans,
                        (i - r[rOk]) * (i - r[rOk]) + (i - b[bOk]) * (i - b[bOk]) +
                        (r[rOk] - b[bOk]) * (r[rOk] - b[bOk]));
                }
            }

            #endregion


            #region b <= g <= r

            {
                int rOk = nr;
                int rNg = -1;
                while (rOk - rNg > 1)
                {
                    int mid = (rNg + rOk) / 2;
                    if (r[mid] >= i) rOk = mid;
                    else rNg = mid;
                }

                int bOk = -1;
                int bNg = nb;
                while (bNg - bOk > 1)
                {
                    int mid = (bNg + bOk) / 2;
                    if (b[mid] <= i) bOk = mid;
                    else bNg = mid;
                }

                if (rOk < nr && bOk >= 0)
                {
                    ans = Math.Min(ans,
                        (i - r[rOk]) * (i - r[rOk]) + (i - b[bOk]) * (i - b[bOk]) +
                        (r[rOk] - b[bOk]) * (r[rOk] - b[bOk]));
                }
            }

            #endregion
        }

        foreach (long i in b)
        {
            #region g <= b <= r

            {
                int gOk = -1;
                int gNg = ng;
                while (gNg - gOk > 1)
                {
                    int mid = (gNg + gOk) / 2;
                    if (g[mid] <= i) gOk = mid;
                    else gNg = mid;
                }

                int rOk = nr;
                int rNg = -1;
                while (rOk - rNg > 1)
                {
                    int mid = (rNg + rOk) / 2;
                    if (r[mid] >= i) rOk = mid;
                    else rNg = mid;
                }

                if (gOk >= 0 && rOk < nr)
                {
                    ans = Math.Min(ans,
                        (i - g[gOk]) * (i - g[gOk]) + (i -r[rOk]) * (i - r[rOk]) +
                        (g[gOk] - r[rOk]) * (g[gOk] - r[rOk]));
                }
            }

            #endregion


            #region r <= b <= g

            {
                int gOk = ng;
                int gNg = -1;
                while (gOk - gNg > 1)
                {
                    int mid = (gNg + gOk) / 2;
                    if (g[mid] >= i) gOk = mid;
                    else gNg = mid;
                }

                int rOk = -1;
                int rNg = nr;
                while (rNg - rOk > 1)
                {
                    int mid = (rNg + rOk) / 2;
                    if (r[mid] <= i) rOk = mid;
                    else rNg = mid;
                }

                if (gOk < ng && rOk >= 0)
                {
                    ans = Math.Min(ans,
                        (i - g[gOk]) * (i - g[gOk]) + (i - r[rOk]) * (i - r[rOk]) +
                        (g[gOk] - r[rOk]) * (g[gOk] - r[rOk]));
                }
            }

            #endregion
        }

        return ans.ToString();
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