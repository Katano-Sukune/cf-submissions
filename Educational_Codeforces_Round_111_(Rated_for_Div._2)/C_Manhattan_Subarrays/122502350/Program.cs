using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    int N;
    int[] A;
    void Q(Scanner sc)
    {
        N = sc.NextInt();
        A = sc.IntArray();

        long cnt = 0;
        for (int l = 0; l < N; l++)
        {
            for (int len = 1; l + len <= N; len++)
            {
                bool flag = true;
                for (int i = l; i < l + len && flag; i++)
                {
                    for (int j = i + 1; j < l + len && flag; j++)
                    {
                        for (int k = j + 1; k < l + len && flag; k++)
                        {

                            flag &= !f(i, j, k);
                            flag &= !f(i, k, j);
                            flag &= !f(j, i, k);
                            flag &= !f(j, k, i);
                            flag &= !f(k, i, j);
                            flag &= !f(k, j, i);
                        }
                    }
                }
                if (flag)
                {
                    cnt++;
                }
                else
                {
                    //   Console.WriteLine($"{l} {len}");
                    break;
                }
            }
        }
        Console.WriteLine(cnt);
    }

    bool f(int i, int j, int k)
    {
        return Math.Abs(A[k] - A[i]) + Math.Abs(k - i) == Math.Abs(A[k] - A[j]) + Math.Abs(A[j] - A[i]) + Math.Abs(k - j) + Math.Abs(j - i);
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
