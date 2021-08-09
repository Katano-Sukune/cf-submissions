using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Console.WriteLine(Q(sc.Next()));
        }
        Console.Out.Flush();
    }

    string Q(string s)
    {
        // 上下左右に移動



        // 後ろから以降の x,y最大、最小

        var bYMin = new long[s.Length + 1];
        var bYMax = new long[s.Length + 1];
        var bXMin = new long[s.Length + 1];
        var bXMax = new long[s.Length + 1];
        {
            int y = 0;
            int x = 0;
            long yMin = 0;
            long yMax = 0;
            long xMin = 0;
            long xMax = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                switch (s[i])
                {
                    case 'S':
                        y++;
                        break;
                    case 'W':
                        y--;
                        break;
                    case 'A':
                        x++;
                        break;
                    case 'D':
                        x--;
                        break;
                }
                yMin = Math.Min(yMin, y);
                yMax = Math.Max(yMax, y);
                xMin = Math.Min(xMin, x);
                xMax = Math.Max(xMax, x);
                bYMax[i] = yMax - y;
                bYMin[i] = yMin - y;
                bXMax[i] = xMax - x;
                bXMin[i] = xMin - x;
            }
        }
        long ans = (bYMax[0] - bYMin[0] + 1) * (bXMax[0] - bXMin[0] + 1);
        {
            long y = 0;
            long x = 0;
            long fYMin = 0;
            long fYMax = 0;
            long fXMin = 0;
            long fXMax = 0;
            for (int i = 0; i <= s.Length; i++)
            {
                {
                    long xMin = Math.Min(fXMin, x + bXMin[i]);
                    long xMax = Math.Max(fXMax, x + bXMax[i]);
                    // w追加
                    {
                        long yMin = Math.Min(fYMin, y + 1 + bYMin[i]);
                        long yMax = Math.Max(fYMax, Math.Max(y + 1, y + 1 + bYMax[i]));
                        ans = Math.Min(ans, (yMax - yMin + 1) * (xMax - xMin + 1));
                    }
                    {
                        // s
                        long yMin = Math.Min(fYMin, Math.Min(y - 1, y - 1 + bYMin[i]));
                        long yMax = Math.Max(fYMax, y - 1 + bYMax[i]);
                        ans = Math.Min(ans, (yMax - yMin + 1) * (xMax - xMin + 1));
                    }
                }

                {
                    long yMin = Math.Min(fYMin, y + bYMin[i]);
                    long yMax = Math.Max(fYMax, y + bYMax[i]);
                    // d追加
                    {
                        long xMin = Math.Min(fXMin, x + 1 + bXMin[i]);
                        long xMax = Math.Max(fXMax, Math.Max(x + 1, x + 1 + bXMax[i]));
                        ans = Math.Min(ans, (yMax - yMin + 1) * (xMax - xMin + 1));
                    }
                    {
                        // a
                        long xMin = Math.Min(fXMin, Math.Min(x - 1, x - 1 + bXMin[i]));
                        long xMax = Math.Max(fXMax, x - 1 + bXMax[i]);
                        ans = Math.Min(ans, (yMax - yMin + 1) * (xMax - xMin + 1));
                    }
                }

                if (i < s.Length)
                {
                    switch (s[i])
                    {
                        case 'W':
                            y++;
                            break;
                        case 'S':
                            y--;
                            break;
                        case 'D':
                            x++;
                            break;
                        case 'A':
                            x--;
                            break;
                    }
                    fYMin = Math.Min(fYMin, y);
                    fYMax = Math.Max(fYMax, y);
                    fXMin = Math.Min(fXMin, x);
                    fXMax = Math.Max(fXMax, x);
                }
            }
        }
        return ans.ToString();
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
