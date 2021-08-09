using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
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
    public string[] StringArray(int index)
    {
        Next();
        Index = S.Length;
        return S.Skip(index).ToArray();
    }
    public string[] StringArray()
    {
        Next();
        Index = S.Length;
        return S.ToArray();
    }
    public int[] IntArray(int index)
    {
        return StringArray(index).Select(int.Parse).ToArray();
    }
    public int[] IntArray()
    {
        return StringArray().Select(int.Parse).ToArray();
    }
    public long[] LongArray(int index)
    {
        return StringArray(index).Select(long.Parse).ToArray();
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

public class Magatro
{

    private int N, K;
    private int[] W;
    private void Scan()
    {
        var cin = new Scanner(Console.OpenStandardInput());
        N = cin.NextInt();
        K = cin.NextInt();
        W = cin.IntArray();
    }

    public void Solve()
    {
        Scan();
        int ans = 0;
        foreach (int i in W)
        {
            ans += (i + K - 1) / K;
        }


        Console.WriteLine((ans+1)/2);
    }
}
