using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();

        var lsB = new List<int>();
        var lsW = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (s[i] == 'B') lsB.Add(i);
            else lsW.Add(i);
        }

        List<int> p = null;
        if (lsB.Count % 2 == 0)
        {
            List<int> tmpP = new List<int>();
            for (int i = 0; i + 1 < lsB.Count; i += 2)
            {
                for (int j = lsB[i]; j < lsB[i + 1] - 1; j++)
                {
                    tmpP.Add(j + 1);
                }

                tmpP.Add(lsB[i + 1]);
            }

            p = tmpP;
        }

        if (lsW.Count % 2 == 0)
        {
            List<int> tmpP = new List<int>();
            for (int i = 0; i + 1 < lsW.Count; i += 2)
            {
                for (int j = lsW[i]; j < lsW[i + 1] - 1; j++)
                {
                    tmpP.Add(j + 1);
                }

                tmpP.Add(lsW[i + 1]);
            }

            if (p == null || tmpP.Count < p.Count) p = tmpP;
        }

        if (p == null)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(p.Count);
        if (p.Count > 0) Console.WriteLine(string.Join(" ", p));
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