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

struct S
{
    public int Begin, End, Length;
    public S(int b, int e)
    {
        Begin = b;
        End = e;
        Length = e - b + 1;
    }
}

class SegmentTree<T>
{
    private T[] Array;
    private readonly int N;
    private Comparison<T> Comparison;
    private readonly T Min;
    public SegmentTree(int length, T min) : this(length, min, Comparer<T>.Default) { }
    public SegmentTree(int length, T min, IComparer<T> comparer) : this(length, min, comparer.Compare) { }
    public SegmentTree(int length, T min, Comparison<T> comparison)
    {
        for (N = 1; N < length; N *= 2) ;
        Array = new T[N * 2];
        Min = min;
        for (int i = 1; i < N * 2; i++)
        {
            Array[i] = Min;
        }
        Comparison = comparison;
    }

    private T Max(T a, T b)
    {
        if (Comparison(a, b) >= 0)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    public void Update(int index, T value)
    {
        index += N;
        Array[index] = value;
        for (index = index / 2; index > 0; index /= 2)
        {
            Array[index] = Max(Array[index * 2], Array[index * 2 + 1]);
        }
    }

    private T Query(int a, int b, int k, int l, int r)
    {
        if (a <= l && r <= b)
        {
            return Array[k];
        }
        if (b <= l || r <= a)
        {
            return Min;
        }
        T left = Query(a, b, k * 2, l, (l + r) / 2);
        T right = Query(a, b, k * 2 + 1, (l + r) / 2, r);
        return Max(left, right);
    }

    public T Query(int a, int b)
    {
        return Query(a, b, 1, 0, N);
    }
}

class Program
{
    private int H1, A1, C1;
    private int H2, A2;
    private void Scan()
    {
        var sc = new Scanner();
        H1 = sc.NextInt();
        A1 = sc.NextInt();
        C1 = sc.NextInt();
        H2 = sc.NextInt();
        A2 = sc.NextInt();
    }

    public void Solve()
    {
        Scan();
        var sb = new StringBuilder();
        int cnt;
        for (cnt = 1; ; cnt++)
        {
            if (H2 <= A1)
            {
                sb.AppendLine("STRIKE");
                break;
            }
            else
            {
                if (H1 <= A2)
                {
                    sb.AppendLine("HEAL");
                    H1 += C1;
                }
                else
                {
                    sb.AppendLine("STRIKE");
                    H2 -= A1;
                }
                H1 -= A2;
            }
        }
        Console.WriteLine(cnt);
        Console.Write(sb.ToString());
    }

    static void Main(string[] args)
    {
        new Program().Solve();
    }
}