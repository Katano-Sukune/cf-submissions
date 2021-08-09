using System;
using System.Collections;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private long[] A;
    private long[] B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray();
        B = sc.LongArray();

        /*
         * xはA_x_iが立っていて、A_y_iが立っていないとき、yより良いと考える
         *
         * 学生のグループ 2人以上
         * グループ内に自分が誰よりも良いと考える人がいなければok
         *
         * 
         * グループのBの和最大
         */

        /*
         * いま残っている全員でグループをつくる
         * 最強がいるとき最強さんを外す
         *
         * 
         */

        // iが勝てない人
        bool[][] w = new bool[N][];
        var q = new Queue<int>[N];
        for (int i = 0; i < N; i++)
        {
            q[i] = new Queue<int>();
            w[i] = new bool[N];
        }

        for (int i = 0; i < N; i++)
        {
            int cnt = 0;
            for (int j = 0; j < N; j++)
            {
                if (i == j) continue;
                if (!C(A[i], A[j]))
                {
                    w[i][j] = true;
                    cnt++;
                }
            }

            q[cnt].Enqueue(i);
        }

        while (q[0].Count > 0)
        {
            var d = q[0].Dequeue();
            for (int i = 1; i < N; i++)
            {
                int count = q[i].Count;
                for (int j = 0; j < count; j++)
                {
                    var d2 = q[i].Dequeue();
                    if (w[d2][d])
                    {
                        w[d2][d] = false;
                        q[i - 1].Enqueue(d2);
                    }
                    else
                    {
                        q[i].Enqueue(d2);
                    }
                }
            }
        }

        long ans = 0;
        for (int i = 1; i < N; i++)
        {
            while (q[i].Count > 0)
            {
                ans += B[q[i].Dequeue()];
            }
        }

        Console.WriteLine(ans);
    }

    bool C(long a, long b)
    {
        // and
        return ((a & b) != a);
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