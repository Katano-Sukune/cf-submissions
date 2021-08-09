using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[][] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = new int[2];
            A[i][0] = sc.NextInt() - 1;
            A[i][1] = sc.NextInt() - 1;
        }

        // 子供が円形に並んでる
        // 番号p_i

        // a[i] 子供番号iの前、もういっこ前にいる子供の番号

        {
            // 0の前、　A[0] A[1]
            var ans = new int[N];
            ans[0] = 0;
            ans[1] = A[0][0];
            ans[2] = A[0][1];
            bool flag = true;
            for (int i = 1; i < N - 2; i++)
            {
                int cur = ans[i];
                int next = ans[i + 1];
                if (A[cur][0] == next)
                {
                    ans[i + 2] = A[cur][1];
                }
                else if (A[cur][1] == next)
                {
                    ans[i + 2] = A[cur][0];
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                // n-2

                int cur = ans[N - 2];
                int next = ans[N - 1];
                bool a = (A[cur][0] == next && A[cur][1] == 0) || (A[cur][0] == 0 && A[cur][1] == next);
                bool b = (A[next][0] == 0 && A[next][1] == ans[1]) || (A[next][0] == ans[1] && A[next][1] == 0);
                var unique = new HashSet<int>();
                bool c = true;
                for (int i = 0; i < N; i++)
                {
                    c &= unique.Add(ans[i]);
                }
                if (a && b && c)
                {
                    Console.WriteLine(string.Join(" ", ans.Select(i => i + 1)));
                    return;
                }
            }
        }
        {
            // 0の前、　A[1] A[0]
            var ans = new int[N];
            ans[0] = 0;
            ans[1] = A[0][1];
            ans[2] = A[0][0];
            bool flag = true;
            for (int i = 1; i < N - 2; i++)
            {
                int cur = ans[i];
                int next = ans[i + 1];
                if (A[cur][0] == next)
                {
                    ans[i + 2] = A[cur][1];
                }
                else if (A[cur][1] == next)
                {
                    ans[i + 2] = A[cur][0];
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                // n-2

                int cur = ans[N - 2];
                int next = ans[N - 1];
                bool a = (A[cur][0] == next && A[cur][1] == 0) || (A[cur][0] == 0 && A[cur][1] == next);
                bool b = (A[next][0] == 0 && A[next][1] == ans[1]) || (A[next][0] == ans[1] && A[next][1] == 0);
                var unique = new HashSet<int>();
                bool c = true;
                for (int i = 0; i < N; i++)
                {
                    c &= unique.Add(ans[i]);
                }
                if (a && b && c)
                {
                    Console.WriteLine(string.Join(" ", ans.Select(i => i + 1)));
                    return;
                }
            }
        }
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
