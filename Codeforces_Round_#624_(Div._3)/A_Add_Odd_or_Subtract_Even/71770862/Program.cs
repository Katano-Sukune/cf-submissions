using CompLib.Util;
using System;
using System.Linq;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();

        var sb = new StringBuilder();

        for (int i = 0; i < t; i++)
        {
            int a = sc.NextInt();
            int b = sc.NextInt();

            if (a == b)
            {
                sb.AppendLine("0");
            }
            else if (a < b)
            {
                if ((b - a) % 2 == 1)
                {
                    sb.AppendLine("1");
                }
                else
                {
                    sb.AppendLine("2");
                }
            }
            else
            {
                if ((a - b) % 2 == 0)
                {
                    sb.AppendLine("1");
                }
                else
                {
                    sb.AppendLine("2");
                }
            }
        }

        Console.Write(sb.ToString());
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}