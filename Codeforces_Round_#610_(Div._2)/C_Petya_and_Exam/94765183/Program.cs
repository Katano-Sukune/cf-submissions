using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
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
        int T = sc.NextInt();
        int a = sc.NextInt();
        int b = sc.NextInt();

        int[] diff = sc.IntArray();

        int[] t = sc.IntArray();

        /*
         * 制限時間t分
         *
         * 簡単 a分
         *
         * 難しい b分
         *
         * 時間sで退出
         *
         * t[i] <= sな問題が解けてないなら0点
         * 
         * 
         * 
         */
        int[] cnt = new int[2];
        (int diff, int t)[] p = new (int diff, int t)[n + 1];
        for (int i = 0; i < n; i++)
        {
            p[i] = (diff[i], t[i]);
            cnt[diff[i]]++;
        }

        p[n] = (0, T + 1);

        Array.Sort(p, (l, r) => l.t.CompareTo(r.t));

        long ans = 0;

        // i問必須
        // 時間最大 p[i].t

        long time = 0;
        for (int i = 0; i <= n; i++)
        {
            if (time < p[i].t)
            {
                long score = 0;
                score += i;

                long tmp = p[i].t - 1 - time;

                long cntA = Math.Min(cnt[0], tmp / a);
                score += cntA;
                tmp -= a * cntA;

                long cntB = Math.Min(cnt[1], tmp / b);
                score += cntB;

                ans = Math.Max(ans, score);
            }

            time += p[i].diff == 0 ? a : b;
            cnt[p[i].diff]--;
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