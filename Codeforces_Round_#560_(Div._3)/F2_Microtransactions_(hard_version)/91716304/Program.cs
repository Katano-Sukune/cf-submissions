using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] K;

    HashSet<int>[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        K = sc.IntArray();
        S = new HashSet<int>[400001];
        for (int i = 0; i <= 400000; i++)
        {
            S[i] = new HashSet<int>();
        }
        for (int i = 0; i < M; i++)
        {
            int d = sc.NextInt();
            int t = sc.NextInt() - 1;
            S[d].Add(t);
        }

        /*
         * 毎日1B獲得
         * 
         * n種類
         * 通常2B 
         * セール1B
         * 
         * タイプiをK_i個
         * 
         * m個
         * 
         * タイプt_iがd_iにセール
         * 
         * 全部買う　最小日数
         */

        /*
         * i日で達成できるか?
         * 
         * 最後から見る
         * 
         */
        int ok = 400000;
        int ng = 0;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }
        Console.WriteLine(ok);

    }

    // t日でできるか?
    bool F(int t)
    {
        int[] tmp = new int[N];
        Array.Copy(K, tmp, N);

        int[] cnt = new int[N];
        for (int i = 1; i <= t; i++)
        {
            foreach (int type in S[i])
            {
                cnt[type]++;
            }
        }
        int b = 0;
        for (int i = 1; i <= t; i++)
        {
            b++;
            foreach (var type in S[i])
            {
                if (cnt[type] == 1)
                {
                    int min = Math.Min(b, tmp[type]);
                    b -= min;
                    tmp[type] -= min;
                }
                cnt[type]--;
            }

        }

        long sum = 0;
        foreach (var i in tmp)
        {
            sum += i;
        }

        return sum * 2 <= b;
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
