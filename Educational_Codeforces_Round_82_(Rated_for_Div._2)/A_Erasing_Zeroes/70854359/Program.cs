using CompLib.Util;
using System;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            var s = sc.Next();
            // sの全部の1が連続する
            bool flag = false;
            int left = -1;
            int right = -1;
            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] == '1')
                {
                    flag = true;
                    left = j;
                    break;
                }
            }

            for (int j = s.Length - 1; j >= 0; j--)
            {
                if (s[j] == '1')
                {
                    flag = true;
                    right = j;
                    break;
                }
            }
            int ans = 0;
            if (flag)
            {

                for (int j = left; j <= right; j++)
                {
                    if (s[j] == '0') ans++;
                }


            }
            sb.AppendLine(ans.ToString());
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