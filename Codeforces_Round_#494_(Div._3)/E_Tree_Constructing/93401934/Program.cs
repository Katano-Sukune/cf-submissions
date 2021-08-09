using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int d = sc.NextInt();
        int k = sc.NextInt();

        /*
         * n頂点、直径d
         * 次数最大k
         */

        /*
         * 
         */

        // 0 ~ d直径

        // d+1~n-1 他

        if (n < d + 1)
        {
            Console.WriteLine("NO");
            return;
        }


        // 番号、一番遠い頂点までの距離
        var q = new Queue<int>();

        int[] cnt = new int[n];
        int[] dist = new int[n];
        var ans = new List<(int s, int t)>();
        for (int i = 0; i < d; i++)
        {
            cnt[i]++;
            cnt[i + 1]++;
            ans.Add((i, i + 1));
        }
        for (int i = 0; i <= d; i++)
        {
            dist[i] = Math.Max(i, d - i);
            if (dist[i] < d && cnt[i] < k)
            {
                q.Enqueue(i);
            }
        }

        for (int i = d + 1; i < n; i++)
        {
            if (q.Count == 0)
            {
                Console.WriteLine("NO");
                return;
            }
            var dq = q.Dequeue();
            ans.Add((dq, i));

            dist[i] = dist[dq] + 1;
            cnt[i] = 1;
            cnt[dq]++;
            if (dist[i] < d && cnt[i] < k)
            {
                q.Enqueue(i);
            }
            if (cnt[dq] < k)
            {
                q.Enqueue(dq);
            }
        }

        if(cnt.Max() > k)
        {
            Console.WriteLine("NO");
            return;
        }

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        Console.WriteLine("YES");
        foreach (var e in ans)
        {
            Console.WriteLine($"{e.s + 1} {e.t + 1}");
        }
        Console.Out.Flush();

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
