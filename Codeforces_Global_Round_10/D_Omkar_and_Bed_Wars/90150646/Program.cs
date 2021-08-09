using System;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        var sc = new Scanner();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();

    }



    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        string s = sc.Next();

        /*
         * n人
         * 円周上にならんでる
         * 
         * 左右を攻撃できる
         * 
         * あるプレイヤーが1人から攻撃されてる
         * してるやつを攻撃
         * 
         * 0 or 2から攻撃
         * どっちでも
         * 
         * 変更する最小
         */

        /*
         * rlrlrl
         * 
         * r?l
         * 
         * r r l
         * 
         * r l間は1つ
         */

        /*
         * 0 1 2番目をrにして開始
         * 
         * 
         */
        int ans = int.MaxValue;
        for (int i = 0; i < 4; i++)
        {
            ans = Math.Min(ans, C(s, i));
        }
        Console.WriteLine(ans);
    }

    int C(string s, int f)
    {
        int[] dp = new int[s.Length + 1];
        for (int i = 0; i <= s.Length; i++)
        {
            dp[i] = int.MaxValue;
        }
        dp[0] = 0;
        for (int i = 1; i <= s.Length; i++)
        {
            if (i >= 2 && dp[i - 2] != int.MaxValue)
            {
                int t = dp[i - 2] + (s[(i + f - 2) % s.Length] == 'R' ? 0 : 1) + (s[(i + f - 1) % s.Length] == 'L' ? 0 : 1);
                dp[i] = Math.Min(dp[i], t);

            }

            if (i >= 3 && dp[i - 3] != int.MaxValue)
            {
                int t = dp[i - 3] + (s[(i + f - 3) % s.Length] == 'R' ? 0 : 1) + (s[(i + f - 1) % s.Length] == 'L' ? 0 : 1);
                dp[i] = Math.Min(dp[i], t);
            }

            if (i >= 4 && dp[i - 4] != int.MaxValue)
            {
                int t = dp[i - 4] + (s[(i + f - 4) % s.Length] == 'R' ? 0 : 1) + (s[(i + f - 3) % s.Length] == 'R' ? 0 : 1) + (s[(i + f - 2) % s.Length] == 'L' ? 0 : 1) + (s[(i + f - 1) % s.Length] == 'L' ? 0 : 1);
                dp[i] = Math.Min(dp[i], t);
            }
        }

        return dp[s.Length];
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
