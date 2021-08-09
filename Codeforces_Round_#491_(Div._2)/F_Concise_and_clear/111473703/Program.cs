using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();

        // 0個
        string ans = N.ToString();

        var map = new Dictionary<long, (long a, long b)>();
        for (long a = 2; a * a <= N; a++)
        {
            int lenA = Len(a);
            long tmp = a * a;
            for (int b = 2; tmp <= N; b++, tmp *= a)
            {
                int lenB = Len(b);
                if (lenA + 1 + lenB < Len(tmp))
                {
                    int len;
                    if (map.TryGetValue(tmp, out (long a, long b) o))
                    {
                        len = Len(o.a, o.b);
                    }
                    else
                    {
                        len = Len(tmp);
                    }

                    int len1 = lenA + 1 + lenB;
                    if (len1 < len)
                    {
                        map[tmp] = (a, b);
                    }
                }

                // 1個
                // a^b
                if (tmp == N)
                {
                    int len1 = lenA + 1 + lenB;

                    if (len1 < ans.Length)
                    {
                        ans = $"{a}^{b}";
                    }
                }

                // 2個
                if (N % tmp == 0)
                {
                    // a^b*c
                    long c = N / tmp;
                    int lenC = Len(c);
                    int len2 = lenA + 1 + lenB + 1 + lenC;
                    if (len2 < ans.Length)
                    {
                        ans = $"{a}^{b}*{c}";
                    }
                }

                {
                    // a^b+c
                    long c = N - tmp;
                    int lenC = Len(c);
                    int len2 = lenA + 1 + lenB + 1 + lenC;
                    if (len2 < ans.Length)
                    {
                        ans = $"{a}^{b}+{c}";
                    }
                }
            }
        }

        // 3個

        for (long a = 2; a * a <= N; a++)
        {
            int lenA = Len(a);
            if (lenA + 2 + 2 + 2 >= ans.Length) break;
            long tmp = a * a;
            for (int b = 2; tmp <= N; tmp *= a, b++)
            {
                int lenB = Len(b);
                if (lenA + 1 + lenB + 2 + 2 >= ans.Length) break;
                // a^b*c+d
                {
                    long c = N / tmp;
                    long d = N % tmp;
                    if (d == 0) continue;
                    int len3 = lenA + 1 + lenB + 1 + Len(c) + 1 + Len(d);
                    if (len3 < ans.Length)
                    {
                        ans = $"{a}^{b}*{c}+{d}";
                    }
                }
            }
        }

        foreach ((long t, (long a, long b)) in map)
        {
            if (N % t == 0)
            {
                // a^b*c^d
                long d = N / t;
                (long c, long d) o;
                if (map.TryGetValue(d, out o))
                {
                    int len3 = Len(a, b, o.c, o.d);
                    if (len3 < ans.Length)
                    {
                        ans = $"{a}^{b}*{o.c}^{o.d}";
                    }
                }
            }

            {
                // a^b+c^d
                long d = N - t;
                (long c, long d) o;
                if (map.TryGetValue(d, out o))
                {
                    int len3 = Len(a, b, o.c, o.d);
                    if (len3 < ans.Length)
                    {
                        ans = $"{a}^{b}+{o.c}^{o.d}";
                    }
                }
            }
        }


        // 4個
        // a^b+c^d+e
        // a^b*c^d+e
        // a^b*c+d^e
        for (long a = 2; a * a < N; a++)
        {
            int lenA = Len(a);
            if (lenA + 2 + 2 + 2 + 2 >= ans.Length) break;
            long ab = a * a;
            for (int b = 2; ab <= N; b++, ab *= a)
            {
                int lenB = Len(b);
                if (lenA + 1 + lenB + 2 + 2 + 2 >= ans.Length) break;
                for (long c = 2; ab + c * c <= N; c++)
                {
                    int lenC = Len(c);
                    long cd = c * c;
                    if (lenA + 1 + lenB + 1 + lenC + 2 + 2 >= ans.Length) break;
                    for (int d = 2; ab + cd <= N; d++, cd *= c)
                    {
                        {
                            long e = N - ab - cd;
                            int len4 = lenA + 1 + lenB + 1 + lenC + 1 + Len(d) + 1 + Len(e);
                            if (len4 < ans.Length)
                            {
                                ans = $"{a}^{b}+{c}^{d}+{e}";
                            }
                        }

                        if ((N - ab) % cd == 0)
                        {
                            long e = (N - ab) / cd;
                            int len4 = lenA + 1 + lenB + 1 + lenC + 1 + Len(d) + 1 + Len(e);
                            if (len4 < ans.Length)
                            {
                                ans = $"{a}^{b}+{c}^{d}*{e}";
                            }
                        }
                    }

                    cd = c * c;
                    for (int d = 2; (BigInteger) ab * cd <= N; d++, cd *= c)
                    {
                        {
                            long e = N - ab * cd;
                            int len4 = lenA + 1 + lenB + 1 + lenC + 1 + Len(d) + 1 + Len(e);
                            if (len4 < ans.Length)
                            {
                                ans = $"{a}^{b}*{c}^{d}+{e}";
                            }
                        }

                        if (N % (ab * cd) == 0)
                        {
                            long e = N / (ab * cd);
                            int len4 = lenA + 1 + lenB + 1 + lenC + 1 + Len(d) + 1 + Len(e);
                            if (len4 < ans.Length)
                            {
                                ans = $"{a}^{b}*{c}^{d}*{e}";
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine(ans);
    }

    int Len(long n)
    {
        if (n < 10) return 1;
        return 1 + Len(n / 10);
    }

    int Len(params long[] a)
    {
        int res = 0;
        foreach (var i in a)
        {
            res += Len(i);
        }

        return res + a.Length - 1;
    }

    string ToString(long[] ar)
    {
        if (ar.Length == 2)
        {
            return $"{ar[0]}^{ar[1]}";
        }
        else if (ar.Length == 3)
        {
            return $"{ar[0]}*{ar[1]}^{ar[2]}";
        }

        throw new Exception();
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