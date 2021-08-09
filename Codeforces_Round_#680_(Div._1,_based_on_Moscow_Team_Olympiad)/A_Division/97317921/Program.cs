using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        long p = sc.NextLong();
        long q = sc.NextLong();

        // pはxで割り切れる

        // xはqで割り切れない
        // 最大の x

        if (p % q != 0)
        {
            Console.WriteLine(p);
            return;
        }

        var prime = new List<long>();
        var cntP = new List<int>();
        var cntQ = new List<int>();

        for (long d = 2; d * d <= q; d++)
        {
            if (q % d == 0)
            {
                int cP = 0;
                while (p % d == 0)
                {
                    cP++;
                    p /= d;
                }

                cntP.Add(cP);

                int cQ = 0;
                while (q % d == 0)
                {
                    cQ++;
                    q /= d;
                }

                cntQ.Add(cQ);
                prime.Add(d);
            }
        }

        if (q != 1)
        {
            cntQ.Add(1);
            int cP = 0;
            while (p % q == 0)
            {
                cP++;
                p /= q;
            }

            cntP.Add(cP);
            prime.Add(q);
        }

        long ans = long.MinValue;
        long[] arP = new long[prime.Count];
        long[] arQ = new long[prime.Count];
        long tmp = 1;
        for (int i = 0; i < prime.Count; i++)
        {
            arQ[i] = 1;
            for (int j = 0; j < cntQ[i] - 1; j++)
            {
                arQ[i] *= prime[i];
            }

            arP[i] = 1;
            for (int j = 0; j < cntP[i]; j++)
            {
                arP[i] *= prime[i];
            }

            tmp *= arP[i];
        }

        for (int i = 0; i < prime.Count; i++)
        {
            long tmp2 = tmp / arP[i] * arQ[i];
            ans = Math.Max(ans, tmp2);
        }

        Console.WriteLine(ans * p);
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