using System;
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
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        string s = sc.Next();
        int low = 0;
        int up = 0;
        int num = 0;
        foreach (var c in s)
        {
            if ('0' <= c && c <= '9') num++;
            if ('a' <= c && c <= 'z') low++;
            if ('A' <= c && c <= 'Z') up++;
        }

        if (num > 0 && low > 0 && up > 0)
        {
            Console.WriteLine(s);
            return;
        }

        char[] ar = s.ToCharArray();
        // 1種
        if (low == s.Length)
        {
            ar[0] = '1';
            ar[1] = 'A';
        }
        else if (up == s.Length)
        {
            ar[0] = '1';
            ar[1] = 'a';
        }
        else if (num == s.Length)
        {
            ar[0] = 'a';
            ar[1] = 'A';
        }
        else
        {
            // 2種
            for (int i = 0; i < s.Length; i++)
            {
                bool u = 'A' <= s[i] && s[i] <= 'Z';
                bool l = 'a' <= s[i] && s[i] <= 'z';
                bool n = '0' <= s[i] && s[i] <= '9';
                if (u && up >= 2)
                {
                    if (num == 0)
                    {
                        ar[i] = '1';
                        break;
                    }
                    else if (low == 0)
                    {
                        ar[i] = 'a';
                        break;
                    }
                }

                if (l && low >= 2)
                {
                    if (num == 0)
                    {
                        ar[i] = '1';
                        break;
                    }
                    else if (up == 0)
                    {
                        ar[i] = 'A';
                        break;
                    }
                }

                if (n && num >= 2)
                {
                    if (up == 0)
                    {
                        ar[i] = 'A';
                        break;
                    }
                    else if (low == 0)
                    {
                        ar[i] = 'a';
                        break;
                    }
                }
            }
        }

        Console.WriteLine(new string(ar));
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
