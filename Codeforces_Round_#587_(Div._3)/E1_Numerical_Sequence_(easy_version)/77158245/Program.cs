using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            sb.AppendLine(Query(sc.NextLong()));
        }
        Console.Write(sb);
    }

    string Query(long k)
    {
        if (k == 1) return "1";
        // 何番目のブロックにあるか?

        long ok = 0;
        long ng = 1000000000;
        while (ng - ok > 1)
        {
            long mid = (ok + ng) / 2;
            if (L(mid) < k) ok = mid;
            else ng = mid;
        }

        long kk = k - L(ok);

        // ngブロックのkk番目

        long ok2 = 0;
        long ng2 = ng;
        while (ng2 - ok2 > 1)
        {
            long mid = (ok2 + ng2) / 2;
            if (M(mid) < kk) ok2 = mid;
            else ng2 = mid;
        }

        long kkk = kk - M(ok2);
        // ng2のkkk番目
        return (ng2.ToString()[(int)(kkk - 1)]).ToString();
    }

    // 1..nをつなげたときの長さ
    long M(long n)
    {
        long result = 0;
        long p = 1;
        while (p <= n)
        {
            long c = n - p + 1;
            result += c;
            p *= 10;
        }
        return result;
    }

    // 1~nブロックの合計長さいくつか?
    long L(long n)
    {
        // 1桁が出てくる
        // 全部
        // n(n-1)/2

        // 2桁が出てくる
        // 10~n
        // 3
        // 100~n
        long result = 0;
        long p = 1;
        while (p <= n)
        {
            long c = n - p + 1;
            result += c * (c + 1) / 2;
            p *= 10;
        }
        return result;
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
