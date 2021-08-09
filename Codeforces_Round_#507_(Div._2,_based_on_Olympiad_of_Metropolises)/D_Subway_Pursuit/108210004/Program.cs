using System;
using System.Linq;
using System.Threading;

public class Program
{
    private long N, K;

#if DEBUG
    private long X;
    private Random R;
#endif
    public void Solve()
    {
#if DEBUG
        N = (long) 100;
        K = 0;
        X = (long) 10;
        R = new Random();
#else
        var ln = Console.ReadLine().Split(' ');
        N = long.Parse(ln[0]);
        K = int.Parse(ln[1]);
#endif
        // 線路
        // N個駅

        // l r
        // 駅[l,r]に電車があるか?

        // 電車クエリごとに+-Kまで移動できる

        // [l,r)にいる
        long l = 1;
        long r = N + 1;
        var rnd = new Random();
        while (true)
        {
            while (r - l > 4 * K + 1)
            {
                long m = (l + r) / 2;
                bool q = Q(l, m - 1);
                if (l == m - 1 && q) return;
                if (q)
                {
                    l = Math.Max(1, l - K);
                    r = Math.Min(N + 1, m + K);
                }
                else
                {
                    l = Math.Max(1, m - K);
                    r = Math.Min(N + 1, r + K);
                }
            }

            int len = (int) (r - l);
            int tmp = rnd.Next(len);
            if (Q(l + tmp, l + tmp))
            {
                return;
            }
            else
            {
                l = Math.Max(1, l - K);
                r = Math.Min(N + 1, r + K);
            }
        }
    }

    bool Q(long l, long r)
    {
#if DEBUG
        bool result = l <= X && X <= r;
        long ll = Math.Max(1, X - K);
        long rr = Math.Min(N, X + K);
        int len = (int) (rr - ll + 1);
        X = ll + R.Next(len);

        Console.WriteLine($"{l} {r} {(result ? "Yes" : "No")} {X}");
        return result;
#else
        Console.WriteLine($"{l} {r}");
        return Console.ReadLine() == "Yes";
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}