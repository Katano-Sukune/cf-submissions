using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {

        var a = sc.IntArray();
        var b = sc.IntArray();

        /*
         * n配列 a,b
         * 
         * 要素は0,1,2 個数は x,y,z
         * 
         * c_i = 
         * 
         * cの総和最大
         */

        // aの2 bの1と

        // aの1 bの0か1と

        // aの0 自由

        long ans = 0;

        // 2 1
        // 1 1
        // 1 0
        // 2 2
        // 0 2
        // 2 0
        // 0 1
        // 0 0
        // 1 2

        var ta = new int[] { 2, 1, 1, 2, 0, 2, 0, 0, 1 };
        var tb = new int[] { 1, 1, 0, 2, 2, 0, 1, 0, 2 };

        for (int i = 0; i < 9; i++)
        {
            int min = Math.Min(a[ta[i]], b[tb[i]]);
            a[ta[i]] -= min;
            b[tb[i]] -= min;
            if (i == 0) ans += 2 * min;
            if (i == 8) ans -= 2 * min;
        }

        Console.WriteLine(ans);
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
