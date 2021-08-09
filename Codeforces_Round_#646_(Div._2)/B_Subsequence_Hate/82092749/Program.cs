using System;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Console.WriteLine(Q(sc.Next()));
        }
        Console.Out.Flush();
    }

    string Q(string s)
    {
        // 前方n個を全部1,0にする
        int n = s.Length;
        var front = new int[n + 1, 2];
        var back = new int[n + 1, 2];
        for (int i = 0; i < n; i++)
        {
            front[i + 1, 0] = front[i, 0];
            front[i + 1, 1] = front[i, 1];
            back[i + 1, 0] = back[i, 0];
            back[i + 1, 1] = back[i, 1];
            if (s[i] == '1')
            {
                front[i + 1, 0]++;
            }
            else
            {
                front[i + 1, 1]++;
            }

            if (s[n - i - 1] == '1')
            {
                back[i + 1, 0]++;
            }
            else
            {
                back[i + 1, 1]++;
            }
        }

        int ans = int.MaxValue;

        for (int i = 0; i <= n; i++)
        {
            ans = Math.Min(ans, Math.Min(front[i, 0], front[i, 1]) + Math.Min(back[n - i, 0], back[n - i, 1]));
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
