using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] l = new int[n];
        int[] r = new int[n];
        for (int i = 0; i < n; i++)
        {
            (l[i], r[i]) = (sc.NextInt(), sc.NextInt());
        }

        /*
         * セグメント[l,r]がn個
         * 
         * 1つ消す
         * 
         * 残りの交点の最大
         */

        var tmpL = new int[n];
        var tmpR = new int[n];
        Array.Copy(l, tmpL, n);
        Array.Copy(r, tmpR, n);

        Array.Sort(tmpL, (x, y) => y.CompareTo(x));
        Array.Sort(tmpR);

        int ans = 0;

        for (int i = 0; i < n; i++)
        {
            int ll = l[i] == tmpL[0] ? tmpL[1] : tmpL[0];
            int rr = r[i] == tmpR[0] ? tmpR[1] : tmpR[0];

            ans = Math.Max(ans, rr-ll);
        }

        Console.WriteLine(ans);
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
