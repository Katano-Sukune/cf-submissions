using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;

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
        string[] s = new string[3];
        for (int i = 0; i < 3; i++)
        {
            s[i] = sc.Next();
        }

        // 2nのbit列3つ
        // 少なくとも2つのbit列が部分列の3nのbit列

        int[] cnt0 = new int[3];
        int[] cnt1 = new int[3];
        for (int i = 0; i < 3; i++)
        {
            cnt0[i] = s[i].Count(ch => ch == '0');
            cnt1[i] = 2 * n - cnt0[i];
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = i + 1; j < 3; j++)
            {
                if (cnt0[i] >= n && cnt0[j] >= n)
                {
                    int idxI = 0;
                    int idxJ = 0;
                    var sb = new StringBuilder();
                    for (int k = 0; k < n; k++)
                    {
                        while (s[i][idxI] != '0')
                        {
                            sb.Append('1');
                            idxI++;
                        }
                        while (s[j][idxJ] != '0')
                        {
                            sb.Append('1');
                            idxJ++;
                        }
                        sb.Append('0');
                        idxI++;
                        idxJ++;
                    }

                    while (idxI < 2 * n) sb.Append(s[i][idxI++]);
                    while (idxJ < 2 * n) sb.Append(s[j][idxJ++]);
                    Console.WriteLine(sb);
                    return;
                }

                if (cnt1[i] >= n && cnt1[j] >= n)
                {
                    int idxI = 0;
                    int idxJ = 0;
                    var sb = new StringBuilder();
                    for (int k = 0; k < n; k++)
                    {
                        while (s[i][idxI] != '1')
                        {
                            sb.Append('0');
                            idxI++;
                        }
                        while (s[j][idxJ] != '1')
                        {
                            sb.Append('0');
                            idxJ++;
                        }
                        sb.Append('1');
                        idxI++;
                        idxJ++;
                    }

                    while (idxI < 2 * n) sb.Append(s[i][idxI++]);
                    while (idxJ < 2 * n) sb.Append(s[j][idxJ++]);
                    Console.WriteLine(sb);
                    return;
                }
            }
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
