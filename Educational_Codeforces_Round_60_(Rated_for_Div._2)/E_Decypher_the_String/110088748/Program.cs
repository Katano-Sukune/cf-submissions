using System;
using System.Linq;
using System.Threading;

public class Program
{
    private string S;

    public void Solve()
    {
#if DEBUG
        S = "yzx";
#else
        S = Console.ReadLine();
#endif
        int n = S.Length;
        // 26で割ったあまり
        char[] a = new char[n];
        for (int i = 0; i < n; i++)
        {
            a[i] = (char) (i % 26 + 'a');
        }

        string resA = Q(new string(a));

        char[] b = new char[n];
        for (int i = 0; i < n; i++)
        {
            b[i] = (char) ((i / 26) % 26 + 'a');
        }

        string resB = Q(new string(b));

        char[] c = new char[n];
        for (int i = 0; i < n; i++)
        {
            c[i] = (char) (i / (26 * 26) + 'a');
        }

        string resC = Q(new string(c));

        char[] ans = new char[n];
        for (int i = 0; i < n; i++)
        {
            int idx = 0;
            idx += 26 * 26 * (resC[i] - 'a');
            idx += 26 * (resB[i] - 'a');
            idx += resA[i] - 'a';

            ans[idx] = S[i];
        }

        Console.WriteLine($"! {new string(ans)}");
    }

    string Q(string s)
    {
#if DEBUG
        int[] tmp = new[] {1, 2, 0};
        char[] ar = new char[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            ar[i] = s[tmp[i]];
        }

        return new string(ar);
#else
        Console.WriteLine($"? {s}");
        return Console.ReadLine();
#endif
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}