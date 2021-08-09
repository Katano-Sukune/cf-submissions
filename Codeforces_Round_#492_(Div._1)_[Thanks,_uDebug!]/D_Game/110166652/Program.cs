using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, R;
    private int[] C;
    private int[] Z, G;

    private bool[] Seen;
    private decimal[] Memo;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        R = sc.NextInt();

        // C_x ゲームの結果がx f(x) = C_x
        C = sc.IntArray();

        // C_z_i := G_i
        Z = new int[R];
        G = new int[R];
        for (int i = 0; i < R; i++)
        {
            Z[i] = sc.NextInt();
            G[i] = sc.NextInt();
        }

#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif

        long sum = 0;
        foreach (var i in C)
        {
            sum += i;
        }

        Console.WriteLine((decimal) sum / (1 << N));
        for (int i = 0; i < R; i++)
        {
            sum -= C[Z[i]];
            C[Z[i]] = G[i];
            sum += C[Z[i]];
            
            Console.WriteLine((decimal) sum / (1 << N));
        }

        Console.Out.Flush();
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