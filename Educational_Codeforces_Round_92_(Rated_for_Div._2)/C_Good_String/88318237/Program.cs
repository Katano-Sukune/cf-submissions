using System;
using System.IO;
using System.Linq;
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
        string s = sc.Next();

        // 文字列s
        // 先頭を末尾に 末尾を先頭にの2つが一致するようにsから削除する文字数最小

        // abababab
        int max = 0;
        for (char a = '0'; a <= '9'; a++)
        {
            for (char b = '0'; b <= '9'; b++)
            {
                if (a == b) continue;
                bool f = false;
                int cnt = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (f)
                    {
                        if (s[i] == b)
                        {
                            cnt++;
                            f = false;
                        }
                    }
                    else
                    {
                        if (s[i] == a)
                        {
                            cnt++;
                            f = true;
                        }
                    }
                }

                if (f) cnt--;
                max = Math.Max(max, cnt);
            }

            // aのみ
            max = Math.Max(max, s.Count(c => c == a));
        }

        Console.WriteLine(s.Length - max);
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