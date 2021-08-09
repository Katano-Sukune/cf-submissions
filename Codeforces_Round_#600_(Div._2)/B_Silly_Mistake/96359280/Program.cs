using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;
    private const int MaxA = 1000000;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        List<int> ans = new List<int>();

        bool[] f = new bool[MaxA + 1];
        int cnt = 0;
        var g = new HashSet<int>();
        int tmp = 0;
        foreach (int i in A)
        {
            tmp++;
            if (i > 0)
            {
                if (f[i] || g.Contains(i))
                {
                    Console.WriteLine("-1");
                    return;
                }

                f[i] = true;
                g.Add(i);
                cnt++;
            }
            else
            {
                int ii = -i;
                if (!f[ii])
                {
                    Console.WriteLine("-1");
                    return;
                }

                f[ii] = false;
                cnt--;

                if (cnt == 0)
                {
                    g.Clear();
                    ans.Add(tmp);
                    tmp = 0;
                }
            }
        }

        if (cnt != 0)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join(" ", ans));
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