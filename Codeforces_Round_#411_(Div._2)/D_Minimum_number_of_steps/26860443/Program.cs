using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

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
    public string[] StringArray(int index = 0)
    {
        Next();
        Index = S.Length;
        return S.Skip(index).ToArray();
    }
    public int[] IntArray(int index = 0)
    {
        return StringArray(index).Select(int.Parse).ToArray();
    }
    public long[] LongArray(int index = 0)
    {
        return StringArray(index).Select(long.Parse).ToArray();
    }
    public bool EndOfStream
    {
        get { return Sr.EndOfStream; }
    }
}

class Magatro
{
    private string S;
    const int Mod = (int)1e9 + 7;
    private void Scan()
    {
        var cin = new Scanner(Console.OpenStandardInput());
        S = cin.Next();
    }
    public void Solve()
    {
        Scan();
        int cnt = 0;
        int sum = 0;
        for (int i = S.Length - 1; i >= 0; i--)
        {
            if (S[i] == 'a')
            {
                cnt += sum;
                cnt %= Mod;
                sum *= 2;
                sum %= Mod;
            }
            else if (S[i] == 'b')
            {
                sum++;
                sum %= Mod;
            }
        }
        Console.WriteLine(cnt);

    }
}