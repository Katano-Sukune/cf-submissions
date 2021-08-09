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
        string s = sc.Next();

        /*
         * 偶数長
         * 奇数位置にある文字と次の文字が違う good
         * 
         * sをgoodにする
         * 
         * 
         */

        /*
         * 貪欲に2文字ずつ取る
         */

        var sb = new StringBuilder();


        var cnt = new int[26];
        for (int i = 0; i < s.Length; i++)
        {
            bool f = false;
            for (int c = 0; c < 26; c++)
            {
                if (s[i] - 'a' == c) continue;
                if (cnt[c] == 0) continue;
                f = true;
                sb.Append((char)(c + 'a'));
                sb.Append(s[i]);
                break;
            }

            if (f)
            {
                for (int c = 0; c < 26; c++)
                {
                    cnt[c] = 0;
                }
            }
            else
            {
                cnt[s[i] - 'a']++;
            }
        }

        Console.WriteLine(n-sb.Length);
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
