using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
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
        int[] a = sc.IntArray();

        // n+1村
        // 2n-1道
        // 
        // n-1個道 iからi+1へ 1 <= i <= n-1
        // 
        // n個 iからn+1へ if a_i = 0
        //     n+1からiへ if a_i = 1
        // 
        // 全ての村を1度だけ通る
        // 
        // 

        int[] ans = new int[n + 1];

        for (int i = 0; i < n - 1; i++)
        {
            if (a[i] == 0 && a[i + 1] == 1)
            {
                int j = 0;
                for (; j <= i; j++) ans[j] = j + 1;
                ans[j++] = n + 1;
                for (; j <= n; j++) ans[j] = j;
                Console.WriteLine(string.Join(" ", ans));
                return;
            }
        }

        if (a[n - 1] == 0)
        {
            for (int i = 0; i <= n; i++) ans[i] = i + 1;
            Console.WriteLine(string.Join(" ", ans));
            return;
        }
        if (a[0] == 1)
        {
            for (int i = 1; i <= n; i++)
            {
                ans[i] = i;

            }
            ans[0] = n + 1;
            Console.WriteLine(string.Join(" ", ans));
            return;
        }

        Console.WriteLine("-1");
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