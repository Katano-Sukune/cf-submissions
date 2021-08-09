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
        int[] a = sc.IntArray();
        int[] b = sc.IntArray();

        // じゃんけん
        // alice グー a_1 チョキ a_2 パー a_3
        // bob b

        // aliceが勝つ最小、最大


        // 最大
        int max = Math.Min(a[0], b[1]) + Math.Min(a[1], b[2]) + Math.Min(a[2], b[0]);

        // min
        int min = int.MaxValue;
        {
            for (int j = 0; j < 3; j++)
            {
                int score = n;
                int[] ta = a.ToArray();
                int[] tb = b.ToArray();
                for (int i = 0; i < 3; i++)
                {
                    int tmp = Math.Min(ta[(i + j) % 3], tb[(i + j + 2) % 3]);
                    score -= tmp;
                    ta[(i + j) % 3] -= tmp;
                    tb[(i + j + 2) % 3] -= tmp;

                    int tmp2 = Math.Min(ta[(i + j + 2) % 3], tb[(i + j + 2) % 3]);
                    score -= tmp2;
                    ta[(i + j + 2) % 3] -= tmp2;
                    tb[(i + j + 2) % 3] -= tmp2;
                }

                min = Math.Min(min, score);
                // Console.WriteLine($"{j} {score}");
            }

        }
        Console.WriteLine($"{min} {max}");
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