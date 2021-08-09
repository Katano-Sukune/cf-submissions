using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N;
    string[] P;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = new string[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = sc.Next();
        }

        long[] t = new long[26];

        foreach (var s in P)
        {
            long[] next = new long[26];

            // 全部同じ
            int f = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == s[0])
                {
                    f++;
                }
                else
                {
                    break;
                }
            }

            if (f == s.Length)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (s[0] - 'a' == i)
                    {
                        next[i] = (s.Length + 1) * (t[i]) + s.Length;
                    }
                    else
                    {
                        next[i] = Math.Max(next[i], t[i] > 0 ? 1 : 0);
                    }
                }
            }
            else
            {
                int cnt = 1;
                int c = s[f];
                for (int i = f+1; i < s.Length; i++)
                {
                    if (s[i] != c)
                    {
                        next[c - 'a'] = Math.Max(next[c - 'a'], cnt);

                        cnt = 0;
                        c = s[i];
                    }
                    cnt++;
                }

                if (s[0] != s[^1])
                {

                    for (int i = 0; i < 26; i++)
                    {
                        if (c - 'a' == i)
                        {
                            next[i] = Math.Max(next[i], cnt + (t[i] > 0 ? 1 : 0));
                        }
                        else if (s[0] - 'a' == i)
                        {
                            next[i] = Math.Max(next[i], f + (t[i] > 0 ? 1 : 0));
                        }
                        else
                        {
                            next[i] = Math.Max(next[i], t[i] > 0 ? 1 : 0);
                        }
                    }
                    // Console.WriteLine($"{s} {cnt}");
                }
                else
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (c - 'a' == i)
                        {
                            if (t[i] > 0)
                            {
                                next[i] = Math.Max(next[i], f + cnt + 1);
                            }
                            else
                            {
                                next[i] = Math.Max(next[i], Math.Max(f, cnt));
                            }
                        }
                        else
                        {
                            next[i] = Math.Max(next[i], t[i] > 0 ? 1 : 0);
                        }
                    }
                }


            }

            for (int i = 0; i < 26; i++)
            {

                next[i] = Math.Min(next[i], int.MaxValue);
            }
            t = next;
            // Console.WriteLine(string.Join(" ",t));
        }

        Console.WriteLine(t.Max());

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
