using System;
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
        int r = sc.NextInt();
        int p = sc.NextInt();
        int s = sc.NextInt();
        string str = sc.Next();

        int w = 0;
        char[] ans = new char[n];

        for (int i = 0; i < n; i++)
        {
            switch (str[i])
            {
                case 'R':
                    if (p > 0)
                    {
                        w++;
                        p--;
                        ans[i] = 'P';
                    }

                    break;
                case 'S':
                    if (r > 0)
                    {
                        w++;
                        r--;
                        ans[i] = 'R';
                    }

                    break;
                case 'P':
                    if (s > 0)
                    {
                        w++;
                        s--;
                        ans[i] = 'S';
                    }

                    break;
            }
        }

        
        if (w * 2 >= n)
        {
            Console.WriteLine("YES");
            for (int i = 0; i < n; i++)
            {
                if (ans[i] == default(char))
                {
                    if (r > 0)
                    {
                        r--;
                        ans[i] = 'R';
                    }
                    else if (s > 0)
                    {
                        s--;
                        ans[i] = 'S';
                    }
                    else if(p > 0)
                    {
                        p--;
                        ans[i] = 'P';
                        ans[i] = 'P';
                    }
                }
            }
            Console.WriteLine(new string(ans));
        }
        else
        {
            Console.WriteLine("NO");
        }
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