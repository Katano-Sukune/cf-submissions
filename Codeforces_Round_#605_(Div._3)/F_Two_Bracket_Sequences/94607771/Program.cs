using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private string S, T;
    private const int MaxCnt = 400;

    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        T = sc.Next();

        bool[,,] dp = new bool[MaxCnt + 1, S.Length + 1, T.Length + 1];
        (int cnt, int s, int t)[,,] prev = new (int cnt, int s, int t)[MaxCnt + 1, S.Length + 1, T.Length + 1];

        var q = new Queue<(int cnt, int s, int t)>();
        q.Enqueue((0, 0, 0));
        dp[0, 0, 0] = true;

        while (q.Count > 0)
        {
            (int cnt, int s, int t) = q.Dequeue();
            // )
            if (cnt + 1 <= MaxCnt)
            {
                int nextS = s == S.Length ? S.Length : S[S.Length - s - 1] == ')' ? s + 1 : s;
                int nextT = t == T.Length ? T.Length : T[T.Length - t - 1] == ')' ? t + 1 : t;
                if (!dp[cnt + 1, nextS, nextT])
                {
                    dp[cnt + 1, nextS, nextT] = true;
                    prev[cnt + 1, nextS, nextT] = (cnt, s, t);
                    q.Enqueue((cnt + 1, nextS, nextT));
                }
            }

            // (
            if (cnt - 1 >= 0)
            {
                int nextS = s == S.Length ? S.Length : S[S.Length - s - 1] == '(' ? s + 1 : s;
                int nextT = t == T.Length ? T.Length : T[T.Length - t - 1] == '(' ? t + 1 : t;
                if (!dp[cnt - 1, nextS, nextT])
                {
                    dp[cnt - 1, nextS, nextT] = true;
                    prev[cnt - 1, nextS, nextT] = (cnt, s, t);
                    q.Enqueue((cnt - 1, nextS, nextT));
                }
            }
        }

        List<char> ans = new List<char>();
        int curCnt = 0;
        int curS = S.Length;
        int curT = T.Length;
        while (!(curCnt == 0 && curS == 0 && curT == 0))
        {
            if (prev[curCnt, curS, curT].cnt + 1 == curCnt)
            {
                ans.Add(')');
            }
            else
            {
                ans.Add('(');
            }

            (curCnt, curS, curT) = prev[curCnt, curS, curT];
        }

        Console.WriteLine(new string(ans.ToArray()));
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