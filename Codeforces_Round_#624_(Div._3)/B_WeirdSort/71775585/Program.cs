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
            int n = sc.NextInt();
            int m = sc.NextInt();
            int[] a = sc.IntArray();
            int[] p = sc.IntArray();

            // aを昇順ソートする
            // a[p[i]],a[p[i]+1のswapだけで

            Array.Sort(p);

            for (int j = 0; j < 100; j++)
            {
                foreach (int k in p)
                {
                    if (a[k] < a[k - 1])
                    {
                        int tmp = a[k];
                        a[k] = a[k - 1];
                        a[k - 1] = tmp;
                    }
                }
            }
            var flag = true;
            for (int j = 1; j < n; j++)
            {
                if (a[j - 1] > a[j])
                {
                    flag = false;
                    break;
                }
            }
            sb.AppendLine(flag ? "YES" : "NO");
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