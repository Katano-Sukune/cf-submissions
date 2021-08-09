using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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

    public Scanner(Stream stream)
    {
        Index = 0;
        S = new string[0];
        Sr = new StreamReader(stream);
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

public class Magatro
{
    string X, Y;
    private void Scan()
    {
        Scanner scanner = new Scanner(Console.OpenStandardInput());
        X = scanner.Next();
        Y = scanner.Next();
    }

    public void Solve()
    {
        Scan();
        char[] ans = new char[X.Length];
        for (int i = 0; i < X.Length; i++)
        {
            if (X[i] == Y[i])
            {
                ans[i] = X[i];
            }
            else
            {
                if (X[i] < Y[i])
                {
                    Console.WriteLine(-1);
                    return;
                }
                else
                {
                    ans[i] = Y[i];
                }
            }
        }
        string a = new string(ans);
        Console.WriteLine(a);
    }
}