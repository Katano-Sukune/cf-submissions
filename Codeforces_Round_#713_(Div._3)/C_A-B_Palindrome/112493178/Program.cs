using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int[] c = sc.IntArray();
        string s = sc.Next();

        int len = c[0] + c[1];
        if (c[0] % 2 == 1 && c[1] % 2 == 1)
        {
            Console.WriteLine("-1");
            return;
        }

        char[] ans = new char[len];
        int[] tmp = new int[2];
        List<int> ls = new List<int>();
        for (int i = 0; i < len / 2; i++)
        {
            int j = len - i - 1;
            if (s[i] == '?')
            {
                if (s[j] == '?')
                {
                    ls.Add(i);
                }
                else
                {
                    tmp[s[j] - '0'] += 2;
                    ans[i] = ans[j] = s[j];
                }
            }
            else
            {
                if (s[j] == '?')
                {
                    tmp[s[i] - '0'] += 2;
                    ans[i] = ans[j] = s[i];
                }
                else
                {
                    if (s[i] != s[j])
                    {
                        Console.WriteLine("-1");
                        return;
                    }

                    tmp[s[i] - '0'] += 2;
                    ans[i] = ans[j] = s[i];
                }
            }
        }
        //
        // Console.WriteLine($"{tmp[0]} {tmp[1]}");
        // Console.WriteLine(new string(ans));
        if (tmp[0] > c[0] || tmp[1] > c[1])
        {
            Console.WriteLine("-1");
            return;
        }

        if ((len) % 2 == 1)
        {
            if (s[len / 2] == '?')
            {
                if (c[0] % 2 == 1)
                {
                    ans[(c[0] + c[1]) / 2] = '0';
                    tmp[0]++;
                }
                else
                {
                    ans[(c[0] + c[1]) / 2] = '1';
                    tmp[1]++;
                }
            }
            else
            {
                if (c[s[len / 2] - '0'] % 2 == 1)
                {
                    ans[len / 2] = s[len / 2];
                    tmp[s[len / 2] - '0']++;
                }
                else
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }
        
        if (tmp[0] > c[0] || tmp[1] > c[1])
        {
            Console.WriteLine("-1");
            return;
        }

        foreach (int i in ls)
        {
            int j = c[0] + c[1] - i - 1;
            if (tmp[0] < c[0])
            {
                ans[i] = ans[j] = '0';
                tmp[0]+=2;
            }
            else
            {
                ans[i] = ans[j] = '1';
                tmp[1]+=2;
            }
        }

        Console.WriteLine(new string(ans));
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