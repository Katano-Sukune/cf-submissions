using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        /*
         * n*nグリッド
         *
         * 下から投げる
         *
         * ぶつかったら90度右向く
         *
         * i列から投げるとA_i回ぶつかる
         *
         * 
         */

        /*
         * 0回 置かない
         *
         * 1回 ユニークな列選ぶ
         *
         * 2回 適当な行選んで 一番左　以降の1にぶつける
         *
         * 3回 ユニークな行 一番左の1にぶつける
         */

        var ls = new List<(int r, int c)>();
        int lastR = N;
        int lastC = N;

        // 0スキップ
        // 1 ユニークな行に置く キューに入れる
        // 2 キューから1個出してぶつける キュー2に入れる
        // 3 キュー2 ,キューの順位で出してユニークな行に置く、ぶつける キュー2に入れる

        var q1 = new Queue<(int r, int c)>();
        var q2 = new Queue<int>();
        for (int i = N - 1; i >= 0; i--)
        {
            switch (A[i])
            {
                case 0:
                    break;
                case 1:
                    lastR--;
                    ls.Add((lastR, i));
                    q1.Enqueue((lastR, i));
                    break;
                case 2:
                    if (q1.Count <= 0)
                    {
                        Console.WriteLine("-1");
                        return;
                    }

                    var d = q1.Dequeue();
                    ls.Add((d.r, i));
                    q2.Enqueue(i);
                    break;
                case 3:
                    lastR--;
                    if (q2.Count > 0)
                    {
                        var d2 = q2.Dequeue();
                        ls.Add((lastR, i));
                        ls.Add((lastR, d2));
                        q2.Enqueue(i);
                    }
                    else if (q1.Count > 0)
                    {
                        var d1 = q1.Dequeue();
                        ls.Add((lastR, i));
                        ls.Add((lastR, d1.c));
                        q2.Enqueue(i);
                    }
                    else
                    {
                        Console.WriteLine("-1");
                        return;
                    }
                    break;
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        Console.WriteLine(ls.Count);
        foreach ((int r, int c) in ls)
        {
            Console.WriteLine($"{r + 1} {c + 1}");
        }

        Console.Out.Flush();
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