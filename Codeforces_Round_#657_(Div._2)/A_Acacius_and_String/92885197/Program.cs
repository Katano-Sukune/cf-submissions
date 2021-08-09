using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{

    const string T = "abacaba";
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        string s = sc.Next();


        /*
         * sの?を入れる
         * 
         * abacabaが部分文字列1回でるようにs
         */

        for (int b = 0; b + T.Length <= n; b++)
        {
            bool f = true;
            for (int i = 0; i < T.Length && f; i++)
            {
                if (s[i + b] == '?') continue;
                f &= s[i + b] == T[i];
            }

            if (!f) continue;

            char[] ans = new char[n];
            for (int i = 0; i < n; i++)
            {
                if (s[i] == '?') ans[i] = b <= i && i < b + T.Length ? ans[i] = T[i - b] : 'z';
                else ans[i] = s[i];
            }

            for (int b2 = 0; b2 + T.Length <= n && f; b2++)
            {
                if (b == b2) continue;
                bool f2 = true;
                for (int i = 0; i < T.Length && f2; i++)
                {
                    f2 &= ans[i + b2] == T[i];
                }
                f &= !f2;
            }

            if (f)
            {
                Console.WriteLine("Yes");
                Console.WriteLine(new string(ans));
                return;
            }
        }
        Console.WriteLine("No");
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
