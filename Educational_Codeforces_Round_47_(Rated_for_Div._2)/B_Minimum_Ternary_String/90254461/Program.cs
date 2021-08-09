using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        string s = sc.Next();

        /*
         * 隣接0,1を入れ替え
         * 1,2を入れ替え
         * 
         * 最小
         */

        /*
         * 0000...22222...0000
         * 最初の0,2の間に1全部　
         * 
         */

        int cnt1 = s.Count(c => c == '1');

        var sb = new StringBuilder();
        bool f = true;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '1') continue;
            if (f && s[i] == '2')
            {
                sb.Append('1', cnt1);
                f = false;
            }
            sb.Append(s[i]);
        }
        if (f)
        {
            sb.Append('1', cnt1);
        }

        Console.WriteLine(sb);
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct S
{
    public char C;
    public int I;
    public S(char c, int i)
    {
        C = c;
        I = i;
    }
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
