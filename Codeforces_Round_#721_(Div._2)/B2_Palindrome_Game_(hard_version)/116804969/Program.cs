using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int[,,] DPEven;
    int[,,,] DPOdd;

    public void Solve()
    {
        // 01の個数、00の個数、反転したか?
        DPEven = new int[501, 501, 2];

        // 01の個数、00,反転、中央
        DPOdd = new int[501, 501, 2, 2];
        for (int i = 0; i <= 500; i++)
        {
            for (int j = 0; j <= 500; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    DPEven[i, j, k] = int.MaxValue;
                    for (int l = 0; l < 2; l++)
                    {
                        DPOdd[i, j, k, l] = int.MaxValue;
                    }
                }
            }
        }




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

    const string Alice = "ALICE";
    const string Bob = "BOB";
    const string Draw = "DRAW";
    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        string s = sc.Next();
        int diff;
        if (n % 2 == 0)
        {
            int oi = 0;
            int oo = 0;
            for (int i = 0; i < n / 2; i++)
            {
                if (s[i] != s[n - i - 1]) oi++;
                if (s[i] == '0' && s[n - i - 1] == '0') oo++;
            }
            diff = Even(oi, oo, 0);
        }
        else
        {
            int oi = 0;
            int oo = 0;
            for (int i = 0; i < (n - 1) / 2; i++)
            {
                if (s[i] != s[n - i - 1]) oi++;
                if (s[i] == '0' && s[n - i - 1] == '0') oo++;
            }
            int m = s[n / 2] - '0';
            diff = Odd(oi, oo, 0, m);
        }

        //         Console.WriteLine(diff);

        if (diff < 0) Console.WriteLine(Alice);
        else if (diff == 0) Console.WriteLine(Draw);
        else Console.WriteLine(Bob);
    }

    // 自分-相手 最小
    int Even(int oi, int oo, int last)
    {
        if (DPEven[oi, oo, last] != int.MaxValue) return DPEven[oi, oo, last];
        if (oi == 0 && oo == 0) return 0;
        int min = int.MaxValue;
        if (last == 0 && oi > 0)
        {
            min = Math.Min(min, -Even(oi, oo, 1));
        }

        if (oi > 0)
        {
            min = Math.Min(min, 1 - Even(oi - 1, oo, 0));
        }

        if (oo > 0)
        {
            min = Math.Min(min, 1 - Even(oi + 1, oo - 1, 0));
        }

        // Console.WriteLine($"oi={oi} oo={oo} {last} {min}");
        DPEven[oi, oo, last] = min;
        return min;
    }

    int Odd(int oi, int oo, int last, int m)
    {
        if (DPOdd[oi, oo, last, m] != int.MaxValue) return DPOdd[oi, oo, last, m];
        if (oi == 0 && oo == 0 && m == 1) return 0;
        int min = int.MaxValue;

        if (last == 0 && oi > 0)
        {
            min = Math.Min(min, -Odd(oi, oo, 1, m));
        }

        if (m == 0)
        {
            min = Math.Min(min, 1 - Odd(oi, oo, 0, 1));
        }
        if (oi > 0)
        {
            min = Math.Min(min, 1 - Odd(oi - 1, oo, 0, m));
        }
        if (oo > 0)
        {
            min = Math.Min(min, 1 - Odd(oi + 1, oo - 1, 0, m));

        }
        DPOdd[oi, oo, last, m] = min;
        return min;
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