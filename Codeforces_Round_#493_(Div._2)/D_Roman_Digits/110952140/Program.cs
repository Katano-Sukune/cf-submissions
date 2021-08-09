using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Console.WriteLine(D(N));

        // L
        // XXXXX

        // 

        // 7

        // X
        // VV


        // LIIIII
        // XXXXVV


        // 3+N C N
        // 6 -1
        // 7 -4
        // 8 -10
        // 9 -22 
        // 10 -42 
        // 11 -72 
        // 12 -114

        // LIIIII 55
        // XXXXXV 55

        // LVVVVVVVV 90
        // XXXXXXXXX 90

        // XXXXIIIII 45
        // VVVVVVVVV 45
    }

    BigInteger C(BigInteger n)
    {
        return (n + 3) * (n + 2) * (n + 1) / 6;
    }

    BigInteger D(BigInteger n)
    {
        BigInteger ans = C(n);
        // LIIIII 55
        // XXXXXV 55
        if (n >= 6)
        {
            ans -= C(n - 6);
        }
        // LVVVVVVVV 90
        // XXXXXXXXX 90

        // XXXXIIIII 45
        // VVVVVVVVV 45
        if (n >= 9)
        {
            ans -= C(n - 9) * 2;

            // 
        }

        // LIIIII & LVVVVVVVV
        if (n >= 14)
        {
            ans += C(n - 14);
        }

        // LVVVVVVVV & VVVVVVVVV
        if (n >= 10)
        {
            ans += C(n - 10);
        }

        // VVVVVVVVV & LIIIII
        // if (n >= 15)
        // {
        //     ans += C(n - 15);
        // }
        
        // IIIIIVVVVVVVVVL
        // if (n >= 15)
        // {
        //     ans -= C(n - 15);
        // }

        return ans;
    }

    void F(int n)
    {
        var hs = new HashSet<int>();
        for (int i = 0; i <= n; i++)
        {
            for (int v = 0; i + v <= n; v++)
            {
                for (int x = 0; i + v + x <= n; x++)
                {
                    int l = n - (i + v + x);

                    int num = i + 5 * v + 10 * x + 50 * l;

                    if (!hs.Add(num))
                    {
                        //   Console.WriteLine(
                        //     $"{new string('I', i)}{new string('V', v)}{new string('X', x)}{new string('L', l)} = {num}");
                    }
                }
            }
        }

        Console.WriteLine(hs.Count);
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