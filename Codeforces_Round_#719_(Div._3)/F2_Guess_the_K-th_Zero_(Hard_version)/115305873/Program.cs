using System;
using System.Linq;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, T;
    public void Solve()
    {
#if DEBUG
        N = 6;
        T = 2;
        K = new int[] { 2, 2 };
#else
        {
            var ln = Console.ReadLine().Split(' ');
            N = int.Parse(ln[0]);
            T = int.Parse(ln[1]);
        }
#endif

        // 01文字列
        // k番目の0の位置をt回当てる

        // ? l r [l,r]の和
        // ! x 回答

        // にぶたん
        int k = int.Parse(Console.ReadLine());

        const int B = 32;
        var ls = new List<(int e, int cnt)>();
        ls.Add((0, 0));
        for (int i = 32; i <= N; i += B)
        {
            ls.Add((i, i - Q(1, i)));
        }

        if (N % B != 0)
        {
            ls.Add((N, N - Q(1, N)));
        }

        for (int i = 0; i < T; i++)
        {
            if (i > 0) k = int.Parse(Console.ReadLine());
            int ok = ls.Count - 1;
            int ng = 0;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (ls[mid].cnt >= k) ok = mid;
                else ng = mid;
            }

            int ok2 = ls[ok].e;
            int ng2 = ls[ng].e;
            while (ok2 - ng2 > 1)
            {
                int mid = (ok2 + ng2) / 2;
                if (mid - Q(1, mid) >= k) ok2 = mid;
                else ng2 = mid;
            }

            A(ok2);
            for (int j = ok; j < ls.Count; j++)
            {
                var tmp = ls[j];
                ls[j] = (tmp.e, tmp.cnt - 1);
            }
        }
    }

    int[] Ar = { 1, 0, 1, 1, 0, 1 };
    int P = 0;

    int Q(int l, int r)
    {
#if DEBUG
        int sum = 0;
        for (int i = l - 1; i < r; i++)
        {
            sum += Ar[i];
        }
        return sum;
#else
        Console.WriteLine($"? {l} {r}");
        return int.Parse(Console.ReadLine());
#endif
    }

    bool A(int x)
    {
#if DEBUG
        int cnt = 0;
        for (int i = 0; i < N; i++)
        {
            if (Ar[i] == 0) cnt++;
            if (cnt == K[P])
            {
                if (x - 1 != i)
                {
                    throw new Exception();
                }
                Ar[i] = 1;
                break;
            }
        }
        P++;
        Console.WriteLine($"{P} ok");
        return true;
#else
        Console.WriteLine($"! {x}");
        return true;
        // return Console.ReadLine() != "-1";
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}