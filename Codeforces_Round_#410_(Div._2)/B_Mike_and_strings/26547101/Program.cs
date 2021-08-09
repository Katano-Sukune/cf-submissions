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
    static void Main(string[] args)
    {
        new Magatro().Solve();
    }
}

public class Scanner
{
    private StreamReader Sr;

    private string[] S;
    private int Index;
    private const char Separator = ' ';

    public Scanner(Stream source)
    {
        Index = 0;
        S = new string[0];
        Sr = new StreamReader(source);
    }

    private string[] Line()
    {
        return Sr.ReadLine().Split(Separator);
    }

    public string Next()
    {
        string result;
        if (Index >= S.Length)
        {
            S = Line();
            Index = 0;
        }
        result = S[Index];
        Index++;
        return result;
    }
    public int NextInt()
    {
        return int.Parse(Next());
    }
    public double NextDouble()
    {
        return double.Parse(Next());
    }
    public long NextLong()
    {
        return long.Parse(Next());
    }
    public decimal NextDecimal()
    {
        return decimal.Parse(Next());
    }
    public string[] StringArray()
    {
        Next();
        Index = S.Length;
        return S.ToArray();
    }
    public int[] IntArray()
    {
        return StringArray().Select(int.Parse).ToArray();
    }
    public long[] LongArray()
    {
        return StringArray().Select(long.Parse).ToArray();
    }
    public bool EndOfStream
    {
        get { return Sr.EndOfStream; }
    }
}

class Magatro
{
    int N;
    string[] S;
    public void Solve()
    {
        var cin = new Scanner(Console.OpenStandardInput());
        N = cin.NextInt();
        if (N == 1)
        {
            Console.WriteLine(0);
            return;
        }
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = cin.Next();
        }
        int ans = int.MaxValue ;
      foreach(var s in S)
        {
            int cnt = 0;
            foreach(var ss in S)
            {
                var sss = dist(s, ss);
                if(sss == -1)
                {
                    Console.WriteLine(-1);
                    return;
                }
                cnt += sss;
            }

            ans = Math.Min(ans, cnt);
        }
        Console.WriteLine(ans);
    }

    int dist (string a,string b)
    {
        for(int i = 0; i < a.Length; i++)
        {
            bool flag = true;
            for(int j = 0; j < a.Length; j++)
            {
                if (a[j] != b[(i + j) % a.Length])
                {
                    flag = false;
                    break;
                }
            }

            if (flag) return i;
        }
        return -1;
    }

    
}

