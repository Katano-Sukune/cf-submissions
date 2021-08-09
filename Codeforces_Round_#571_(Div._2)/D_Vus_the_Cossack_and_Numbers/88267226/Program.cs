using System;
using CompLib.Util;

public class Program
{
    private int N;
    private double[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new double[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextDouble();
        }

        // 総和が0のAがある

        // 全部小さいほうに寄せる
        // 小さいぶんてきとうなやつをおおきくする
        long sum = 0;
        long[] b = new long[N];
        for (int i = 0; i < N; i++)
        {
            b[i] = (long) Math.Floor(A[i]);
            sum += b[i];
        }

        for (int i = 0; sum < 0; i++)
        {
            if (Math.Abs(Math.Floor(A[i]) - A[i]) < 1e-8) continue;
            sum++;
            b[i]++;
        }

        Console.WriteLine(string.Join("\n", b));
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