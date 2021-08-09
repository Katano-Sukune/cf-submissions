using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, K;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();

        /*
         * Sの部分列tを取る集合に加える
         * コスト n-|t|
         * 重複不可
         * 
         * サイズkの集合つくる最小コスト
         */

        /*
         * t = s 1個
         * 
         * |t| = |s|-1 N個
         */

        var hs = new HashSet<string>(StringComparer.Ordinal);
        hs.Add(S);
        var q = new Queue<string>();
        q.Enqueue(S);

        long ans = 0;
        for (int i = 0; i < K; i++)
        {
            if (q.Count <= 0)
            {
                Console.WriteLine("-1");
                return;
            }

            var d = q.Dequeue();
            ans += N - d.Length;
            if (d.Length <= 0) continue;
            char[] tmp = new char[d.Length - 1];
            for (int j = 0; j < d.Length - 1; j++)
            {
                tmp[j] = d[j + 1];
            }

            for (int j = 0; j < d.Length; j++)
            {
                var str = new string(tmp);
                if (hs.Add(str))
                {
                    q.Enqueue(str);
                }
                if (j < d.Length - 1) tmp[j] = d[j];
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
