using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string kuro = sc.Next();
        string shiro = sc.Next();
        string katie = sc.Next();

        int kuroScore = F(kuro, n);
        int shiroScore = F(shiro, n);
        int katieScore = F(katie, n);

        if (kuroScore > shiroScore && kuroScore > katieScore)
        {
            Console.WriteLine("Kuro");
        }
        else if (shiroScore > kuroScore && shiroScore > katieScore)
        {
            Console.WriteLine("Shiro");
        }
        else if (katieScore > kuroScore && katieScore > shiroScore)
        {
            Console.WriteLine("Katie");
        }
        else
        {
            Console.WriteLine("Draw");
        }
    }

    int F(string s, int n)
    {
        int[] cnt = new int[256];
        foreach (char c in s)
        {
            cnt[c]++;
        }

        int result = 0;
        for (char c = 'a'; c <= 'z'; c++)
        {
            int diff = s.Length - cnt[c];
            if (diff == 0 && n == 1)
            {
                return s.Length - 1;
            }

            if (n <= diff)
            {
                result = Math.Max(result, cnt[c] + n);
            }
            else
            {
                if (n % 2 == diff % 2)
                {
                    result = Math.Max(result, s.Length);
                }
                else
                {
                    return s.Length;
                }
            }
        }

        for (char c = 'A'; c <= 'Z'; c++)
        {
            int diff = s.Length - cnt[c];
            if (diff == 0 && n == 1)
            {
                return s.Length - 1;
            }

            if (n <= diff)
            {
                result = Math.Max(result, cnt[c] + n);
            }
            else
            {
                return s.Length;
            }
        }

        return result;
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