using CompLib.Util;
using System;

public class Program
{
    int N, A, R, M;
    int[] H;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.NextInt();
        R = sc.NextInt();
        M = sc.NextInt();
        H = sc.IntArray();

        // hをぜんぶ同じにする
        // コストAで1つ加算
        // Rで1つ減算
        // Mで2つ i,j 選んで iに加算、j減算

        long ans = 0;
        foreach (int i in H)
        {
            ans += i;
        }

        int ok = 1;
        int ng = 1000000000;

        // Q(i-1) > Q(i)な i最大
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            if (Q(mid - 1) > Q(mid)) ok = mid;
            else ng = mid;
        }
        Console.WriteLine(Q(ok));
    }

    long Q(int target)
    {
        long aa = 0;
        long rr = 0;
        for (int i = 0; i < N; i++)
        {
            if (H[i] < target) aa += target - H[i];
            if (H[i] > target) rr += H[i] - target;
        }

        if (A + R <= M)
        {
            return (long)aa * A + (long)rr * R;
        }
        if (aa >= rr)
        {
            return (long)(aa - rr) * A + (long)rr * M;
        }
        else
        {
            return (long)(rr - aa) * R + (long)aa * M;
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
