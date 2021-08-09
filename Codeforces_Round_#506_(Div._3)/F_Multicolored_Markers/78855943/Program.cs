using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    long A, B;
    public void Solve()
    {
        var sc = new Scanner();
        A = sc.NextLong();
        B = sc.NextLong();
        var div = Div(A + B);
        var divA = Div(A);
        var divB = Div(B);
        div.Sort();
        divA.Sort();
        divB.Sort();
        long ans = long.MaxValue;
        for (int i = 0; i <= div.Count / 2; i++)
        {

            long j = div[div.Count - 1 - i];


            // A,Bのどちらかが i以下、j以下の積で表せるか?
            {
                int ok = -1;
                int ng = divA.Count;
                while (ng - ok > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (divA[mid] <= div[i]) ok = mid;
                    else ng = mid;
                }
                if(ok >= 0 && divA[divA.Count - ng] <= j)
                {
                    // Console.WriteLine($"A {div[i]} {j}");
                    ans = Math.Min(ans, (div[i] + j) * 2);
                    continue;
                }
            }

            {
                int ok = -1;
                int ng = divB.Count;
                while (ng - ok > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (divB[mid] <= div[i]) ok = mid;
                    else ng = mid;
                }
                if (ok >= 0 && divB[divB.Count - ng] <= j)
                {
                    // Console.WriteLine($"B {div[i]} {j}");
                    ans = Math.Min(ans, (div[i] + j) * 2);
                    continue;
                }
            }
        }
        Console.WriteLine(ans);
    }

    public List<long> Div(long l)
    {
        var result = new List<long>();
        for (long i = 1; i * i <= l; i++)
        {
            if (l % i == 0)
            {
                result.Add(i);
                if (l / i != i) result.Add(l / i);
            }
        }
        return result;
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
