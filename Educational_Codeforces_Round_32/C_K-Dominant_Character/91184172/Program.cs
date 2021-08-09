using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    string S;
    int N;
    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        N = S.Length;

        /*
         * 文字列
         * c 長さkの文字列にcが含まれる k-dominant
         * 
         * 最小のk
         */

        int ok = N;
        int ng = 0;
        while (ok - ng > 1)
        {
            int mid = (ok + ng)/2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(ok);
    }

    bool F(int k)
    {
        int[] cnt = new int[26];
        for (int i = 0; i < k - 1; i++)
        {
            cnt[S[i] - 'a']++;
        }

        bool[] f = new bool[26];
        for (int i = 0; i < 26; i++)
        {
            f[i] = true;
        }

        for (int i = k - 1; i < N; i++)
        {
            cnt[S[i] - 'a']++;
            for (int j = 0; j < 26; j++)
            {
                f[j] &= cnt[j] > 0;
            }
            cnt[S[i - (k - 1)] - 'a']--;
        }
        
        for (int i = 0; i < 26; i++)
        {
            if (f[i]) return true;
        }
        return false;
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
