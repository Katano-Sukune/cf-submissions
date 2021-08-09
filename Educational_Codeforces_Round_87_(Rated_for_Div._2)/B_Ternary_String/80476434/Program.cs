using CompLib.Util;
using System;
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
            sb.AppendLine(Q(sc.Next()));
        }
        Console.Write(sb);
    }

    string Q(string s)
    {
        // 
        var front = new int[s.Length, 3];
        var back = new int[s.Length, 3];
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                front[i, j] = -1;
                back[i, j] = -1;
            }
        }
        for (int i = 0; i < s.Length; i++)
        {
            if (i > 0) for (int j = 0; j < 3; j++) front[i, j] = front[i - 1, j];
            front[i, s[i] - '1'] = i;
        }
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (i + 1 < s.Length) for (int j = 0; j < 3; j++) back[i, j] = back[i + 1, j];
            back[i, s[i] - '1'] = i;
        }

        int ans = int.MaxValue;
        for (int i = 0; i < s.Length; i++)
        {
            int mm = s[i] - '1';
            for (int ff = 0; ff < 3; ff++)
            {
                if (mm == ff) continue;
                for (int bb = 0; bb < 3; bb++)
                {
                    if (ff == bb || mm == bb) continue;
                    if (front[i, ff] == -1 || back[i, bb] == -1) continue;
                    ans = Math.Min(ans, back[i, bb] - front[i, ff] + 1);
                }
            }
        }
        if (ans == int.MaxValue) return "0";
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
