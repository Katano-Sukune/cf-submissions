using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Query(sc.NextInt(), sc.NextInt(), sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Query(long str, long i, long exp)
    {
        // (exp ,0)から (exp-x,x) まで x+1通り
        // str + exp-x > x + int
        // str + exp - int > 2x
        if (str + exp <= i) return "0";
        long x2 = str + exp - i;

        long x = Math.Min(exp, (x2 - 1) / 2);
        return (x + 1).ToString();
    }
    string Q2(long str, long i, long exp)
    {
        long ans = 0;
        for (int ss = 0; ss <= exp; ss++)
        {
            long ii = exp - ss;
            if (str + ss > i + ii) ans++;
        }
        return ans.ToString();
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
