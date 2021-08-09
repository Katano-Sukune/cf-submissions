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
        int ans = 1;
        for(int i = 1; i <= Math.Min(A, B); i++)
        {
            ans *= i;
        }
        Console.WriteLine(ans);
    }
}
