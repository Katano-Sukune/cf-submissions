using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();
        int[] cnt = new int[N];
        int min = 0;
        int t = 0;
        for (int i = 0; i < N; i++)
        {
            var c = S[i];
            if (c == '(') t++;
            if (c == ')') t--;
            cnt[i] = t;
            min = Math.Min(min, t);
        }
        if (cnt[N - 1] == 2)
        {
            if (min < 0)
            {
                Console.WriteLine(0);
                return;
            }
            // ( -> )
            // cnt -2
            // 以降のカウンタが2以上のところ
            int ans = 0;
            for (int i = N - 1; i >= 0; i--)
            {
                if (cnt[i] < 2) break;
                if (S[i] == '(') ans++;
            }
            Console.WriteLine(ans);
        }
        else if (cnt[N - 1] == -2)
        {
            if (min < -2)
            {
                Console.WriteLine(0);
                return;
            }
            // ) -> (
            // cnt += 2
            // 以降のminを打ち消せるところ
            int ans = 0;
            for (int i = 0; i < N; i++)
            {
               
                if (S[i] == ')') ans++;
                if (cnt[i] < 0) break;
            }
            Console.WriteLine(ans);
        }
        else
        {
            Console.WriteLine(0);
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
