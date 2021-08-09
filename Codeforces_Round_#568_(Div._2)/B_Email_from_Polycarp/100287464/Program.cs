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
        string s = sc.Next();
        string t = sc.Next();

        var lsS = F(s);
        var lsT = F(t);
        if (lsS.Length != lsT.Length)
        {
            Console.WriteLine("NO");
            return;
        }

        for (int i = 0; i < lsS.Length; i++)
        {
            if (lsS[i].c != lsT[i].c)
            {
                Console.WriteLine("NO");
                return;
            }

            if (lsS[i].cnt > lsT[i].cnt)
            {
                Console.WriteLine("NO");
                return;
            }
        }

        Console.WriteLine("YES");
    }

    (char c, int cnt)[] F(string s)
    {
        var res = new List<(char c, int cnt)>();
        char c = s[0];
        int cnt = 1;
        for (int i = 1; i < s.Length; i++)
        {
            if (s[i] == c)
            {
                cnt++;
            }
            else
            {
                res.Add((c, cnt));
                c = s[i];
                cnt = 1;
            }
        }

        res.Add((c, cnt));
        return res.ToArray();
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