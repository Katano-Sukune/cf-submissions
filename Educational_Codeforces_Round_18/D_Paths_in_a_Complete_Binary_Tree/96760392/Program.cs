using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private long N;
    private int Q;

    private long[] U;
    private string[] S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        Q = sc.NextInt();
        U = new long[Q];
        S = new string[Q];
        for (int i = 0; i < Q; i++)
        {
            U[i] = sc.NextLong();
            S[i] = sc.Next();
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < Q; i++)
        {
            long cur = U[i];
            long height = cur & -cur;
            foreach (char c in S[i])
            {
                switch (c)
                {
                    case 'U':
                        if (cur != (N + 1) / 2)
                        {
                            if ((cur & (height * 2)) == 0)
                            {
                                cur += height;
                            }
                            else
                            {
                                cur -= height;
                            }

                            height *= 2;
                        }

                        break;
                    case 'L':
                        if (height != 1)
                        {
                            cur = cur - height / 2;
                            height /= 2;
                        }

                        break;
                    case 'R':
                        if (height != 1)
                        {
                            cur = cur + height / 2;
                            height /= 2;
                        }

                        break;
                }
            }

            Console.WriteLine(cur);
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