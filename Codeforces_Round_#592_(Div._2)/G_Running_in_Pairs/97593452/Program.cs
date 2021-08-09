using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long N;
    private long K;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        K = sc.NextLong();

        long min = (1 + N) * N / 2;
        if (K < min)
        {
            Console.WriteLine("-1");
            return;
        }

        int[] q = new int[N];
        for (int i = 0; i < N; i++)
        {
            q[i] = i + 1;
        }

        long sum = min;
        for (int i = 0; K - sum > 0 && i < N / 2; i++)
        {
            long diff = K - sum;
            if (N - i - (i + 1) <= diff)
            {
                (q[i], q[N - i - 1]) = (q[N - i - 1], q[i]);
                sum += (N - i) - (i + 1);
            }
            else
            {
                (q[i], q[i + diff]) = (q[i + diff], q[i]);
                sum = K;
                break;
            }
        }

        Console.WriteLine(sum);
        Console.WriteLine(string.Join(" ", new int[N].Select((i, index) => index + 1)));
        Console.WriteLine(string.Join(" ", q));
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