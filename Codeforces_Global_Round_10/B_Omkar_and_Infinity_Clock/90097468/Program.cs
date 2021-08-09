using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        var sc = new Scanner();
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
        int[] a = sc.IntArray();

        /*
         * 配列a
         * 
         * dをaの最大とする
         * aの要素すべてを d - a_iに置き換える
         * 
         * k回操作後a
         */


        /*
         * 1回目 0~
         * 
         * 2回目
         */

        int max = a.Max();
        long[] one = new long[n];
        for (int i = 0; i < n; i++)
        {
            one[i] = max - a[i];
        }

        long max2 = one.Max();
        long[] two = new long[n];
        for (int i = 0; i < n; i++)
        {
            two[i] = max2 - one[i];
        }

        if (k % 2 == 0)
        {
            Console.WriteLine(string.Join(" ", two));
        }
        else
        {
            Console.WriteLine(string.Join(" ", one));
        }
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
