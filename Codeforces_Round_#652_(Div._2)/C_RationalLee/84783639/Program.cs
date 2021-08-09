using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();
        int[] w = sc.IntArray();

        // n個の数 a_iがある

        // k人の友達
        // 友達 jにはw_j個配る

        // jの幸福度 min + max

        // 幸福度の総和の最大

        // a最大から wの昇順に割り当てる

        Array.Sort(a, (l, r) => r.CompareTo(l));
        Array.Sort(w);
        List<int> ls = new List<int>();
        long ans = 0;

        for (int i = 0; i < k; i++)
        {
            if (w[i] == 1)
            {
                ans += 2 * a[i];
            }
            else
            {
                ans += a[i];
                ls.Add(w[i] - 1);
            }
        }

        int j = k;
        foreach (int i in ls)
        {
            ans += a[j + i - 1];
            j += i;
        }

        Console.WriteLine(ans);
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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