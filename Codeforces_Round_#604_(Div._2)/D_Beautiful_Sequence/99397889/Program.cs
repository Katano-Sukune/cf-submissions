using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int A, B, C, D;

    public void Solve()
    {
        var sc = new Scanner();
        A = sc.NextInt();
        B = sc.NextInt();
        C = sc.NextInt();
        D = sc.NextInt();

        // 0,1,2,3 がA,B,C,D個
        int[] ans = new int[A + B + C + D];
        int ptr = 0;
        // 連続2数の差が1
        if (B == 0)
        {
            if (A == 0)
            {
                // 2323
                if (C == D || C - 1 == D)
                {
                    for (int i = 0; i < C + D; i++)
                    {
                        ans[i] = i % 2 == 0 ? 2 : 3;
                    }

                    Console.WriteLine("YES");
                    Console.WriteLine(string.Join(" ", ans));
                    return;
                }

                // 3232
                if (C + 1 == D)
                {
                    for (int i = 0; i < C + D; i++)
                    {
                        ans[i] = i % 2 == 0 ? 3 : 2;
                    }

                    Console.WriteLine("YES");
                    Console.WriteLine(string.Join(" ", ans));
                    return;
                }
            }

            // 0
            if (A == 1 && C == 0 && D == 0)
            {
                Console.WriteLine("YES");
                Console.WriteLine("0");
                return;
            }

            Console.WriteLine("NO");
            return;
        }
        else
        {
            // 先頭3232....
            if (D >= 1 && C >= 1)
            {
                // 末尾23
                if (D >= 2 && C >= 2)
                {
                    // 2323232の個数
                    int t1 = (C - 2) - (D - 2);
                    if (t1 >= 0)
                    {
                        if (t1 + A == B - 1)
                        {
                            ans[ptr++] = 3;
                            ans[ptr++] = 2;
                            for (int i = 0; i < D - 2; i++)
                            {
                                ans[ptr++] = 3;
                                ans[ptr++] = 2;
                            }

                            for (int i = 0; i < A; i++)
                            {
                                ans[ptr++] = 1;
                                ans[ptr++] = 0;
                            }

                            for (int i = 0; i < t1; i++)
                            {
                                ans[ptr++] = 1;
                                ans[ptr++] = 2;
                            }

                            ans[ptr++] = 1;
                            ans[ptr++] = 2;
                            ans[ptr++] = 3;

                            Console.WriteLine("YES");
                            Console.WriteLine(string.Join(" ", ans));
                            return;
                        }
                    }
                }

                {
                    int t1 = (C - 1) - (D - 1);
                    if (t1 >= 0)
                    {
                        // 末尾1
                        if (t1 + A == B - 1)
                        {
                            ans[ptr++] = 3;
                            ans[ptr++] = 2;
                            for (int i = 0; i < D - 1; i++)
                            {
                                ans[ptr++] = 3;
                                ans[ptr++] = 2;
                            }

                            for (int i = 0; i < A; i++)
                            {
                                ans[ptr++] = 1;
                                ans[ptr++] = 0;
                            }

                            for (int i = 0; i < t1; i++)
                            {
                                ans[ptr++] = 1;
                                ans[ptr++] = 2;
                            }

                            ans[ptr++] = 1;

                            Console.WriteLine("YES");
                            Console.WriteLine(string.Join(" ", ans));
                            return;
                        }

                        // 末尾2
                        if (t1 + A == B)
                        {
                            ans[ptr++] = 3;
                            ans[ptr++] = 2;
                            for (int i = 0; i < D - 1; i++)
                            {
                                ans[ptr++] = 3;
                                ans[ptr++] = 2;
                            }

                            for (int i = 0; i < A; i++)
                            {
                                ans[ptr++] = 1;
                                ans[ptr++] = 0;
                            }

                            for (int i = 0; i < t1; i++)
                            {
                                ans[ptr++] = 1;
                                ans[ptr++] = 2;
                            }

                            Console.WriteLine("YES");
                            Console.WriteLine(string.Join(" ", ans));
                            return;
                        }
                    }
                }
            }

            {
                int t1 = C - D;

                if (t1 == 0)
                {
                    if (D == 0)
                    {
                        if (A == B || A + 1 == B)
                        {
                            for (int i = 0; i < A + B; i++)
                            {
                                ans[i] = i % 2 == 0 ? 1 : 0;
                            }

                            Console.WriteLine("YES");
                            Console.WriteLine(string.Join(" ", ans));
                            return;
                        }

                        if (A == B + 1)
                        {
                            for (int i = 0; i < A + B; i++)
                            {
                                ans[i] = i % 2 == 0 ? 0 : 1;
                            }

                            Console.WriteLine("YES");
                            Console.WriteLine(string.Join(" ", ans));
                            return;
                        }
                    }
                }

                if (t1 >= 1)
                {
                    if (t1 + A == B - 1 || t1 + A == B)
                    {
                        for (int i = 0; i < A; i++)
                        {
                            ans[ptr++] = 1;
                            ans[ptr++] = 0;
                        }

                        for (int i = 0; i < t1; i++)
                        {
                            ans[ptr++] = 1;
                            ans[ptr++] = 2;
                        }

                        for (int i = 0; i < D; i++)
                        {
                            ans[ptr++] = 3;
                            ans[ptr++] = 2;
                        }

                        if (t1 + A == B - 1)
                        {
                            ans[ptr++] = 1;
                        }

                        Console.WriteLine("YES");
                        Console.WriteLine(string.Join(" ", ans));

                        return;
                    }

                    if (t1 + A == B + 1)
                    {
                        for (int i = 0; i < t1 - 1; i++)
                        {
                            ans[ptr++] = 2;
                            ans[ptr++] = 1;
                        }

                        ans[ptr++] = 2;
                        for (int i = 0; i < D; i++)
                        {
                            ans[ptr++] = 3;
                            ans[ptr++] = 2;
                        }

                        for (int i = 0; i < A; i++)
                        {
                            ans[ptr++] = 1;
                            ans[ptr++] = 0;
                        }

                        Console.WriteLine("YES");
                        Console.WriteLine(string.Join(" ", ans));

                        return;
                    }
                }
            }
        }

        Console.WriteLine("NO");
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