using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    long L, R;
    public void Solve()
    {
        var sc = new Scanner();
        L = sc.NextLong();
        R = sc.NextLong();

        /*
         * [l,r] からa,b選ぶ
         * xor 最大
         */
        /*
         * aはRとする
         * aの立ってないbitでL以上R以下最大
         */

        // i桁目見る a L超過確定か? R未満確定か b 最大
        var dp = new long[61, 2, 2, 2, 2];
        for (int i = 0; i <= 60; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int l = 0; l < 2; l++)
                    {
                        for (int m = 0; m < 2; m++)
                        {
                            dp[i, j, k, l, m] = long.MinValue;
                        }
                    }
                }
            }
        }

        dp[60, 0, 0, 0, 0] = 0;

        for (int i = 60 - 1; i >= 0; i--)
        {
            long b = 1L << i;
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int l = 0; l < 2; l++)
                    {
                        for (int m = 0; m < 2; m++)
                        {
                            // aに1建てられる
                            // bに1建てられる

                            // aに1建てるとR未満になる、
                            // L超過になる
                            long cur = dp[i + 1, j, k, l, m];
                            if (cur == long.MinValue) continue;

                            // aに1建てる
                            if ((k == 1) || (R & b) > 0)
                            {
                                int nJ = ((j == 1) || (L & b) == 0) ? 1 : 0;
                                // bにも建てる
                                if ((m == 1) || (R & b) > 0)
                                {
                                    // 建てると aがL超過になる                    
                                    int nL = ((l == 1) || (L & b) == 0) ? 1 : 0;
                                    dp[i, nJ, k, nL, m] = Math.Max(dp[i, nJ, k, nL, m], cur);
                                }

                                // 建てない
                                if ((l == 1) || (L & b) == 0)
                                {
                                    int nM = (m == 1 || (R & b) > 0) ? 1 : 0;
                                    dp[i, nJ, k, l, nM] = Math.Max(dp[i, nJ, k, l, nM], cur | b);
                                }
                            }

                            // 建てない
                            if ((j == 1) || (L & b) == 0)
                            {
                                int nK = k | ((R & b) > 0 ? 1 : 0);
                                // b建てる
                                if ((m == 1) || (R & b) > 0)
                                {
                                    // 建てると aがL超過になる                    
                                    int nL = ((l == 1) || (L & b) == 0) ? 1 : 0;
                                    dp[i, j, nK, nL, m] = Math.Max(dp[i, j, nK, nL, m], cur | b);
                                }

                                // 建てない
                                if ((l == 1) || (L & b) == 0)
                                {
                                    int nM = (m == 1 || (R & b) > 0) ? 1 : 0;
                                    dp[i, j, nK, l, nM] = Math.Max(dp[i, j, nK, l, nM], cur);
                                }
                            }
                        }
                    }
                }
            }
        }

        long ans = long.MinValue;
        for (int j = 0; j < 2; j++)
        {
            for (int k = 0; k < 2; k++)
            {
                for (int l = 0; l < 2; l++)
                {
                    for (int m = 0; m < 2; m++)
                    {
                        ans = Math.Max(ans, dp[0, j, k, l, m]);
                    }
                }
            }
        }

        Console.WriteLine(ans);
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
