using System;
using System.Linq;
using System.Reflection;
using CompLib.Util;

public class Program
{


    public void Solve()
    {
        var sc = new Scanner();

        // 現在地x = 0. x = Aに行く
        // 区間 [l_i,r_i]には雨が降っている

        // x = x_iには重さpの傘がおいてある

        // 雨が降っている区間を通るときは傘を持っていなければならない

        // 傘を持っているとき次に進むには持っている傘の重さの合計コストがかかる

        // x = Aにたどりつけるとき最小コスト
        // たどり着けないとき-1

        int a = sc.NextInt();
        int n = sc.NextInt();
        int m = sc.NextInt();

        // rain_i x=iに雨が降っているか?
        bool[] rain = new bool[a];
        for (int i = 0; i < n; i++)
        {
            int l = sc.NextInt();
            int r = sc.NextInt();
            for (int j = l; j < r; j++) rain[j] = true;
        }

        // x = iに置いてある傘 置いてないならinf
        int[] u = new int[a + 1];
        for (int i = 0; i <= a; i++)
        {
            u[i] = int.MaxValue;
        }
        for (int i = 0; i < m; i++)
        {
            int x = sc.NextInt();
            int p = sc.NextInt();
            u[x] = Math.Min(u[x], p);
        }

        // 現在地、持っている傘(の位置)(持ってないなら a+1)
        long[,] dp = new long[a + 1, a + 2];

        for (int i = 0; i <= a; i++)
        {
            for (int j = 0; j <= a + 1; j++)
            {
                dp[i, j] = long.MaxValue;
            }
        }
        dp[0, a + 1] = 0;
        for (int i = 0; i < a; i++)
        {
            // 今持っている傘をもって次に
            for (int j = 0; j < i; j++)
            {
                if (dp[i, j] != long.MaxValue && u[j] != int.MaxValue) dp[i + 1, j] = Math.Min(dp[i + 1, j], dp[i, j] + u[j]);
            }
            // 現在地にある傘を持って、(すでに持っているなら持ち替えて移動)
            if (u[i] != int.MaxValue)
            {
                long min = dp[i, a + 1];
                for (int j = 0; j < i; j++)
                {
                    min = Math.Min(min, dp[i, j]);
                }
                if (min != long.MaxValue) dp[i + 1, i] = Math.Min(dp[i + 1, i], min + u[i]);
            }

            // 傘を持たずに移動(持っているときは捨てる)
            if (!rain[i])
            {
                for (int j = 0; j < i; j++)
                {
                    if (dp[i, j] != long.MaxValue) dp[i + 1, a + 1] = Math.Min(dp[i + 1, a + 1], dp[i, j]);
                }
                if (dp[i, a + 1] != long.MaxValue) dp[i + 1, a + 1] = Math.Min(dp[i + 1, a + 1], dp[i, a + 1]);
            }
        }

        long ans = long.MaxValue;

        for (int i = 0; i <= a + 1; i++)
        {
            ans = Math.Min(ans, dp[a, i]);
        }
        if (ans == long.MaxValue) Console.WriteLine("-1");
        else Console.WriteLine(ans);
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
