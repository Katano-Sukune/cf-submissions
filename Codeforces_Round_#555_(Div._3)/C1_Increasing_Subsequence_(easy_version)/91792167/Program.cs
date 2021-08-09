using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        /*
         * 配列a
         * 
         * 右端 or 左端の要素を削除　書き留める
         * 
         * 書き留められる狭義単調増加列
         * 
         * 
         */

        int l = 0;
        int r = n - 1;
        int ans = 0;
        int last = int.MinValue;
        StringBuilder sb = new StringBuilder();
        while (l < r)
        {
            // onsole.WriteLine($"{l} {r}");
            if (a[l] <= last && a[r] <= last)
            {
                break;
            }
            if ((a[r] <= last || a[l] < a[r]) && a[l] > last)
            {
                last = a[l++];
                sb.Append('L');
                ans++;

            }
            else if ((a[l] <= last || a[l] > a[r])&& a[r] > last)
            {
                last = a[r--];
                sb.Append('R');
                ans++;
            }
            else
            {
                // 左から単調増加の個数
                int lCnt = 1;
                for (int i = l + 1; i <= r; i++)
                {
                    if (a[i] <= a[i - 1])
                    {
                        lCnt = i - l;
                        break;
                    }
                }
                int rCnt = 1;
                for (int i = r - 1; i >= l; i--)
                {
                    if (a[i] <= a[i + 1])
                    {
                        rCnt = r - i;
                        break;
                    }
                }

                if (lCnt >= rCnt)
                {
                    ans += lCnt;
                    sb.Append('L', lCnt);
                }
                else
                {
                    ans += rCnt;
                    sb.Append('R', rCnt);
                }

                break;
            }
        }

        if (l == r && a[l] > last)
        {
            sb.Append("R");
            ans++;
        }

        Console.WriteLine(ans);
        Console.WriteLine(sb);
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
