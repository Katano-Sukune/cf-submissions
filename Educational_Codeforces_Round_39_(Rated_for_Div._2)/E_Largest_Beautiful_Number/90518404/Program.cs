using System;
using System.Collections.Generic;
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
        string s = sc.Next();
        /*
         * 先頭0以外10進整数
         * 
         * 偶数桁
         * 並び替えると回文になる
         * 
         * s未満
         */

        // 奇数桁
        // 999999
        if (s.Length % 2 == 1)
        {
            Console.WriteLine(new string('9', s.Length - 1));
            return;
        }

        /*
         * 最小 1000000....00001
         * 100000000001なら99999
         */
        if (s[0] == '1' && (s[s.Length-1] =='0' || s[s.Length - 1] == '1'))
        {
            bool f = true;
            for (int i = 1; f && i < s.Length - 1; i++)
            {
                f &= s[i] == '0';
            }

            if (f)
            {
                Console.WriteLine(new string('9', s.Length - 2));
                return;
            }
        }

        // 上位i桁一致
        // i+1桁　小さい
        // のこりいいかんじにする
        // 可能ならi最大
        int[] cnt = new int[10];
        for (int i = 0; i < s.Length; i++)
        {
            cnt[s[i] - '0']++;
        }

        for (int i = s.Length - 1; i >= 0; i--)
        {
            int d = s[i] - '0';
            cnt[d]--;

            for (int j = d - 1; j >= 0; j--)
            {
                if (i == 0 && j == 0) continue;
                cnt[j]++;
                // i桁目をjにする

                // 999999... 奇数個の桁を降順
                // 奇数個の桁
                int c = 0;
                for (int k = 0; k <= 9; k++)
                {
                    if (cnt[k] % 2 == 1) c++;
                }

                // 下N-i-1桁使える
                if (c <= s.Length - i - 1)
                {
                    var ls = new List<char>();
                    for (int k = 9; k >= 0; k--)
                    {
                        if (cnt[k] % 2 == 1) ls.Add((char)(k + '0'));
                    }
                    var ans = s.ToCharArray();
                    ans[i] = (char)(j + '0');
                    for (int k = i + 1; k + c < s.Length; k++)
                    {
                        ans[k] = '9';
                    }

                    for (int k = s.Length - c; k < s.Length; k++)
                    {
                        ans[k] = ls[k - (s.Length - c)];
                    }
                    Console.WriteLine(new string(ans));
                    return;
                }

                cnt[j]--;
            }
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
