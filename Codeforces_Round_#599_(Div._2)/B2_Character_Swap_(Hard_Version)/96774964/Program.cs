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
        char[] s = sc.NextCharArray();
        char[] t = sc.NextCharArray();

        List<(int i, int j)> ls = new List<(int i, int j)>();
        for (int i = 0; i < n; i++)
        {
            if (s[i] == t[i]) continue;
            // t側に t_iがある
            bool f = true;
            for (int j = i + 1; f && j < n; j++)
            {
                // t側にt_iがある
                if (t[i] == t[j])
                {
                    ls.Add((i, j));
                    (s[i], t[j]) = (t[j], s[i]);
                    f = false;
                }
                else if (t[i] == s[j])
                {
                    ls.Add((j, j));
                    (s[j], t[j]) = (t[j], s[j]);
                    ls.Add((i, j));
                    (s[i], t[j]) = (t[j], s[i]);
                    f = false;
                }
            }

            if (f)
            {
                Console.WriteLine("No");
                return;
            }
        }

        Console.WriteLine("Yes");
        Console.WriteLine(ls.Count);
        foreach ((int i, int j) in ls)
        {
            Console.WriteLine($"{i + 1} {j + 1}");
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