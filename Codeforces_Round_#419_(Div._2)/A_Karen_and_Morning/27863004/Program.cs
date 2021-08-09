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
    private int HH, WW;
    private void Scan()
    {
        var line = Console.ReadLine().Split(':');
        HH = int.Parse(line[0]);
        WW = int.Parse(line[1]);

    }

    public void Solve()
    {
        Scan();
        for (int i = 0; i <= 100000; i++)
        {
            int w = WW + i;
            int h = HH + w / 60;
            w %= 60;
            h %= 24;
            string time = string.Format("{0:D2}{1:D2}",h,w);
            if(time[0] == time[3] && time[1] == time[2])
            {
                Console.WriteLine(i);
                return;
            }
        }
    }
}
