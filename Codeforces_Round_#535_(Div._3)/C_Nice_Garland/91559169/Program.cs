using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();

        string rgb = "RGB";

        int ans = int.MaxValue;
        string t = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == j) continue;
                for (int k = 0; k < 3; k++)
                {
                    if (i == k || j == k) continue;
                    int cnt = 0;

                    for (int l = 0; l < n; l++)
                    {
                        if (l % 3 == 0 && s[l] != rgb[i]) cnt++;
                        if (l % 3 == 1 && s[l] != rgb[j]) cnt++;
                        if (l % 3 == 2 && s[l] != rgb[k]) cnt++;
                    }

                    if (cnt < ans)
                    {
                        ans = cnt;
                        var ar = new char[n];
                        for (int l = 0; l < n; l++)
                        {
                            if (l % 3 == 0) ar[l] = rgb[i];
                            if (l % 3 == 1) ar[l] = rgb[j];
                            if (l % 3 == 2) ar[l] = rgb[k];
                        }
                        t = new string(ar);
                    }
                }
            }
        }
        Console.WriteLine(ans);
        Console.WriteLine(t);
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
