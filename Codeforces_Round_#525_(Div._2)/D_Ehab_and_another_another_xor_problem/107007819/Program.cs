using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Program
{
    public void Solve()
    {
        const int l = 30;
        int a = 0;
        int b = 0;
        int q1 = Query(0, 0);

        for (int i = l - 1; i >= 0; i--)
        {
            int bit = 1 << i;
            if (q1 == 0)
            {
                int q3 = Query(a | bit, b);
                if (q3 == -1)
                {
                    a |= bit;
                    b |= bit;
                }
            }
            else if (q1 == 1)
            {
                int q2 = Query(a | bit, b | bit);
                int q3 = Query(a | bit, b);
                if (q2 == 1)
                {
                    if (q3 == -1)
                    {
                        a |= bit;
                        b |= bit;
                    }
                }
                else if (q2 == -1)
                {
                    a |= bit;
                    q1 = q3;
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (q1 == -1)
            {
                int q2 = Query(a | bit, b | bit);
                int q3 = Query(a, b | bit);
                if (q2 == -1)
                {
                    if (q3 == 1)
                    {
                        a |= bit;
                        b |= bit;
                    }
                }
                else if (q2 == 1)
                {
                    b |= bit;
                    q1 = q3;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        Console.WriteLine($"! {a} {b}");
    }

    private const int A = 3;
    private const int B = 1;

    int Query(int c, int d)
    {
#if DEBUG
        return (A ^ c).CompareTo(B ^ d);
#else
        Console.WriteLine($"? {c} {d}");
        return int.Parse(Console.ReadLine());
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}