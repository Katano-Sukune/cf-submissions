using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;

static class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}

class Magatro
{
    private int A, B;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        A = int.Parse(line[0]);
        B = int.Parse(line[1]);
    }

    public void Solve()
    {
        Scan();
        int sum = 0;
        int i = 1;
        int aj;
        for (aj = 0; ; aj++)
        {
            sum += i;
            if (A < sum)
            {
                break;
            }
            i += 2;
        }
        sum = 0;
        i = 2;
        int bj;
        for(bj = 0; ; bj++)
        {
            sum += i;
            if (B < sum)
            {
                break;
            }
            i += 2;
        }
        if (bj < aj)
        {
            Console.WriteLine("Valera");
        }
        else
        {
            Console.WriteLine("Vladik");
        }

    }
}