using CompLib.Util;
using System;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        // f(x,y) = x|y -y

        // ibit目が立ってる ibitが立ってるのが1つだけ 

        int[] left = new int[n + 1];
        for (int i = 0; i < n; i++)
        {
            left[i + 1] = left[i] | a[i];
        }

        int[] right = new int[n + 1];
        for (int i = n - 1; i >= 0; i--)
        {
            right[i] = right[i + 1] | a[i];
        }

        int max = -1;
        int index = -1;
        for (int i = 0; i < n; i++)
        {
            int t = left[i] | right[i + 1];
            int tt = (a[i] | t) - t;

            if (max < tt)
            {
                max = tt;
                index = i;
            }
        }

        int tmp = a[0];
        a[0] = a[index];
        a[index] = tmp;
        Console.WriteLine(string.Join(" ", a));
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