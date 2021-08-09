using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private string S;
    private int X, Y;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();
        X = sc.NextInt();
        Y = sc.NextInt();

        {
            // 0
            int xx = S.Count(c => c == 'R') - S.Count(c => c == 'L');
            int yy = S.Count(c => c == 'U') - S.Count(c => c == 'D');
            if (xx == X && yy == Y)
            {
                Console.WriteLine("0");
                return;
            }
        }

        int ok = N + 1;
        int ng = 0;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        if (ok > N) Console.WriteLine("-1");
        else Console.WriteLine(ok);
    }

    bool F(int len)
    {
        int xx = 0;
        int yy = 0;
        for (int i = len; i < N; i++)
        {
            switch (S[i])
            {
                case 'U':
                    yy++;
                    break;
                case 'D':
                    yy--;
                    break;
                case 'R':
                    xx++;
                    break;
                case 'L':
                    xx--;
                    break;
            }
        }

        for (int i = len; i <= N; i++)
        {
            // [i-len, i) 変える
            // それ以外変えない

            int diff = Math.Abs(Y - yy) + Math.Abs(X - xx);
            if (diff <= len && diff % 2 == len % 2) return true;

            if (i < N)
            {
                switch (S[i-len])
                {
                    case 'U':
                        yy++;
                        break;
                    case 'D':
                        yy--;
                        break;
                    case 'R':
                        xx++;
                        break;
                    case 'L':
                        xx--;
                        break;
                }
                
                switch (S[i])
                {
                    case 'U':
                        yy--;
                        break;
                    case 'D':
                        yy++;
                        break;
                    case 'R':
                        xx--;
                        break;
                    case 'L':
                        xx++;
                        break;
                }
            }
        }

        return false;
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