using System;
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
        int n = sc.NextInt();
        int p = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();

        /*
         * iはa_iコイン
         *
         * 所持金p
         *
         * 
         */

        Array.Sort(a);

        int[] front = new int[k];
        for (int i = 0; i < k - 1; i++)
        {
            front[i + 1] = front[i] + a[i];
        }

        int[][] table = new int[k][];
        for (int i = 0; i < k; i++)
        {
            table[i] = new int[n / k + 2];
        }

        for (int i = 0; i < n; i++)
        {
            table[i % k][i / k + 1] += table[i % k][i / k] + a[i];
        }

        for (int ans = n; ans >= 0; ans--)
        {
            int cost = 0;
            if (ans % k == 0)
            {
                cost += table[k - 1][ans / k];
            }
            else
            {
                cost += table[ans % k - 1][ans / k + 1] - table[ans % k - 1][1];
            }

            // Console.WriteLine($"{ans}- {cost}");

            cost += front[ans % k];

            if (cost <= p)
            {
                Console.WriteLine(ans);
                return;
            }
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