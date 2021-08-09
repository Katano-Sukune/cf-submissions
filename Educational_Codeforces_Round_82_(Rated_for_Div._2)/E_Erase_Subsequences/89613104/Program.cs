using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        string s = sc.Next();
        string t = sc.Next();

        // 文字列sからpをつくる

        // 2回まで
        // sの部分列(連続じゃなくても良い) s_{i_1} .. s_{i_k} を選ぶ
        // 選んだ部分列をsから消す
        // pの末尾にその順で追加する

        // sからtが作れるか?

        /*
         * tを前半、後半に分ける
         *
         * 前半、後半が部分列として存在するか?
         *
         *
         */

        for (int i = 0; i < t.Length; i++)
        {
            // 前半i文字、後半|T|-i文字
            // 前半j文字構成できるとき、後半構成できる最大
            var dp = new int[s.Length + 1, i + 1];
            for (int j = 0; j <= s.Length; j++)
            {
                for (int k = 0; k <= i; k++)
                {
                    dp[j, k] = -1;
                }
            }

            dp[0, 0] = 0;
            for (int j = 0; j < s.Length; j++)
            {
                // 前半
                for (int k = i - 1; k >= 0; k--)
                {
                    if (t[k] == s[j])
                    {
                        dp[j + 1, k + 1] = Math.Max(dp[j + 1, k + 1], dp[j, k]);
                    }
                }

                // 後半
                for (int k = 0; k <= i; k++)
                {
                    if (dp[j, k] == -1) continue;
                    if (dp[j, k] == t.Length - i) continue;
                    if (t[i + dp[j, k]] == s[j]) dp[j + 1, k] = Math.Max(dp[j + 1, k], dp[j, k] + 1);
                }

                // 無視
                for (int k = 0; k <= i; k++)
                {
                    dp[j + 1, k] = Math.Max(dp[j + 1, k], dp[j, k]);
                }
            }

            if (dp[s.Length, i] == t.Length - i)
            {
                Console.WriteLine("YES");
                return;
            }
        }

        Console.WriteLine("NO");
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