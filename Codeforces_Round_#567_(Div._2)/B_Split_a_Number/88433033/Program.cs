using System;
using System.Linq;
using System.Numerics;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int l = sc.NextInt();
        string s = sc.Next();
        for (int d = l % 2;; d += 2)
        {
            // ||f| - |b|| = d
            // 前が短い
            // f = [0, l/2-d/2)
            // b = [l/2-d/2, l)
            bool flagA = s[l / 2 - d / 2] != '0';
            // 後ろ
            // f = [0, l/2+(d+1)/2 )
            // b = [l/2 + (d+1)/2  , l)

            bool flagB = s[(l + 1) / 2 + d / 2] != '0';
            if (flagA)
            {
                BigInteger a = Parse(s.Substring(0, l / 2 - d / 2)) + Parse(s.Substring(l / 2 - d / 2));
                if (flagB)
                {
                    BigInteger b = Parse(s.Substring(0, (l + 1) / 2 + d / 2)) + Parse(s.Substring((l + 1) / 2 + d / 2));
                    Console.WriteLine(BigInteger.Min(a, b));
                    return;
                }
                else
                {
                    Console.WriteLine(a);
                    return;
                }
            }
            else
            {
                if (flagB)
                {
                    BigInteger b = Parse(s.Substring(0, (l + 1) / 2 + d / 2)) + Parse(s.Substring((l + 1) / 2 + d / 2));
                    Console.WriteLine(b);
                    return;
                }
            }
        }
    }

    BigInteger Parse(string s)
    {
        BigInteger result = 0;
        foreach (char c in s)
        {
            result *= 10;
            result += c - '0';
        }

        return result;
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