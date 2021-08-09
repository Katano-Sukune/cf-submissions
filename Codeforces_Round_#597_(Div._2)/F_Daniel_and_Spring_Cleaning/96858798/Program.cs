using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private const int Len = 30;

    void Q(Scanner sc)
    {
        int l = sc.NextInt();
        int r = sc.NextInt();

        int[] arL = new int[Len];
        int[] arR = new int[Len];
        for (int i = 0; i < Len; i++)
        {
            arL[i] = l % 2;
            arR[i] = r % 2;
            l /= 2;
            r /= 2;
        }
        // a+b = a^b
        // L <= a,b <= R
        // 何通りか?

        // 上位i桁、aがL超過確定、R未満確定、 b 何通りか?
        var dp = new long[Len + 1, 2, 2, 2, 2];
        dp[Len, 0, 0, 0, 0] = 1;
        for (int i = Len - 1; i >= 0; i--)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int m = 0; m < 2; m++)
                    {
                        for (int n = 0; n < 2; n++)
                        {
                            long cur = dp[i + 1, j, k, m, n];

                            for (int o = 0; o < 2; o++)
                            {
                                for (int p = 0; p < 2; p++)
                                {
                                    if (o == 1 && p == 1) continue;
                                    if (j == 0 && arL[i] == 1 && o == 0) continue;
                                    if (m == 0 && arL[i] == 1 && p == 0) continue;
                                    if (k == 0 && arR[i] == 0 && o == 1) continue;
                                    if (n == 0 && arR[i] == 0 && p == 1) continue;

                                    int nJ = (j == 1 || (arL[i] == 0 && o == 1)) ? 1 : 0;
                                    int nK = (k == 1 || (arR[i] == 1 && o == 0)) ? 1 : 0;

                                    int nM = (m == 1 || (arL[i] == 0 && p == 1)) ? 1 : 0;
                                    int nN = (n == 1 || (arR[i] == 1 && p == 0)) ? 1 : 0;

                                    dp[i, nJ, nK, nM, nN] += cur;
                                }
                            }
                        }
                    }
                }
            }
        }

        
        long ans = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int m = 0; m < 2; m++)
                    {
                        ans += dp[0, i, j, k, m];
                    }
                }
            }
        }

        Console.WriteLine(ans);
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