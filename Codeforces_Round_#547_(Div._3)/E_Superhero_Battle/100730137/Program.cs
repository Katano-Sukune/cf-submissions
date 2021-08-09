using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long H;
    private int N;
    private int[] D;

    public void Solve()
    {
        var sc = new Scanner();
        H = sc.NextLong();
        N = sc.NextInt();
        D = sc.IntArray();

        long sum = 0;
        long min = 0;
        for (int i = 0; i < N; i++)
        {
            sum += D[i];
            min = Math.Min(min, sum);
        }


        if (H + min <= 0)
        {
            for (int i = 0; i < N; i++)
            {
                H += D[i];
                if (H <= 0)
                {
                    Console.WriteLine(i + 1);
                    return;
                }
            }
        }

        if (sum >= 0)
        {
            Console.WriteLine("-1");
            return;
        }

        long l = (H + min - sum - 1) / (-sum);
        long ans = l * N;
        H += l * sum;
        if (H <= 0)
        {
            Console.WriteLine(ans);
            return;
        }

        for (int i = 0; i < N; i++)
        {
            ans++;
            H += D[i];
            if (H <= 0)
            {
                Console.WriteLine(ans);
                return;
            }
        }
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