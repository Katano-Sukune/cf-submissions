using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }

        /*
         * 01文字列
         *
         * しりとりになるように並べる
         *
         * 並べられるようにsのいくつか反転させる
         *
         * 反転させる文字列個数最小
         */

        /*
         * 1-0
         * 0-1を反転
         *
         * 個数の差 0 or 1にする
         *
         * 0-0, 1-1がある、1-0が0個不可能
         */

        int IO = 0;
        int OI = 0;
        int OO = 0;
        int II = 0;
        foreach (var str in s)
        {
            if (str[0] == '1')
            {
                if (str[^1] == '1')
                {
                    II++;
                }
                else
                {
                    IO++;
                }
            }
            else
            {
                if (str[^1] == '1')
                {
                    OI++;
                }
                else
                {
                    OO++;
                }
            }
        }

        if (II > 0 && OO > 0 && IO == 0 && OI == 0)
        {
            Console.WriteLine("-1");
            return;
        }

        var hs = new HashSet<string>(StringComparer.Ordinal);
        foreach (string str in s)
        {
            hs.Add(str);
        }

        List<int> ans = new List<int>();
        if (IO > OI + 1)
        {
            // 1-0をへらす
            for (int i = 0; IO > OI + 1 && i < n; i++)
            {
                if (s[i][0] == '1' && s[i][^1] == '0')
                {
                    if (!hs.Contains(new String(s[i].Reverse().ToArray())))
                    {
                        ans.Add(i + 1);
                        IO--;
                        OI++;
                    }
                }
            }
        }
        else if (OI > IO + 1)
        {
            for (int i = 0; OI > IO + 1 && i < n; i++)
            {
                if (s[i][0] == '0' && s[i][^1] == '1')
                {
                    if (!hs.Contains(new string(s[i].Reverse().ToArray())))
                    {
                        ans.Add(i + 1);
                        OI--;
                        IO++;
                    }
                }
            }
        }

        if (Math.Abs(IO - OI) > 1)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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