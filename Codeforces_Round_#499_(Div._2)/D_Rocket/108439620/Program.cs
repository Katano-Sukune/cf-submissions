using System;
using System.Linq;
using System.Threading;

public class Program
{
    private int M, N;

    public void Solve()
    {
#if DEBUG
        M = 5;
        N = 2;
#else
        {
            var ln = Console.ReadLine().Split(' ');
            M = int.Parse(ln[0]);
            N = int.Parse(ln[1]);
        }
#endif

        bool[] p = new bool[N];
        for (int i = 0; i < N; i++)
        {
            int q = Q(1);
            if (q == 0) return;
            p[i] = q > 0;
        }

        int ok = 2;
        int ng = M + 1;
        int idx = 0;
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            int q = Q(mid);
            if (q == 0) return;
            if (q >= 0 == p[idx++]) ok = mid;
            else ng = mid;
            idx %= N;
        }

        Q(ok);
    }

#if DEBUG
    private int X = 3;
    private bool[] P = {true, false};
    private int Idx = 0;
#endif
    int Q(int y)
    {
#if DEBUG
        int t = X.CompareTo(y);
        if (!P[Idx++]) t *= -1;
        Idx %= P.Length;
        Console.WriteLine($"{y} {t}");
        return t;
#else
        Console.WriteLine(y);
        return int.Parse(Console.ReadLine());
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}