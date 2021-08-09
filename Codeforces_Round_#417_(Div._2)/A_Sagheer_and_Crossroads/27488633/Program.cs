using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}

class Magatro
{
    bool[][] A;
    public void Solve()
    {
        A = new bool[4][];
        for (int i = 0; i < 4; i++)
        {
            A[i] = Console.ReadLine().Split(' ').Select(s => s == "1").ToArray();
        }
        for (int i = 0; i <= 3; i++)
        {
            if (A[i][3])
            {
                bool a = A[i][0] || A[i][1] || A[i][2];
                bool b = A[(i + 1) % 4][0] || A[(i + 2) % 4][1] || A[(i + 3) % 4][2];
                if (a || b)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }
        }

        Console.WriteLine("NO");
    }
}
