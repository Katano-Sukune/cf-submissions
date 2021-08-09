using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    private List<long> L;


    public void Solve()
    {
        var sc = new Scanner();

        L = new List<long>() { 1 };
        for (int len = 1; L[^1] < (long)1e18; len++)
        {
            long s = 0;
            for (int i = 0; i < len; i++) s += L[i];
            L.Add(s);
        }
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
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
        long k = sc.NextLong();
        // n順列 a
        // a_{i+1} >= a_i - 1
        // 辞書順k番目

        // x順列個数 f(x)
        // 

        // 1 ...... f(n-1)個
        // 2 1 .....  f(n-2)個
        // 3 2 1 ..... 
        if (n < L.Count && L[n] < k)
        {
            Console.WriteLine("-1");
            return;
        }

        // min(n, |L|)で k番目
        int len = Math.Min(n, L.Count - 1);
        int[] tmp = new int[len];
        long s = 0;
        for (int i = 0; i < len;)
        {
            for (int l = 1; ; l++)
            {
                if (s + L[len - i - l] >= k)
                {
                    for (int j = 0; j < l; j++)
                    {
                        tmp[i + l - j - 1] = i + j;
                    }
                    i += l;
                    break;
                }
                s += L[len - i - l];
            }
        }

        // Console.WriteLine(string.Join(" ", tmp));

        int[] ans = new int[n];
        for (int i = 0; i < n - len; i++)
        {
            ans[i] = i + 1;
        }
        for (int i = n - len; i < n; i++)
        {
            ans[i] = tmp[i - (n - len)] + (n - len) + 1;
        }

        Console.WriteLine(string.Join(" ", ans));
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
