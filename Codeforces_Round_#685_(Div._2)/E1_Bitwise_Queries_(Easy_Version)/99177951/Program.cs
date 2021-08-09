using System;
using System.Linq;
using System.Threading;

public class Program
{
    private int[] Sample = {7, 6, 5, 4, 3, 2, 1, 0};
    private int N;

    public void Solve()
    {
#if DEBUG
        N = Sample.Length;
#else
         N = int.Parse(Console.ReadLine());
#endif


        // 2べきN
        // 配列a

        // AND i j
        // a_i, a_jのAND

        // OR

        // XOR

        // n+2回まで

        int[] xor = new int[N];
        for (int i = 1; i < N; i++)
        {
            xor[i] = XOR(0, i);
        }

        // 同じやつ

        int[] t = new int[N];
        for (int i = 0; i < N; i++)
        {
            t[i] = -1;
        }

        t[0] = 0;
        int l = -1;
        int r = -1;
        for (int i = 1; i < N; i++)
        {
            if (t[xor[i]] == -1)
            {
                t[xor[i]] = i;
            }
            else
            {
                l = t[xor[i]];
                r = i;
                break;
            }
        }

        int[] ans = new int[N];
        if (l == -1)
        {
            // 全部違う
            // 反転してる2個
            int l1 = 0;
            int r1 = -1;
            for (int i = 0; i < N; i++)
            {
                if (xor[i] == N - 1)
                {
                    r1 = i;
                    break;
                }
            }

            int c = -1;
            for (int i = 0; i < N; i++)
            {
                if (i == l1 || i == r1) continue;
                c = i;
                break;
            }

            ans[c] = AND(l1, c) | AND(r1, c);
            ans[0] = xor[c] ^ ans[c];
            for (int i = 1; i < N; i++)
            {
                if(i == c) continue;
                ans[i] = ans[0] ^ xor[i];
            }
        }
        else
        {
            // l,r同じ
            int or = OR(l, r);
            ans[l] = or;
            ans[r] = or;
            if (l != 0) ans[0] = xor[l] ^ ans[l];
            for (int i = 1; i < N; i++)
            {
                if (i == l || i == r) continue;
                ans[i] = xor[i] ^ ans[0];
            }
        }

        Console.WriteLine($"! {string.Join(" ", ans)}");
    }

    int XOR(int i, int j)
    {
#if DEBUG
        return Sample[i] ^ Sample[j];
#else
        Console.WriteLine($"XOR {i + 1} {j + 1}");
        return int.Parse(Console.ReadLine());
#endif
    }

    int AND(int i, int j)
    {
#if DEBUG
        return Sample[i] & Sample[j];
#else
        Console.WriteLine($"AND {i + 1} {j + 1}");
        return int.Parse(Console.ReadLine());
#endif
    }

    int OR(int i, int j)
    {
#if DEBUG
        return Sample[i] | Sample[j];
#else
        Console.WriteLine($"OR {i + 1} {j + 1}");
        return int.Parse(Console.ReadLine());
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}