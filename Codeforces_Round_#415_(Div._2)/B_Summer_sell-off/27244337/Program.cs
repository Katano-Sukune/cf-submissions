using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;

static class Program
{
    static void Main()
    {
        new Magatro().Solve();
    }
}

struct P
{
    public int Num, Value;
    public P(int num,int value)
    {
        Num = num;
        Value = value;
    }
}

class Magatro
{

    public void Solve()
    {
        int N, F;
        int[] k, l;
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        F = int.Parse(line[1]);
        k = new int[N];
        l = new int[N];
        for (int i = 0; i < N; i++)
        {
            line = Console.ReadLine().Split(' ');
            k[i] = int.Parse(line[0]);
            l[i] = int.Parse(line[1]);
        }

        var a = new int[N];//二ばい
        for (int i = 0; i < N; i++)
        {
            if (2 * k[i] <= l[i])
            {
                a[i] = k[i];
            }
            else
            {
                a[i] = Math.Max(0, l[i] - k[i]);
            } 
        }

        var pq = new PriortyQueue<P>((aa, bb) => aa.Value.CompareTo(bb.Value));
        for (int i = 0; i < N; i++)
        {
            pq.Add(new P(i, a[i]));
        }
        for (int i = 0; i < F; i++)
        {
            var p = pq.Poll();
            k[p.Num] *= 2;
        }
        long ans = 0;

        for(int i = 0; i < N; i++)
        {
            if (k[i] <= l[i])
            {
                ans += k[i];
            }
            else
            {
                ans += l[i];
            }
        }
        Console.WriteLine(ans);
    }
}

public class PriortyQueue<T>
{
    private Comparison<T> Comparator;
    private List<T> L;
    public PriortyQueue(Comparison<T> comparator)
    {
        Clear();
        Comparator = comparator;
    }
    public PriortyQueue(PriortyQueue<T> queue)
    {
        L = queue.ToList();
        Comparator = queue.Comparator;
    }
    public void Add(T item)
    {
        int n = L.Count;
        L.Add(item);
        while (n > 0)
        {
            int i = (n - 1) / 2;
            if (Comparator(L[n], L[i]) > 0)
            {
                Swap(n, i);
            }
            n = i;
        }
    }
    public T Poll()
    {
        T ret = Peak();
        Pop();
        return ret;
    }
    public void Pop()
    {
        int n = L.Count - 1;
        L[0] = L[n];
        L.RemoveAt(n);
        for (int i = 0, j; (j = 2 * i + 1) < n;)
        {
            if ((j != n - 1) && (Comparator(L[j], L[j + 1]) < 0))
            {
                j++;
            }

            if (Comparator(L[i], L[j]) < 0)
            {
                Swap(i, j);
            }
            i = j;
        }
    }
    public T Peak()
    {
        return L[0];
    }
    public T[] ToArray()
    {
        return L.ToArray();
    }
    public List<T> ToList()
    {
        return L.ToList();
    }
    public void Clear()
    {
        L = new List<T>();
    }
    public int Size
    {
        get { return L.Count; }
    }
    public bool IsEmpty
    {
        get { return L.Count == 0; }
    }
    private void Swap(int a, int b)
    {
        T temp = L[a];
        L[a] = L[b];
        L[b] = temp;
    }
}
