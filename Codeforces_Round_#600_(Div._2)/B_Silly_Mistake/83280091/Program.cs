using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // 最後に0になった日

        List<int> ls = new List<int>();
        int cnt = 0;
        bool[] flag = new bool[1000001];
        var hs = new HashSet<int>();
        int last = 0;
        for (int i = 0; i < N; i++)
        {
            if (A[i] > 0)
            {
                if (flag[A[i]])
                {
                    Console.WriteLine("-1");
                    return;
                }

                if (!hs.Add(A[i]))
                {
                    Console.WriteLine("-1");
                    return;
                }

                flag[A[i]] = true;
                cnt++;
            }
            else if (A[i] < 0)
            {
                if (!flag[-A[i]])
                {
                    Console.WriteLine("-1");
                    return;
                }

                flag[-A[i]] = false;
                cnt--;
            }

            if (cnt == 0)
            {
                hs = new HashSet<int>();
                ls.Add(i - last + 1);
                last = i+1;
            }
        }

        if (cnt != 0)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(ls.Count);
        Console.WriteLine(string.Join(" ", ls));
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