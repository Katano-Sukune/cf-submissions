using CompLib.Util;
using System;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();

        string s = sc.Next();
        int[,] cnt = new int[s.Length + 1, 26];
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                cnt[i + 1, j] = cnt[i, j];
            }
            cnt[i + 1, s[i] - 'a']++;
        }
        int q = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            int l = sc.NextInt();
            int r = sc.NextInt();


            // 長さ1 ok
            if (l == r)
            {
                sb.AppendLine("Yes");
                continue;
            }

            //　最初、最後違う　入れ替えでok
            if (s[l - 1] != s[r - 1])
            {
                sb.AppendLine("Yes");
                continue;
            }

            int c = 0;
            for (int j = 0; j < 26; j++)
            {
                if (cnt[r, j] > cnt[l - 1, j]) c++;
            }
            if (c >= 3)
            {
                sb.AppendLine("Yes");
                continue;
            }

            sb.AppendLine("No");
            // 同じ 1種　無理
            // 2種 無理
            // 3以上 ok
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