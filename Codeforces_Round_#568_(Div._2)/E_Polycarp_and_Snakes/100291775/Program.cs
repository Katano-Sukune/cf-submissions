using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N, M;
    private string[] S;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        M = sc.NextInt();

        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        int[] u = new int[26];
        int[] d = new int[26];

        int[] l = new int[26];
        int[] r = new int[26];
        for (int i = 0; i < 26; i++)
        {
            u[i] = l[i] = int.MaxValue;
            d[i] = r[i] = int.MinValue;
        }

        int[] cnt = new int[26];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (S[i][j] != '.')
                {
                    int num = S[i][j] - 'a';
                    cnt[num]++;

                    d[num] = Math.Max(d[num], i);
                    r[num] = Math.Max(r[num], j);

                    u[num] = Math.Min(u[num], i);
                    l[num] = Math.Min(l[num], j);
                }
            }
        }

        int tR = -1;
        int tC = -1;

        // これより上に置いた
        bool flag = false;
        var ans = new List<string>();
        for (int c = 25; c >= 0; c--)
        {
            if (cnt[c] == 0)
            {
                if (flag)
                {
                    ans.Add($"{tR + 1} {tC + 1} {tR + 1} {tC + 1}");
                }
            }
            else
            {
                if (u[c] == d[c])
                {
                    for (int col = l[c]; col <= r[c]; col++)
                    {
                        if (S[u[c]][col] == '.')
                        {
                            Console.WriteLine("NO");
                            return;
                        }

                        if (S[u[c]][col] - 'a' < c)
                        {
                            Console.WriteLine("NO");
                            return;
                        }

                        if (S[u[c]][col] - 'a' == c)
                        {
                            cnt[c]--;
                        }
                    }
                }
                else if (l[c] == r[c])
                {
                    for (int row = u[c]; row <= d[c]; row++)
                    {
                        if (S[row][l[c]] == '.')
                        {
                            Console.WriteLine("NO");
                            return;
                        }

                        if (S[row][l[c]] - 'a' < c)
                        {
                            Console.WriteLine("NO");
                            return;
                        }

                        if (S[row][l[c]] - 'a' == c)
                        {
                            cnt[c]--;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("NO");
                    return;
                }

                if (cnt[c] != 0)
                {
                    Console.WriteLine("NO");
                    return;
                }

                flag = true;
                tR = u[c];
                tC = l[c];
                ans.Add($"{u[c] + 1} {l[c] + 1} {d[c] + 1} {r[c] + 1}");
            }
        }

        ans.Reverse();
        Console.WriteLine("YES");
        Console.WriteLine(ans.Count);
        if (ans.Count > 0) Console.WriteLine(string.Join("\n", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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