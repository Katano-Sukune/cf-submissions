using System;
using System.Collections.Generic;
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

    void Q(Scanner sc)
    {
        string s = sc.Next();
        int n = s.Length;

        /*
         * sから文字消して部分文字列にone, twoが無いようにする
         *
         * 消す最小数、消す位置
         */

        // iまで見る、one,two 消す最小
        var dp = new int[n + 1, 3, 3];

        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    dp[i, j, k] = int.MaxValue;
                }
            }
        }

        dp[0, 0, 0] = 0;
        const string strOne = "one";
        const string strTwo = "two";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (dp[i, j, k] == int.MaxValue) continue;
                    dp[i + 1, j, k] = Math.Min(dp[i + 1, j, k], dp[i, j, k] + 1);
                    bool o = s[i] == strOne[j];
                    bool t = s[i] == strTwo[k];
                    int nJ = o ? j + 1 : s[i] == strOne[0] ? 1 : 0;
                    int nK = t ? k + 1 : s[i] == strTwo[0] ? 1 : 0;
                    if (nJ < 3 && nK < 3) dp[i + 1, nJ, nK] = Math.Min(dp[i + 1, nJ, nK], dp[i, j, k]);
                }
            }
        }

        int r = int.MaxValue;
        int ptrJ = -1;
        int ptrK = -1;

        for (int j = 0; j < 3; j++)
        {
            for (int k = 0; k < 3; k++)
            {
                if (dp[n, j, k] < r)
                {
                    ptrJ = j;
                    ptrK = k;
                    r = dp[n, j, k];
                }
            }
        }

        List<int> ls = new List<int>();
        for (int i = n - 1; i >= 0; i--)
        {
            if (dp[i, ptrJ, ptrK] == r - 1)
            {
                ls.Add(i + 1);
                r--;
                continue;
            }

            bool f = true;
            for (int j = 0; f && j < 3; j++)
            {
                for (int k = 0; f && k < 3; k++)
                {
                    if (dp[i, j, k] == int.MaxValue) continue;
                    bool o = s[i] == strOne[j];
                    bool t = s[i] == strTwo[k];
                    int nJ = o ? j + 1 : s[i] == strOne[0] ? 1 : 0;
                    int nK = t ? k + 1 : s[i] == strTwo[0] ? 1 : 0;
                    if (nJ == ptrJ && nK == ptrK && dp[i, j, k] == dp[i + 1, nJ, nK])
                    {
                        ptrJ = j;
                        ptrK = k;
                        f = false;
                    }
                }
            }
        }


        Console.WriteLine(ls.Count);
        Console.WriteLine(string.Join(" ", ls));
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