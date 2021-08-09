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
    private string A, B, C;
    private void Scan()
    {
        A = Console.ReadLine();
        B = Console.ReadLine();
        C = Console.ReadLine();
    }

    public void Solve()
    {
        Scan();

        char[] map = new char[200];
        for (int i = 0; i < 26; i++)
        {
            map[A[i]] = (char)(B[i]-A[i]);
        }
        char[] result = C.ToArray();
        for(int i = 0; i < result.Length; i++)
        {
            if (('a' <= result[i] && result[i] <= 'z') )
            {
                result[i] += map[result[i]];
            }
            if('A' <= result[i] && result[i] <= 'Z')
            {
                result[i] += map[result[i] - 'A' + 'a'];
            }

        }
        Console.WriteLine(new string(result));
    }
}
