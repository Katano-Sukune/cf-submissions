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
    private int X1, Y1, X2, Y2, X, Y;
    private void Scan()
    {
        var line = Console.ReadLine().Split(' ');
        X1 = int.Parse(line[0]);
        Y1 = int.Parse(line[1]);
        X2 = int.Parse(line[2]);
        Y2 = int.Parse(line[3]);
        line = Console.ReadLine().Split(' ');
        X = int.Parse(line[0]);
        Y = int.Parse(line[1]);
    }

    public void Solve()
    {
        Scan();
        int xDist = Math.Abs(X2 - X1);
        int yDist = Math.Abs(Y2 - Y1);
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
