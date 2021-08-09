using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.IntArray();

        /*
         * 配列、
         * 1回だけ現れる数 nice
         * 
         * SをA,Bに分ける
         * 
         * AとBのniceの個数同じにする
         * 
         */

        /*
         * 1個 片方に+1
         * 2個 両方に+1 両方0
         * 3以上 片方+1, 両方0
         */

        List<int>[] ls = new List<int>[101];
        for (int i = 0; i <= 100; i++)
        {
            ls[i] = new List<int>();
        }

        for (int i = 0; i < N; i++)
        {
            ls[S[i]].Add(i);
        }

        int a = 0;
        int b = 0;
        for (int i = 0; i <= 100; i++)
        {
            if (ls[i].Count == 1) a++;
            if (ls[i].Count > 2) b++;
        }
        // 1個だけ　奇数個 NO
        if (a % 2 == 1 && b == 0)
        {
            Console.WriteLine("NO");
            return;
        }

        // 1個のやつa,bに振り分ける
        bool f = false;
        bool f2 = a % 2 == 1;
        char[] ans = new char[N];
        for (int i = 0; i <= 100; i++)
        {
            if (ls[i].Count == 1)
            {
                ans[ls[i][0]] = f ? 'A' : 'B';
                f ^= true;
            }
            else if (ls[i].Count == 2)
            {
                ans[ls[i][0]] = 'A';
                ans[ls[i][1]] = 'A';
            }
            else if (ls[i].Count > 2)
            {
                if (f2)
                {
                    // 最初A
                    ans[ls[i][0]] = 'A';
                    for(int j = 1;j < ls[i].Count; j++)
                    {
                        ans[ls[i][j]] = 'B';
                    }
                    f2 = false;
                }
                else
                {
                    for (int j = 0; j < ls[i].Count; j++)
                    {
                        ans[ls[i][j]] = 'B';
                    }
                }
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(new string(ans));
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
