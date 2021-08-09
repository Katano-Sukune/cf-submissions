using System;
using System.Linq;
using CompLib.Util;

public class Program
{

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
        int k = sc.NextInt();

        string s = sc.Next();

        /*
         * 01文字列
         * 
         * k部分列が
         */

        /*
         * kごと同じ文字
         * 
         * 
         */

        // 
        int[] t = new int[k];

        for (int i = 0; i < n; i++)
        {
            if (s[i] == '?') continue;
            if (t[i % k] == 0) t[i % k] = (s[i] == '1' ? 1 : -1);
            else if (t[i % k] != (s[i] == '1' ? 1 : -1))
            {
                Console.WriteLine("NO");
                return;
            }
        }

        int q = t.Count(num => num == 0);

        int tmp = 0;
        for (int i = 0; i < k; i++)
        {
            tmp += t[i];
        }

        if (Math.Abs(tmp) > q)
        {
            Console.WriteLine("NO");
            return;
        }

        int p = tmp;
        tmp -= t[0];

        for (int i = k; i < n; i++)
        {
            tmp += t[i % k];
            if (tmp != p)
            {
                Console.WriteLine("NO");
                return;
            }
            tmp -= t[(i - (k - 1)) % k];
        }

        Console.WriteLine("YES");
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
