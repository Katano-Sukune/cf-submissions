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
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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

        long sum = 0;
        foreach (var i in a)
        {
            sum += i;
        }
        var tmpB1 = new int[n];
        var tmpD1 = 0L;

        var tmpB2 = new int[n];
        var tmpD2 = 0L;
        for (int i = 0; i < n; i++)
        {
            if (i % 2 == 0)
            {
                tmpB1[i] = 1;
                tmpB2[i] = a[i];
            }
            else
            {
                tmpB1[i] = a[i];
                tmpB2[i] = 1;
            }

            tmpD1 += Math.Abs(a[i] - tmpB1[i]);
            tmpD2 += Math.Abs(a[i] - tmpB2[i]);
        }

        if (2 * tmpD1 <= sum)
        {
            Console.WriteLine(string.Join(" ", tmpB1));
        }
        else if (2 * tmpD2 <= sum)
        {
            Console.WriteLine(string.Join(" ", tmpB2));
        }
        else
        {
            throw new Exception();
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
