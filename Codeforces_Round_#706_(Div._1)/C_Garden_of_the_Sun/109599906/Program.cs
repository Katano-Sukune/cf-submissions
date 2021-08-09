using System;
using System.Linq;
using System.Runtime.CompilerServices;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
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

        char[][] ans = new char[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new char[M];
            for (int j = 0; j < M; j++)
            {
                ans[i][j] = S[i][j];
            }
        }

        if (N == 1 || N == 2)
        {
            for (int j = 0; j < M; j++)
            {
                ans[0][j] = 'X';
            }
        }
        else if (N == 3)
        {
            for (int j = 0; j < M; j++)
            {
                ans[1][j] = 'X';
            }
        }
        else if (M == 1 || M == 2)
        {
            for (int i = 0; i < N; i++)
            {
                ans[i][0] = 'X';
            }
        }
        else if (M == 3)
        {
            for (int i = 0; i < N; i++)
            {
                ans[i][1] = 'X';
            }
        }
        else
        {
            // N <= 4 && M<=4
            if (N % 3 == 0)
            {
                for (int i = 1; i < N; i += 3)
                {
                    for (int j = 0; j < M; j++)
                    {
                        ans[i][j] = 'X';
                    }
                }

                for (int i = 4; i < N; i += 3)
                {
                    bool flag = false;
                    for (int j = 0; j < M; j++)
                    {
                        if (S[i - 1][j] == 'X' || S[i - 2][j] == 'X')
                        {
                            ans[i - 1][j] = 'X';
                            ans[i - 2][j] = 'X';
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        ans[i - 1][0] = 'X';
                        ans[i - 2][0] = 'X';
                    }
                }
            }
            else
            {
                for (int i = 0; i < N; i += 3)
                {
                    for (int j = 0; j < M; j++)
                    {
                        ans[i][j] = 'X';
                    }
                }

                for (int i = 3; i < N; i += 3)
                {
                    bool flag = false;
                    for (int j = 0; j < M; j++)
                    {
                        if (S[i - 1][j] == 'X' || S[i - 2][j] == 'X')
                        {
                            ans[i - 1][j] = 'X';
                            ans[i - 2][j] = 'X';
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        ans[i - 1][0] = 'X';
                        ans[i - 2][0] = 'X';
                    }
                }
            }
        }

        for (int i = 0; i < N; i++)
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