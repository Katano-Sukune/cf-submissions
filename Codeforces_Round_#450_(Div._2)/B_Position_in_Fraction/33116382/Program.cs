using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Scanner
{
    private readonly char Separator = ' ';
    private int Index = 0;
    private string[] Line = new string[0];
    public string Next()
    {
        if (Index >= Line.Length)
        {
            Line = Console.ReadLine().Split(Separator);
            Index = 0;
        }
        var ret = Line[Index];
        Index++;
        return ret;
    }

    public int NextInt()
    {
        return int.Parse(Next());
    }

    public long NextLong()
    {
        return long.Parse(Next());
    }

    public string[] StringArray()
    {
        Line = Console.ReadLine().Split(Separator);
        Index = Line.Length;
        return Line;
    }

    public int[] IntArray()
    {
        var l = StringArray();
        var res = new int[l.Length];
        for (int i = 0; i < l.Length; i++)
        {
            res[i] = int.Parse(l[i]);
        }
        return res;
    }

    public long[] LongArray()
    {
        var l = StringArray();
        var res = new long[l.Length];
        for (int i = 0; i < l.Length; i++)
        {
            res[i] = long.Parse(l[i]);
        }
        return res;
    }
}

class Program
{
    private int A, B, C;
    private void Scan()
    {
        var sc = new Scanner();
        A = sc.NextInt();
        B = sc.NextInt();
        C = sc.NextInt();
    }

    public void Solve()
    {
        Scan();
        var b = new bool[100001];
        for (int i = 1; ; i++)
        {
            if(b[A])
            {
                Console.WriteLine(-1);
                return;
            }
            b[A] = true;
            A *= 10;
            if (A / B == C)
            {
                Console.WriteLine(i);
                return;
            }
            A %= B;
        }
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}