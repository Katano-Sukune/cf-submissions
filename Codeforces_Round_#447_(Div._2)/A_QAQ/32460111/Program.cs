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

}
class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        var S = sc.Next();
        long ans = 0;
        for (int i = 0; i < S.Length; i++)
        {
            if (S[i] == 'Q')
            {
                for (int j = i + 1; j < S.Length; j++)
                {
                    if(S[j]=='A')
                    {
                        for(int k=j+1;k<S.Length;k++)
                        {
                            if(S[k]=='Q')
                            {
                                ans++;
                            }
                        }
                    }
                }
            }
        }
        Console.WriteLine(ans);
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}
