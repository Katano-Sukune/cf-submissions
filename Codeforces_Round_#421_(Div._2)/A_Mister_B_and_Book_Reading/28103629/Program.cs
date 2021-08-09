using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}

class Magatro
{
    private int C, V0, V1, A, L;

    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        C = int.Parse(line[0]);
        V0 = int.Parse(line[1]);
        V1 = int.Parse(line[2]);
        A = int.Parse(line[3]);
        L = int.Parse(line[4]);
    }


    public void Solve()
    {
        Scan();
        int read = 0;
        for(int i = 1; true; i++)
        {
            read += V0;
            V0 = Math.Min(V0 + A, V1);
            if (read >= C)
            {
                Console.WriteLine(i);
                return;
            }
            read = Math.Max(0, read - L);
        }
    }
}
