using System;
using System.ComponentModel;
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
        int p = sc.NextInt();
        int f = sc.NextInt();

        int cntS = sc.NextInt();
        int cntW = sc.NextInt();

        int s = sc.NextInt();
        int w = sc.NextInt();

        /*
         * 自分はp持てる
         * フォロワーはf持てる
         * 
         * 剣 S 斧W
         * 
         * cntS個、cntW個
         * 
         * 2人でいくつ持てるか?
         */

        if (s > w)
        {
            int t = s;
            s = w;
            w = t;

            t = cntS;
            cntS = cntW;
            cntW = t;
        }

        if (p / s + f / s <= cntS)
        {
            Console.WriteLine(p / s + f / s);
            return;
        }

        int ans = 0;
        for (int pp = 0; pp <= cntS && pp * s <= p; pp++)
        {
            int ff = cntS - pp;
            if (ff * s > f) continue;
            // pが持てるW
            int cP = (p - pp * s) / w;
            int cF = (f - ff * s) / w;

            // Console.WriteLine($"{pp} {cntS + Math.Min(cntW, cP + cF)}");
            ans = Math.Max(ans, cntS + Math.Min(cntW, cP + cF));
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
