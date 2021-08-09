using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    int N;
    string S, T;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();
        T = sc.Next();

        // 同じcharが無い
        // aaaaaa...bbbbbb....ccccccc
        if (S[0] != S[1] && T[0] != T[1])
        {
            // 境目
            for (char a = 'a'; a <= 'c'; a++)
            {
                for (char b = 'a'; b <= 'c'; b++)
                {
                    if (a == b) continue;
                    for (char c = 'a'; c <= 'c'; c++)
                    {
                        if (a == c || b == c) continue;
                        if (S[0] == a && S[1] == b) continue;
                        if (S[0] == b && S[1] == c) continue;
                        if (T[0] == a && T[1] == b) continue;
                        if (T[0] == b && T[1] == c) continue;

                        Console.WriteLine("YES");
                        Console.WriteLine($"{new string(a, N)}{new string(b, N)}{new string(c, N)}");
                        return;
                    }
                }
            }
        }
        // ある
        // abcの並べ替え
        else
        {
            for (char a = 'a'; a <= 'c'; a++)
            {
                for (char b = 'a'; b <= 'c'; b++)
                {
                    if (a == b) continue;
                    for (char c = 'a'; c <= 'c'; c++)
                    {
                        if (a == c || b == c) continue;
                        if (S[0] == a && S[1] == b) continue;
                        if (S[0] == b && S[1] == c) continue;
                        if (T[0] == a && T[1] == b) continue;
                        if (T[0] == b && T[1] == c) continue;
                        if (S[0] == c && S[1] == a) continue;
                        if (T[0] == c && T[1] == a) continue;
                        Console.WriteLine("YES");
                        var sb = new StringBuilder();
                        for (int i = 0; i < N; i++)
                        {
                            sb.Append(a);
                            sb.Append(b);
                            sb.Append(c);
                        }
                        Console.WriteLine(sb);
                        return;
                    }
                }
            }
        }
        Console.WriteLine("NO");
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct Edge
{
    public int To, W;
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
