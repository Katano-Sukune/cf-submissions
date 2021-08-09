using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        /*
         * カードn枚 1~n
         *
         * 空カードn枚
         *
         * 積んで n枚引く
         *
         * 2つのカード集合 A,B
         *
         * play
         *
         * Aのカード1枚選ぶ
         * Bの一番下に入れる
         * Bの一番上のカードを取る
         *
         * Bを1~nソートにする
         * 操作最小
         */

        var index = new int[N + 1];
        var hs = new HashSet<int>();
        for (int i = 0; i < N; i++)
        {
            if (A[i] != 0) hs.Add(i);
            index[B[i]] = i + 1;
        }

        // Bのラストが123....になっているか?
        int cnt = 0;
        for (int i = 0; i < N; i++)
        {
            if (B[i] == cnt + 1)
            {
                cnt++;
            }
            else
            {
                cnt = 0;
            }
        }

        if (cnt > 0)
        {
            bool f = true;
            int a = 0;
            for (int t = cnt + 1; t <= N; t++)
            {
                // カードtがa手以内に手に入るか?
                if (index[t] <= a)
                {
                    a++;
                }
                else
                {
                    f = false;
                    break;
                }
            }

            if (f)
            {
                Console.WriteLine(a);
                return;
            }
        }

        {
            int a = 0;
            for (int t = 1; t <= N; t++)
            {
                // カードtがa手以内に手に入る
                if (index[t] <= a)
                {
                    a++;
                }
                else
                {
                    a = index[t] + 1;
                }
            }

            Console.WriteLine(a);
        }
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