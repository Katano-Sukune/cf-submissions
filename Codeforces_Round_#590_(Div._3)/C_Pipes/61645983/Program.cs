using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
public class Program
{
    private StringBuilder Sb;
 
    public void Solve()
    {
        var sc = new Scanner();
        Sb = new StringBuilder();
        int Q = sc.NextInt();
        for (int i = 0; i < Q; i++)
        {
            Query(sc.NextInt(), new string[] {sc.Next(), sc.Next()});
        }
 
        Console.Write(Sb.ToString());
    }
 
    private void Query(int n, string[] s)
    {
        int posY = 0;
        int r = 0;
        for (int i = 1; i <= n;)
        {
            char p = s[posY][i - 1];
            switch (r)
            {
                case 0:
                    if (p == '1' || p == '2')
                    {
                        i++;
                        r = 0;
                    }
                    else
                    {
                        if (posY == 0)
                        {
                            r = 3;
                            posY = 1;
                        }
                        else
                        {
                            r = 1;
                            posY = 0;
                        }
 
                    }
 
                    break;
                case 1:
                    if (p == '1' || p == '2')
                    {
                        Sb.AppendLine("NO");
                        return;
                    }
                    else
                    {
                        if (posY == 0)
                        {
                            r = 0;
                            i++;
                        }
                    }
 
                    break;
                case 2:
                    break;
                case 3:
                    if (p == '1' || p == '2')
                    {
                        Sb.AppendLine("NO");
                        return;
                    }
                    else
                    {
                        if (posY == 1)
                        {
                            r = 0;
                            i++;
                        }
                    }
                    break;
            }
        }
 
        Sb.AppendLine(posY == 1 ? "YES" : "NO");
    }
 
    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}
 
class Scanner
{
    public Scanner()
    {
        _pos = 0;
        _line = new string[0];
    }
 
    const char Separator = ' ';
    private int _pos;
    private string[] _line;
 
    #region スペース区切りで取得
 
    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = Console.ReadLine().Split(Separator);
            _pos = 0;
        }
 
        return _line[_pos++];
    }
 
    public int NextInt()
    {
        return int.Parse(Next());
    }
 
    public long NextLong()
    {
        return long.Parse(Next());
    }
 
    public double NextDouble()
    {
        return double.Parse(Next());
    }
 
    #endregion
 
    #region 型変換
 
    private int[] ToIntArray(string[] array)
    {
        var result = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = int.Parse(array[i]);
        }
 
        return result;
    }
 
    private long[] ToLongArray(string[] array)
    {
        var result = new long[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = long.Parse(array[i]);
        }
 
        return result;
    }
 
    private double[] ToDoubleArray(string[] array)
    {
        var result = new double[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = double.Parse(array[i]);
        }
 
        return result;
    }
 
    #endregion
 
    #region 配列取得
 
    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = Console.ReadLine().Split(Separator);
 
        _pos = _line.Length;
        return _line;
    }
 
    public int[] IntArray()
    {
        return ToIntArray(Array());
    }
 
    public long[] LongArray()
    {
        return ToLongArray(Array());
    }
 
    public double[] DoubleArray()
    {
        return ToDoubleArray(Array());
    }
 
    #endregion
}