using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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
        int[] t = new int[n];
        int[] x = new int[n];
        for (int i = 0; i < n; i++)
        {
            t[i] = sc.NextInt();
            x[i] = sc.NextInt();
        }

        int ans = 0;

        // 実行中コマンド
        int command = -1;

        // 終了時刻
        long end = -1;

        // 現在地
        long pos = 0;

        // 速度
        long v = 0;

        for (int i = 0; i < n; i++)
        {
            if (command != -1)
            {
                if (end <= t[i])
                {
                    pos += v * (end - t[i - 1]);
                    v = 0;
                    command = -1;
                }
                else
                {
                    pos += v * (t[i] - t[i - 1]);
                }
            }

            if (command == -1)
            {
                command = i;
                end = t[i] + Math.Abs(pos - x[i]);
                v = Math.Sign(x[i] - pos);
            }

            long t1 = pos;

            long t2;
            if (i == n - 1)
            {
                t2 = pos + v * (end - t[i]);
            }
            else
            {
                t2 = pos + v * (Math.Min(end, t[i + 1]) - t[i]);
            }

            if (t2 < t1) (t1, t2) = (t2, t1);
            if (t1 <= x[i] && x[i] <= t2)
            {
                // Console.WriteLine($"{i} {t1} {t2}");
                ans++;
            }
        }
        // Console.WriteLine("-----");
        Console.WriteLine(ans);
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
