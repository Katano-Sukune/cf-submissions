using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private const int MaxN = 5000;
    private const int MaxD = 5000;

    // (cn, cd)[] からの遷移
    // 遷移元が無いならnull
    private (int cn, int cd)[,][] DP;
    private int[] Min, Max;

    public void Solve()
    {
        // [個数、総和] = 作れるか?
        // [0,0] = true
        DP = new (int pn, int pd)[MaxN + 1, MaxD + 1][];
        DP[1, 0] = new (int pn, int pd)[0];
        Min = new int[MaxN + 1];
        Max = new int[MaxN + 1];

        for (int i = 2; i <= MaxN; i++)
        {
            // 片方0
            for (int j = Min[i - 1]; j + i - 1 <= MaxD && j <= Max[i - 1]; j++)
            {
                DP[i, j + i - 1] = new (int cn, int cd)[1] {(i - 1, j)};
            }

            for (int j = 1; j <= (i - 1) / 2; j++)
            {
                int k = i - 1 - j;
                for (int jj = Min[j];
                    jj <= Max[j] && jj + Min[k] + i - 1 <= MaxD && DP[i, jj + Min[k] + i - 1] == null;
                    jj++)
                {
                    for (int kk = Min[k];
                        kk <= Max[k] && jj + kk + i - 1 <= MaxD && DP[i, jj + kk + i - 1] == null;
                        kk++)
                    {
                        DP[i, jj + kk + i - 1] = new (int cn, int cd)[2] {(j, jj), (k, kk)};
                    }
                }
            }


            Max[i] = Max[i - 1] + i - 1;
            Min[i] = Min[(i - 1) / 2] + Min[(i - 1) - (i - 1) / 2] + i - 1;
        }

        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        int d = sc.NextInt();
        // 深さ、個数、総和] = 一番下の葉
        if (d > MaxD)
        {
            Console.WriteLine("NO");
            return;
        }

        if (d < Min[n] || Max[n] < d)
        {
            Console.WriteLine("NO");
            return;
        }

        // 親
        int[] ans = new int[n];
        Go(n, d, 0, ans);

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", ans.Skip(1).Select(num => num+1)));
    }

    void Go(int n, int d, int root, int[] ans)
    {
        var t = DP[n, d];
        if (t.Length == 0)
        {
            return;
        }
        if (t.Length == 1)
        {
            ans[root + 1] = root;
            Go(t[0].cn, t[0].cd, root + 1, ans);
        }
        else
        {
            ans[root + 1] = root;
            ans[root + 1 + t[0].cn] = root;
            Go(t[0].cn, t[0].cd, root + 1, ans);
            Go(t[1].cn, t[1].cd, root + 1 + t[0].cn, ans);
        }
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