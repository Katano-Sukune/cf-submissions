using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private string S, T;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();
        T = sc.Next();

        // Sの部分文字列Tを選ぶ
        // Tを 1つ回して置き換える
        // Tに一致させるには何回操作すればいいか?

        // S,T一致してるやつ そのまま

        // 一致してない右端 Tの左端 
        int t = 0;

        List<int> ls = new List<int>();

        for (int i = 0; i < N; i++)
        {
            if (S[i] != T[i])
            {
                if (S[i] == '0') t++;
                else t--;
                ls.Add(i);
            }
        }

        if (t != 0)
        {
            Console.WriteLine("-1");
            return;
        }

        int zero = 0;
        int one = 0;
        foreach (int i in ls)
        {
            if (S[i] == '0')
            {
                if (one > 0)
                {
                    one--;
                    zero++;
                }
                else
                {
                    zero++;
                }
            }
            else
            {
                if (zero > 0)
                {
                    zero--;
                    one++;
                }
                else
                {
                    one++;
                }
            }
        }

        Console.WriteLine(one + zero);
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

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