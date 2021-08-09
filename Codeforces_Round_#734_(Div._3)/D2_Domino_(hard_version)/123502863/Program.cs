using System;
using System.Linq;
using CompLib.Util;
using System.IO;
using System.Threading;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();
        int k = sc.NextInt();

        // k個 横向き
        // n*m/2-k個縦
        // 
        // 
        // 
        // 
        // 
        char[][] ans = new char[n][];
        for (int i = 0; i < n; i++) ans[i] = new char[m];

        if (m % 2 == 0)
        {
            if (n % 2 == 0)
            {
                if (k % 2 != 0)
                {
                    Console.WriteLine("NO");
                    return;
                }
                int tmp = 0;
                for (int i = 0; i < n; i += 2)
                {
                    for (int j = 0; j < m; j += 2)
                    {
                        if (tmp < k)
                        {
                            tmp+=2;
                            char a, b;
                            if ((j / 2) % 2 == 0)
                            {
                                a = 'a';
                                b = 'b';
                            }
                            else
                            {
                                a = 'b';
                                b = 'a';
                            }
                            ans[i][j] = a;
                            ans[i][j + 1] = a;
                            ans[i + 1][j] = b;
                            ans[i + 1][j + 1] = b;
                        }
                        else
                        {
                            char a, b;
                            if ((i / 2) % 2 == 0)
                            {
                                a = 'c';
                                b = 'd';
                            }
                            else
                            {
                                a = 'd';
                                b = 'c';
                            }
                            ans[i][j] = a;
                            ans[i][j + 1] = b;
                            ans[i + 1][j] = a;
                            ans[i + 1][j + 1] = b;
                        }
                    }
                }
            }
            else
            {
                // n奇数
                // 1列目全部埋める
                if (k < m / 2)
                {
                    Console.WriteLine("NO");
                    return;
                }

                if ((k - m / 2) % 2 != 0)
                {
                    Console.WriteLine("NO");
                    return;
                }

                for (int j = 0; j < m; j += 2)
                {
                    char a = (j / 2) % 2 == 0 ? 'a' : 'b';
                    ans[0][j] = a;
                    ans[0][j + 1] = a;
                }
                int tmp = m / 2;
                for (int i = 1; i < n; i += 2)
                {
                    for (int j = 0; j < m; j += 2)
                    {
                        if (tmp < k)
                        {
                            tmp += 2;
                            char a, b;
                            if ((j / 2) % 2 == 0)
                            {
                                a = 'c';
                                b = 'd';
                            }
                            else
                            {
                                a = 'd';
                                b = 'c';
                            }
                            ans[i][j] = a;
                            ans[i][j + 1] = a;
                            ans[i + 1][j] = b;
                            ans[i + 1][j + 1] = b;
                        }
                        else
                        {
                            char a, b;
                            if ((i / 2) % 2 == 0)
                            {
                                a = 'e';
                                b = 'f';
                            }
                            else
                            {
                                a = 'f';
                                b = 'e';
                            }
                            ans[i][j] = a;
                            ans[i][j + 1] = b;
                            ans[i + 1][j] = a;
                            ans[i + 1][j + 1] = b;
                        }
                    }
                }
            }
        }
        else
        {
            // n偶数
            // 偶数段
            if (k % 2 != 0)
            {
                Console.WriteLine("NO");
                return;
            }
            if (k > (n * (m - 1)) / 2)
            {
                Console.WriteLine("NO");
                return;
            }
            int tmp = 0;
            for (int i = 0; i < n; i += 2)
            {
                for (int j = 0; j < m - 1; j += 2)
                {
                    if (tmp < k)
                    {
                        tmp += 2;
                        char a, b;
                        if ((j / 2) % 2 == 0)
                        {
                            a = 'a';
                            b = 'b';
                        }
                        else
                        {
                            a = 'b';
                            b = 'a';
                        }
                        ans[i][j] = a;
                        ans[i][j + 1] = a;
                        ans[i + 1][j] = b;
                        ans[i + 1][j + 1] = b;
                    }
                    else
                    {
                        char a, b;
                        if ((i / 2) % 2 == 0)
                        {
                            a = 'c';
                            b = 'd';
                        }
                        else
                        {
                            a = 'd';
                            b = 'c';
                        }
                        ans[i][j] = a;
                        ans[i][j + 1] = b;
                        ans[i + 1][j] = a;
                        ans[i + 1][j + 1] = b;
                    }
                }
            }

            for (int i = 0; i < n; i += 2)
            {
                char a = (i / 2) % 2 == 0 ? 'e' : 'f';
                ans[i][m - 1] = a;
                ans[i + 1][m - 1] = a;
            }
        }

        Console.WriteLine("YES");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(new string(ans[i]));
        }
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