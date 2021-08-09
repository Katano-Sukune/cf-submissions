using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.Next()));
        }
        Console.Write(sb);
    }

    string Q(int n, string s)
    {
        // <なら s_i < s_i+1
        // > s_i > s_i+1

        // lis最小、最大　要素 1 <= s_i <= n 順列

        // min
        int[] min = new int[n];
        {
            int i;
            for (i = 0; i < n; i++)
            {
                min[i] = n - i;
            }
            bool flag = false;
            int cnt = 0;

            for (i = 0; i < n - 1; i++)
            {
                if (s[i] == '<')
                {
                    if (!flag)
                    {
                        flag = true;
                        cnt = 0;
                    }
                    cnt++;
                }
                else if(flag)
                {

                    int l = i - cnt;
                    // [l,r]を反転
                    // Console.WriteLine($"aaaa {l} {i}");
                    for (int j = 0; j < (i - l + 1) / 2; j++)
                    {
                        int t = min[j + l];
                        min[j + l] = min[i - j];
                        min[i - j] = t;
                    }

                    flag = false;
                }
            }
            if (flag)
            {
                int l = i - cnt;
                for (int j = 0; j < (i - l + 1) / 2; j++)
                {
                    int t = min[j + l];
                    min[j + l] = min[i - j];
                    min[i - j] = t;
                }
            }
        }





        // <の数+1

        // max
        int[] max = new int[n];
        {
            int i;
            for (i = 0; i < n; i++)
            {
                max[i] = i + 1;
            }
            bool flag = false;
            int cnt = 0;

            for (i = 0; i < n - 1; i++)
            {
                if (s[i] == '>')
                {
                    if (!flag)
                    {
                        flag = true;
                        cnt = 0;
                    }
                    cnt++;
                }
                else if(flag)
                {
                    int l = i - cnt;
                    // [l,r]を反転

                    for (int j = 0; j < (i - l + 1) / 2; j++)
                    {
                        int t = max[j + l];
                        max[j + l] = max[i - j];
                        max[i - j] = t;
                    }

                    flag = false;
                }
            }
            if (flag)
            {
                int l = i - cnt;
                for (int j = 0; j < (i - l + 1) / 2; j++)
                {
                    int t = max[j + l];
                    max[j + l] = max[i - j];
                    max[i - j] = t;
                }
            }

        }

        return $"{string.Join(" ", min)}\n{string.Join(" ", max)}";
    }


    public static void Main(string[] args) => new Program().Solve();
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
