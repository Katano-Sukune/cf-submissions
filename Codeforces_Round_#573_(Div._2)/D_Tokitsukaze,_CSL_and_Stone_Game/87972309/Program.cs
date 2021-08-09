using System;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;
    private const string Sente = "sjfnb";
    private const string Gote = "cslnb";

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        /*
         * 空じゃない山を選んで1つ取る
         *
         * 全部空を渡される　負け
         * 取ったあと同じ個数の山2つ　負け
         *
         * 先手勝ち sjfnb 後手 cslnb
         */

        // 最初に同じ数が3個以上ある
        // 2個以上あるやつが2種
        {
            var map = new HashMap<int, int>();
            foreach (int i in A)
            {
                map[i]++;
            }

            int cnt = 0;
            foreach (var pair in map)
            {
                if (pair.Value >= 3)
                {
                    Console.WriteLine(Gote);
                    return;
                }

                if (pair.Value >= 2)
                {
                    if (pair.Key == 0)
                    {
                        Console.WriteLine(Gote);
                        return;
                    }

                    if (map[pair.Key - 1] > 0)
                    {
                        Console.WriteLine(Gote);
                        return;
                    }
                    cnt++;
                }
            }

            if (cnt >= 2)
            {
                Console.WriteLine(Gote);
                return;
            }
        }

        Array.Sort(A);
        long sum = 0;
        for (int i = 0; i < N; i++)
        {
            long d = A[i] - i;

            sum += d;
            if (d < 0)
            {
                Console.WriteLine(Gote);
                return;
            }
        }

        Console.WriteLine(sum % 2 == 0 ? Gote : Sente);
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections
{
    using System.Collections.Generic;

    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue o;
                return TryGetValue(key, out o) ? o : default(TValue);
            }
            set { base[key] = value; }
        }
    }
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