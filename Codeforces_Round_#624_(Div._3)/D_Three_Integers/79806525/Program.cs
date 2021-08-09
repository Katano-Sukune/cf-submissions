using System;
using System.Linq;
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
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Q(int a, int b, int c)
    {
        // a <=b<=c
        // 1つ選んで +-1する

        // b%a = 0
        // c%b = 0
        // 操作回数

        int ansA = -1;
        int ansB = -1;
        int ansC = -1;

        int ans = int.MaxValue;
        for (int bb = 1; bb <= 20000; bb++)
        {
            int diff = Math.Abs(bb - b);
            int diffA = int.MaxValue;
            int tmpA = -1;
            for (int aa = 1; aa * aa <= bb; aa++)
            {
                if (bb % aa == 0)
                {
                    if (Math.Abs(aa - a) < diffA)
                    {
                        diffA = Math.Abs(aa - a);
                        tmpA = aa;
                    }
                    if (Math.Abs(bb / aa - a) < diffA)
                    {
                        diffA = Math.Abs(bb / aa - a);
                        tmpA = bb / aa;
                    }
                }

            }
            diff += diffA;

            int lC = (c / bb) * bb;
            int gC = ((c + bb - 1) / bb) * bb;
            int cc;
            if (lC >= bb && Math.Abs(lC - c) <= Math.Abs(gC - c))
            {
                diff += Math.Abs(lC - c);
                cc = lC;
            }
            else
            {
                diff += Math.Abs(gC - c);
                cc = gC;
            }


            if (diff < ans)
            {
                ansA = tmpA;
                ansB = bb;
                ansC = cc;
                ans = diff;
            }
        }
        return $"{ans}\n{ansA} {ansB} {ansC}";
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
