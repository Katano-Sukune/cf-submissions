using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Program
{
    private int N;

    public void Solve()
    {
        N = int.Parse(Console.ReadLine());

        // n*nグリッド
        // alice 3色から選ぶ a

        // bob 選ぶ b, a != b
        // 1 <= i,j <= n
        // (i,j)にb色を置く

        // 同じ色のマスが辺で隣接したら alice勝ち
        // else bob勝ち

        // 全部埋まったらbob勝ち

        // 1以外
        // 偶数マス 1置く

        // 1
        // 奇数マス 2置く

        // 偶数マス埋まった
        // 2
        // 3
        // 1, 3
        // 奇数マス2

        // 奇数マス埋まった
        // 1以外 1
        // 1 3

        int pp = N * N;

        int even = 0;
        int odd = 0;

        List<(int i, int j)> oLs = new List<(int i, int j)>();
        List<(int i, int j)> eLs = new List<(int i, int j)>();
        for (int i = 1; i <= N; i++)
        {
            for (int j = 1; j <= N; j++)
            {
                if ((i + j) % 2 == 0) eLs.Add((i, j));
                else oLs.Add((i, j));
            }
        }

        for (int t = 0; t < N * N; t++)
        {
            int a = int.Parse(Console.ReadLine());

            switch (a)
            {
                case 1:
                    if (odd < oLs.Count)
                    {
                        Console.WriteLine($"2 {oLs[odd].i} {oLs[odd].j}");
                        odd++;
                    }
                    else
                    {
                        Console.WriteLine($"3 {eLs[even].i} {eLs[even].j}");
                        even++;
                    }

                    break;
                case 2:
                    if (even < eLs.Count)
                    {
                        Console.WriteLine($"1 {eLs[even].i} {eLs[even].j}");
                        even++;
                    }
                    else
                    {
                        Console.WriteLine($"3 {oLs[odd].i} {oLs[odd].j}");
                        odd++;
                    }

                    break;
                case 3:
                    if (even < eLs.Count)
                    {
                        Console.WriteLine($"1 {eLs[even].i} {eLs[even].j}");
                        even++;
                    }
                    else
                    {
                        Console.WriteLine($"2 {oLs[odd].i} {oLs[odd].j}");
                        odd++;
                    }

                    break;
            }
        }
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}