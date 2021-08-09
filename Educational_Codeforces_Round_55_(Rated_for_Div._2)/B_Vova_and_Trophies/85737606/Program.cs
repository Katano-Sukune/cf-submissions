using System;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();

        // 元々のやつ
        // 他にある + 1

        int ans = 0;
        int cnt = 0;
        int g = 0;
        for (int i = 0; i < n; i++)
        {
            if (s[i] == 'G')
            {
                cnt++;
                g++;
            }
            else
            {
                if (cnt > 0)
                {
                    ans = Math.Max(ans, cnt);
                }

                cnt = 0;
            }
        }
        
        if (cnt > 0)
        {
            ans = Math.Max(ans, cnt);
        }

        if (g == ans)
        {
            Console.WriteLine(ans);
            return;
        }

        ans++;


        // 
        // 1つ空けて隣合う2つ
        // 他にある + 1

        for (int i = 1; i < n - 1; i++)
        {
            if (s[i] == 'S')
            {
                int gg = 0;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (s[j] == 'G') gg++;
                    else break;
                }

                for (int j = i + 1; j < n; j++)
                {
                    if (s[j] == 'G') gg++;
                    else break;
                }

                if (gg < g) gg++;
                ans = Math.Max(ans, gg);
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