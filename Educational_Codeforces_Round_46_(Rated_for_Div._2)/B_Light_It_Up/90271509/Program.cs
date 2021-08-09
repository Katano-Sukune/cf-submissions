using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int[] a = new int[n + 2];
        for (int i = 1; i <= n; i++)
        {
            a[i] = sc.NextInt();
        }
        a[n + 1] = m;

        // 時刻A_iに状態が反転
        // aに1つ整数追加

        // ついてる時間最大

        // 消えてる時間 a[i] - a[i+1]に追加
        // a[i]+1に追加

        int[] b = new int[n + 1];
        for (int i = n - 1; i >= 0; i--)
        {
            // i-i+1に追加
            // i+1からの消えてる時間合計
            b[i] = b[i + 1];
            if (i % 2 == 0)
            {
                b[i] += a[i + 2] - a[i + 1];
            }
        }

        int ans = int.MinValue;
        int s = 0;
        for (int i = 0; i <= n; i++)
        {
            // i-i+1に追加

            if (i % 2 == 0)
            {
                // 点いてる
                if (a[i + 1] - a[i] > 1)
                {
                    int t = s + a[i + 1] - a[i] - 1 + b[i];
                    ans = Math.Max(ans, t);
                }

                s += a[i + 1] - a[i];
            }
            else
            {
                // 消えてる
                if(a[i+1]-a[i] > 1)
                {
                    int t = s + a[i + 1] - a[i] - 1 + b[i];
                    ans = Math.Max(ans, t);
                }
            }
        }

        ans = Math.Max(ans, s);
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
