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
    private int XOne, YOne, XTwo, YTwo, X, Y;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        XOne = int.Parse(line[0]);
        YOne = int.Parse(line[1]);
        XTwo = int.Parse(line[2]);
        YTwo = int.Parse(line[3]);
        line = Console.ReadLine().Split(' ');
        X = int.Parse(line[0]);
        Y = int.Parse(line[1]);
    }

    public void Solve()
    {
        Scan();
        int xDist = Math.Abs(XTwo - XOne);
        int yDist = Math.Abs(YTwo - YOne);
        if ((xDist % X != 0)||(yDist%Y!=0))
        {
            Console.WriteLine("NO");
            return;
        }
        int xx = xDist / X;
        int yy = yDist / Y;
        if (xx % 2 == yy % 2)
        {
            Console.WriteLine("YES");
        }
        else
        {
            Console.WriteLine("NO");
        }
    }
}
