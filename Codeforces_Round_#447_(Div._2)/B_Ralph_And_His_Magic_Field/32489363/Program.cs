
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

}
class Program
{
    private readonly int Mod = 1000000007;
    private long N, M;
    private int K;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        M = sc.NextLong();
        K = sc.NextInt();
        long yy = (N - 1) % (Mod - 1);
        long xx = (M - 1) % (Mod - 1);
        if (K == 1)
        {
            Console.WriteLine(Pow((xx * yy) % (Mod - 1)));
        }
        else if (K == -1)
        {
            if (N % 2 == M % 2)
            {
                Console.WriteLine(Pow((xx * yy) % (Mod - 1)));
            }
            else
            {
                Console.WriteLine(0);
            }
        }
    }

    private long Pow(long y)
    {
        long res = 1;
        long x = 2;
        while (y > 0)
        {
            if (y % 2 == 1)
            {
                res *= x;
                res %= Mod;
            }
            y /= 2;
            x = (x * x) % Mod;
        }
        return res;
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}