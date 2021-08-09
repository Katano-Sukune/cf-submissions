using System;
using System.Linq;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, T;
    int[] K;
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

        K = new int[T];
        for (int i = 0; i < T; i++)
        {
            K[i] = int.Parse(Console.ReadLine());
        }
#endif

        // 01文字列
        // k番目の0の位置をt回当てる

        // ? l r [l,r]の和
        // ! x 回答

        // にぶたん

        var ls = new List<(int e, int cnt0)>();
        ls.Add((0, 0));
        ls.Add((N, N - Q(1, N)));
        foreach (var i in K)
        {
            int ok = ls.Count - 1;
            int ng = 0;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (ls[mid].cnt0 >= i) ok = mid;
                else ng = mid;
            }

            int ok2 = ls[ok].e;
            int ng2 = ls[ng].e;
            while (ok2 - ng2 > 1)
            {
                int mid = (ok2 + ng2) / 2;
                int q = mid - Q(1, mid);
                ls.Add((mid, q));
                if (q >= i) ok2 = mid;
                else ng2 = mid;
            }

            A(ok2);
            ls.Sort();
            for (int j = ng; j < ls.Count; j++)
            {
                var t = ls[j];
                ls[j] = (t.e, t.cnt0 - 1);
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
        return Console.ReadLine() != "-1";
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

