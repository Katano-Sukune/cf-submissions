using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        /*
         * |x|,|y|間が　Arrayland
         * 
         * |x-y|,|x+y|間が Vectorland
         * 
         * Aland ∈ Vland 
         * 
         * x,yの値
         * 
         * 答えの候補a
         * 
         * a内で条件を満たすペアの個数
         */

        /*
         * x正 y正
         * 
         * x < y
         * 
         * y-x, x+y
         * 
         * x < y <= 2x
         */

        /*
         * x負 y正
         * 
         * |x| < |y|
         * 
         * y+x <= -x
         * y <= -2x
         *
         * |x| > |y|
         * 
         * 
         */

        /*
         * 絶対値小さい順に
         * 
         * 大きいやつとペアにする
         * 
         * 
         */

        Array.Sort(a, (l, r) => Math.Abs(l).CompareTo(Math.Abs(r)));
        var plus = new List<int>();
        var minus = new List<int>();

        long ans = 0;

        foreach (var i in a)
        {

            if (i >= 0)
            {

                // iと正をペアにする
                int ng = -1;
                int ok = plus.Count;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    // midとiをペアにできるか?
                    if (i - plus[mid] <= plus[mid]) ok = mid;
                    else ng = mid;
                }


                ans += plus.Count - ok;

                // iと負をペアにする
                ng = -1;
                ok = minus.Count;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (i + minus[mid] <= -minus[mid]) ok = mid;
                    else ng = mid;
                }


                ans += minus.Count - ok;


                plus.Add(i);
            }
            else
            {
                // 
                int ng = -1;
                int ok = plus.Count;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (-i - plus[mid] <= plus[mid]) ok = mid;
                    else ng = mid;
                }
                ans += plus.Count - ok;

                ng = -1;
                ok = minus.Count;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (-i + minus[mid] <= -minus[mid]) ok = mid;
                    else ng = mid;
                }

                ans += minus.Count - ok;

                minus.Add(i);
            }
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
