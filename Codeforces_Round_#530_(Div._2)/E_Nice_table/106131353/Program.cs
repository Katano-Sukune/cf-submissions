using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

public class Program
{
    int N, M;
    string[] S;
    const string T = "ACTG";

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        int min = int.MaxValue;
        char[][] ans = new char[N][];
        char[][] tmp = new char[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new char[M];
            tmp[i] = new char[M];
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                int k;
                for (k = 0; k < 4; k++)
                {
                    if (k == i || k == j) continue;
                    break;
                }
                int l = 6 - (i + j + k);

                {
                    // 横
                    int diff = 0;
                    for (int r = 0; r < N; r++)
                    {
                        int d1 = 0;
                        int d2 = 0;
                        for (int c = 0; c < M; c++)
                        {
                            if (r % 2 == 0)
                            {
                                if (c % 2 == 0)
                                {
                                    if (S[r][c] != T[i]) d1++;
                                    if (S[r][c] != T[j]) d2++;
                                }
                                else
                                {
                                    if (S[r][c] != T[j]) d1++;
                                    if (S[r][c] != T[i]) d2++;
                                }
                            }
                            else
                            {
                                if (c % 2 == 0)
                                {
                                    if (S[r][c] != T[k]) d1++;
                                    if (S[r][c] != T[l]) d2++;

                                }
                                else
                                {
                                    if (S[r][c] != T[l]) d1++;
                                    if (S[r][c] != T[k]) d2++;
                                }
                            }
                        }
                        for (int c = 0; c < M; c++)
                        {
                            if (r % 2 == 0)
                            {
                                tmp[r][c] = (c % 2 == 0) == (d1 < d2) ? T[i] : T[j];
                            }
                            else
                            {
                                tmp[r][c] = (c % 2 == 0) == (d1 < d2) ? T[k] : T[l];
                            }
                        }
                        diff += Math.Min(d1, d2);

                    }


                    if (diff < min)
                    {
                        // Console.WriteLine("b");
                        min = diff;
                        ans = tmp;
                        tmp = new char[N][];
                        for (int r = 0; r < N; r++)
                        {
                            tmp[r] = new char[M];
                        }
                    }
                }

                {
                    // 横
                    int diff = 0;
                    for (int c = 0; c < M; c++)
                    {
                        int d1 = 0;
                        int d2 = 0;
                        for (int r = 0; r < N; r++)
                        {
                            if (c % 2 == 0)
                            {
                                if (r % 2 == 0)
                                {
                                    if (S[r][c] != T[i]) d1++;
                                    if (S[r][c] != T[j]) d2++;
                                }
                                else
                                {
                                    if (S[r][c] != T[j]) d1++;
                                    if (S[r][c] != T[i]) d2++;
                                }
                            }
                            else
                            {
                                if (r % 2 == 0)
                                {
                                    if (S[r][c] != T[k]) d1++;
                                    if (S[r][c] != T[l]) d2++;
                                }
                                else
                                {
                                    if (S[r][c] != T[l]) d1++;
                                    if (S[r][c] != T[k]) d2++;
                                }
                            }
                        }

                        for (int r = 0; r < N; r++)
                        {
                            if (c % 2 == 0)
                            {
                                tmp[r][c] = (r % 2 == 0) == (d1 <= d2) ? T[i] : T[j];
                            }
                            else
                            {
                                tmp[r][c] = (r % 2 == 0) == (d1 <= d2) ? T[k] : T[l];
                            }
                        }
                        diff += Math.Min(d1, d2);

                    }

                    if (diff < min)
                    {
                        min = diff;
                        ans = tmp;
                        tmp = new char[N][];
                        for (int r = 0; r < N; r++)
                        {
                            tmp[r] = new char[M];
                        }
                    }
                }
            }
        }


       //  Console.WriteLine(min);

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(new string(ans[i]));
        }
        Console.Out.Flush();
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
