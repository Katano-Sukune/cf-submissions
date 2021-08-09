using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int k = sc.NextInt();
        string s = sc.Next();

        // 2つの連続Wの間のL
        List<int> l = new List<int>();

        // 連続Wの個数
        int cnt = 0;

        // Wの個数
        int cntW = 0;

        int last = -1;
        int cntL = 0;
        for (int i = 0; i < n; i++)
        {
            if (s[i] == 'W')
            {
                cntW++;
                if (i == 0 || s[i - 1] == 'L')
                {
                    if (last != -1)
                    {
                        l.Add(i - last - 1);
                    }

                    cnt++;
                }

                last = i;
            }
            else
            {
                cntL++;
            }
        }

        if (cntW == 0)
        {
            if (k == 0)
            {
                Console.WriteLine(0);
                return;
            }
            Console.WriteLine(Math.Min(cntL, k)*2-1);
            return;
        }

        l.Sort();
        long score = cntW * 2 - cnt;

        for (int i = 0; i < l.Count && k >= l[i]; i++)
        {
            score += l[i] * 2 + 1;
            cntL -= l[i];
            k -= l[i];
        }

        score += 2 * Math.Min(cntL, k);
        Console.WriteLine(score);
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