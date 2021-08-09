using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Program
{
    int N, M, K;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        // 右　端まで M-1 R 
        // 下　上　左 0まで M-1 DUL
        // D 1

        // 上の N-1回
        // M-1 R
        // M-1 L
        // N-1 U

        if ((M - 1) * N * 2 + (N - 1) * M * 2 < K)
        {
            Console.WriteLine("NO");
            return;
        }

        if (M == 1)
        {
            if (K <= N - 1)
            {
                Console.WriteLine("YES");
                Console.WriteLine("1");
                Console.WriteLine($"{K} D");
            }
            else
            {
                Console.WriteLine("YES");
                Console.WriteLine(2);
                Console.WriteLine($"{N - 1} D");
                Console.WriteLine($"{K - (N - 1)} U");
            }
            return;
        }

        int a = 0;
        int cnt = 0;
        var sb = new StringBuilder();
        for (int i = 0; i < N - 1; i++)
        {
            {
                if (cnt + M - 1 >= K)
                {

                    sb.AppendLine($"{K - cnt} R");
                    cnt += K - cnt;
                    a++;

                    Write(a, sb.ToString());
                    return;
                }
                else
                {
                    sb.AppendLine($"{M - 1} R");
                    cnt += M - 1;
                    a++;
                }

            }
            {
                if (cnt + 3 * (M - 1) >= K)
                {
                    int diff = K - cnt;
                    int d = diff / 3;
                    int m = diff % 3;
                    if (d > 0)
                    {
                        sb.AppendLine($"{d} DUL");
                        a++;
                    }
                    if (m == 1)
                    {
                        sb.AppendLine("1 D");
                        a++;
                    }
                    if (m == 2)
                    {
                        sb.AppendLine("1 DU");
                        a++;
                    }
                    cnt += K - cnt;
                    Write(a, sb.ToString());
                    return;
                }
                else
                {
                    sb.AppendLine($"{M - 1} DUL");
                    cnt += 3 * (M - 1);
                    a++;
                }
            }
            {
                sb.AppendLine("1 D");
                a++;
                cnt++;
                if (cnt == K)
                {
                    Write(a, sb.ToString());
                    return;
                }
            }
        }
        if (cnt + M - 1 >= K)
        {
            sb.AppendLine($"{K - cnt} R");
            cnt += K - cnt;
            a++;
            Write(a, sb.ToString());
            return;
        }
        else
        {
            sb.AppendLine($"{M - 1} R");
            cnt += M - 1;
            a++;
        }

        if (cnt + M - 1 >= K)
        {
            sb.AppendLine($"{K - cnt} L");
            cnt += K - cnt;
            a++;
            Write(a, sb.ToString());
            return;
        }
        else
        {
            sb.AppendLine($"{M - 1} L");
            cnt += M - 1;
            a++;
        }

        if (cnt + N - 1 >= K)
        {
            sb.AppendLine($"{K - cnt} U");
            cnt += K - cnt;
            a++;
            Write(a, sb.ToString());
            return;
        }
        else
        {
            sb.AppendLine($"{N - 1} U");
            cnt += N - 1;
            a++;
        }
        cnt += M - 1;
        Write(a, sb.ToString());
    }

    void Write(int a, string s)
    {
        Console.Write($"YES\n{a}\n{s}");
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}