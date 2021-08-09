using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    const int Inf = int.MaxValue / 2;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        // 最初 カウンタ 0
        // カウンタ下1桁を末尾に追加 x or yを加算 
        // 10*10 行列
        var ans = new int[10][];
        for (int i = 0; i < 10; i++)
        {
            ans[i] = new int[10];
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = i; j < 10; j++)
            {
                // i -> jにいくつかかるか?
                var dist = new int[10, 10];
                for (int k = 0; k < 10; k++)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        dist[k, l] = Inf;
                    }
                }
                for (int k = 0; k < 10; k++)
                {
                    dist[k, (k + i) % 10] = 1;
                    dist[k, (k + j) % 10] = 1;
                }
                for (int k = 0; k < 10; k++)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            dist[l, m] = Math.Min(dist[l, m], dist[l, k] + dist[k, m]);
                        }
                    }
                }
                bool f = true;
                int t = 0;
                for (int k = 0; k < S.Length - 1; k++)
                {
                    int cur = S[k] - '0';
                    int next = S[k + 1] - '0';
                    if (dist[cur, next] == Inf)
                    {
                        f = false;
                        break;
                    }
                    t += dist[cur, next] -1;
                }

                ans[i][j] = ans[j][i] = (f ? t : -1);
            }
        }

        Console.WriteLine(string.Join("\n", ans.Select(l => string.Join(" ", l))));
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
