using System;
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
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        /*
         * n個山 山iにはa_i個石
         * 
         * T, HL Tが先手
         * 
         * 空じゃない山を選んで1個取る
         * 
         * 前のターンで選択された山は選べない
         * 
         * 自分のターンで選べないなら負け
         * 
         * 
         */

        /*
         * 山1個 T
         * 
         * 1 1 HL
         * 1 2 T
         * 
         * 
         * 
         */

        if (n == 1)
        {
            Console.WriteLine("T");
            return;
        }
        if (n == 2)
        {
            Console.WriteLine(a[0] == a[1] ? "HL" : "T");
            return;
        }

        /*
         * 偶数 HL有利
         * 奇数 T有利
         * 
         * 半分超の山がある T勝ち
         */

        // ちょうど合計半分のサブセットある
        // HL Tの逆のセットから取る　勝ち

        int sum = a.Sum();

        if (sum%2==1 || a.Any(num => num * 2 > sum))
        {
            Console.WriteLine("T");
            return;
        }
        else
        {
            Console.WriteLine("HL");
        }


        //var dp = new bool[sum + 1];
        //dp[0] = true;
        //for (int i = 0; i < n; i++)
        //{
        //    for (int j = sum; j >= 0; j--)
        //    {
        //        if (!dp[j]) continue;
        //        dp[j + a[i]] = true;
        //    }
        //}

        //if (sum % 2 == 0 && dp[sum / 2])
        //{
        //    Console.WriteLine("HL");
        //}
        //else
        //{
        //    Console.WriteLine("T");
        //}
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
