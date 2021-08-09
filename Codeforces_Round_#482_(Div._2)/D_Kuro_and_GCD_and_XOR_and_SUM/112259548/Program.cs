using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private const int Max = 100000;

    public void Solve()
    {
        var sc = new Scanner();

        // Q個タスク
        // 1 u Aにu追加

        // 2 x k s
        // Aから GCD(xi,v)がkで割り切れる, x+v <= s, x^vが最大になるvを探す


        int q = sc.NextInt();
        int[] t = new int[q];
        int[] x = new int[q];
        int[] k = new int[q];
        int[] s = new int[q];

        int cc = 0;
        int[] map = new int[Max + 1];
        Array.Fill(map, -1);


        for (int i = 0; i < q; i++)
        {
            t[i] = sc.NextInt();
            if (t[i] == 1)
            {
                x[i] = sc.NextInt();
            }
            else
            {
                x[i] = sc.NextInt();
                k[i] = sc.NextInt();
                s[i] = sc.NextInt();
                if (map[k[i]] == -1)
                {
                    map[k[i]] = cc++;
                }
            }
        }


        var c = new Trie[cc];
        for (int i = 0; i < cc; i++)
        {
            c[i] = new Trie();
        }

        List<int>[] div = new List<int>[Max + 1];
        for (int i = 1; i <= Max; i++)
        {
            div[i] = new List<int>();
        }

        for (int i = 1; i <= Max; i++)
        {
            if (map[i] == -1) continue;
            for (int j = i; j <= Max; j += i)
            {
                div[j].Add(i);
            }
        }

#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif
        for (int i = 0; i < q; i++)
        {
            if (t[i] == 1)
            {
                foreach (int d in div[x[i]])
                {
                    c[map[d]].Add(x[i]);
                }
            }
            else
            {
                if (x[i] % k[i] != 0)
                {
                    Console.WriteLine("-1");
                    continue;
                }

                Console.WriteLine(c[map[k[i]]].V(x[i], s[i]));
            }
        }

        Console.Out.Flush();
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Trie
{
    private const int H = 16;
    public List<int> L, R, Cnt;
    private int[] Stack = new int[H + 1];


    public Trie()
    {
        L = new List<int>();
        R = new List<int>();
        Cnt = new List<int>();
        L.Add(-1);
        R.Add(-1);
        Cnt.Add(0);
    }

    public void Add(int u)
    {
        int cur = 0;
        for (int i = H; i >= 0; i--)
        {
            int bit = 1 << i;
            Stack[i] = cur;
            if ((u & bit) != 0)
            {
                // r
                if (R[cur] == -1)
                {
                    R[cur] = L.Count;
                    L.Add(-1);
                    R.Add(-1);
                    Cnt.Add(0);
                }

                cur = R[cur];
            }
            else
            {
                if (L[cur] == -1)
                {
                    L[cur] = L.Count;
                    L.Add(-1);
                    R.Add(-1);
                    Cnt.Add(0);
                }

                cur = L[cur];
            }
        }

        Cnt[cur] = 1;

        foreach (int i in Stack)
        {
            Cnt[i] = (L[i] == -1 ? 0 : Cnt[L[i]]) + (R[i] == -1 ? 0 : Cnt[R[i]]);
        }
    }

    public int V(int x, int s)
    {
        int max = s - x;
        if (max < 0) return -1;
        return S(0, x, H, false, max, 0);
    }

    int S(int cur, int x, int d, bool f, int max, int num)
    {
        if (d < 0)
        {
            return num;
        }

        int bit = 1 << d;

        if (f)
        {
            if ((x & bit) == 0)
            {
                // R,L
                if (R[cur] != -1 && Cnt[R[cur]] > 0)
                {
                    return S(R[cur], x, d - 1, true, max, num | bit);
                }

                if (L[cur] != -1 && Cnt[L[cur]] > 0)
                {
                    return S(L[cur], x, d - 1, true, max, num);
                }

                // Console.WriteLine("a");
                return -1;
            }
            else
            {
                if (L[cur] != -1 && Cnt[L[cur]] > 0)
                {
                    return S(L[cur], x, d - 1, true, max, num);
                }

                if (R[cur] != -1 && Cnt[R[cur]] > 0)
                {
                    return S(R[cur], x, d - 1, true, max, num | bit);
                }


                // Console.WriteLine("b");
                return -1;
            }
        }
        else
        {
            if ((max & bit) == 0)
            {
                if (L[cur] == -1 || Cnt[L[cur]] == 0)
                {
                    // Console.WriteLine("c");                    
                    return -1;
                }

                return S(L[cur], x, d - 1, false, max, num);
            }
            else
            {
                if ((x & bit) == 0)
                {
                    // R,L
                    if (R[cur] != -1 && Cnt[R[cur]] > 0)
                    {
                        int s = S(R[cur], x, d - 1, false, max, num | bit);
                        if (s != -1)
                        {
                            return s;
                        }
                    }

                    if (L[cur] != -1 && Cnt[L[cur]] > 0)
                    {
                        int s = S(L[cur], x, d - 1, true, max, num);
                        if (s != -1)
                        {
                            return s;
                        }
                    }

                    // Console.WriteLine("d");
                    return -1;
                }
                else
                {
                    if (L[cur] != -1 && Cnt[L[cur]] > 0)
                    {
                        int s = S(L[cur], x, d - 1, true, max, num);
                        if (s != -1) return s;
                    }

                    if (R[cur] != -1 && Cnt[R[cur]] > 0)
                    {
                        int s = S(R[cur], x, d - 1, false, max, num | bit);
                        if (s != -1) return s;
                    }

                    // Console.WriteLine("e");
                    return -1;
                }
            }
        }
    }
}


namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}