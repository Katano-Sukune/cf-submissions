using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // 1のやつがいくつあるか
        var f = new bool[N];
        var ls = new List<int>();
        var ls2 = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (A[i] == 1)
            {
                ls.Add(i);
                f[i] = true;
            }
            else
            {
                ls2.Add(i);
            }
        }

        int m = 0;
        var sb = new StringBuilder();
        if (ls.Count == 0 || (ls.Count == 1 && (ls[0] == 0 || ls[0] == N - 1)))
        {
            // 全部一列
            for (int i = 1; i < N; i++)
            {
                m++;
                sb.AppendLine($"{i} {i + 1}");
            }

            Console.WriteLine($"YES {N - 1}");
            Console.WriteLine(m);
            Console.Write(sb);
            return;
        }
        else if (ls.Count == 1)
        {
            // 1のやつ右端残り適当
            m++;
            sb.AppendLine($"{ls[0] + 1} {1}");
            for (int i = 0; i < ls2.Count - 1; i++)
            {
                m++;
                sb.AppendLine($"{ls2[i] + 1} {ls2[i + 1] + 1}");
            }

            Console.WriteLine($"YES {N - 1}");
            Console.WriteLine(m);
            Console.Write(sb);
            return;
        }

        for (int i = 0; i < ls2.Count - 1; i++)
        {
            m++;
            sb.AppendLine($"{ls2[i] + 1} {ls2[i + 1] + 1}");
            A[ls2[i]]--;
            A[ls2[i + 1]]--;
        }

        if (ls2.Count == 0)
        {
            Console.WriteLine("NO");
            return;
        }
        
        {
            m += 2;
            sb.AppendLine($"{ls[0] + 1} {ls2[0] + 1}");
            A[ls2[0]]--;
            sb.AppendLine($"{ls[1] + 1} {ls2[ls2.Count - 1] + 1}");
            A[ls2[ls2.Count - 1]]--;
        }

        int j = 0;
        for (int i = 2; i < ls.Count; i++)
        {
            while (j < ls2.Count && A[ls2[j]] == 0)
            {
                j++;
            }

            if (j >= ls2.Count)
            {
                Console.WriteLine("NO");
                return;
            }

            m++;
            sb.AppendLine($"{ls[i] + 1} {ls2[j] + 1}");
            A[ls2[j]]--;
        }

        Console.WriteLine($"YES {ls2.Count + 1}");
        Console.WriteLine(m);
        Console.Write(sb);
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