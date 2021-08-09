using System;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private Scanner Sc;

    private readonly int[] Dx = new int[] {0, 1, 2};
    private readonly int[] Dy = new int[] {2, 1, 0};

    private readonly int[] Dx2 = new int[] {0, 1, 2, 3};
    private readonly int[] Dy2 = new int[] {3, 2, 1, 0};

    public void Solve()
    {
        Sc = new Scanner();
        N = Sc.NextInt();
        int[][] t1 = new int[N][];
        int[][] t2 = new int[N][];
        for (int i = 0; i < N; i++)
        {
            t1[i] = new int[N];
            t2[i] = new int[N];
            for (int j = 0; j < N; j++)
            {
                t1[i][j] = t2[i][j] = -1;
            }
        }

        t1[0][0] = t2[0][0] = 1;
        t1[N - 1][N - 1] = t2[N - 1][N - 1] = 0;
        t1[0][1] = 0;
        t2[0][1] = 1;

        #region (2,1)

        {
            if (Query(0, 1, 2, 1))
            {
                t1[2][1] = t1[0][1];
                t2[2][1] = t2[0][1];
            }
            else
            {
                t1[2][1] = 1 - t1[0][1];
                t2[2][1] = 1 - t2[0][1];
            }
        }

        #endregion

        #region (1,0)

        {
            if (Query(1, 0, 2, 1))
            {
                t1[1][0] = t1[2][1];
                t2[1][0] = t2[2][1];
            }
            else
            {
                t1[1][0] = 1 - t1[2][1];
                t2[1][0] = 1 - t2[2][1];
            }
        }

        #endregion

        #region 他

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int ni = i + Dy[k];
                    int nj = j + Dx[k];
                    if (ni >= N || nj >= N) continue;
                    if (t1[ni][nj] != -1) continue;

                    if (Query(i, j, ni, nj))
                    {
                        t1[ni][nj] = t1[i][j];
                        t2[ni][nj] = t2[i][j];
                    }
                    else
                    {
                        t1[ni][nj] = 1 - t1[i][j];
                        t2[ni][nj] = 1 - t2[i][j];
                    }
                }
            }
        }

        #endregion

        #region Check

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    int ni = i + Dx2[k];
                    int nj = j + Dy2[k];
                    if (ni >= N || nj >= N) continue;
                    bool q = DryQ(i, j, ni, nj, t1);
                    bool w = DryQ(i, j, ni, nj, t2);
                    if (q == w) continue;

                    bool e = Query(i, j, ni, nj);
                    if (q == e)
                    {
                        Write(t1);
                    }
                    else
                    {
                        Write(t2);
                    }

                    return;
                }
            }
        }

        throw new Exception();

        #endregion
    }

    void Write(int[][] t)
    {
        var sb = new StringBuilder();
        sb.AppendLine("!");
        for (int i = 0; i < N; i++)
        {
            sb.AppendLine(string.Join("", t[i]));
        }

        Console.Write(sb);
    }


    bool DryQ(int x1, int y1, int x2, int y2, int[][] t)
    {
        if (t[x1][y1] != t[x2][y2]) return false;
        if (x1 == x2)
        {
            if (t[x1][y1 + 1] == t[x1][y1 + 2]) return true;
        }
        else if (x1 + 1 == x2)
        {
            if (t[x1][y1 + 1] == t[x1][y1 + 2]) return true;
            if (t[x1][y1 + 1] == t[x1 + 1][y1 + 1]) return true;
            if (t[x1 + 1][y1] == t[x1 + 1][y1 + 1]) return true;
        }
        else if (x1 + 2 == x2)
        {
            if (t[x1 + 1][y1] == t[x1 + 2][y1]) return true;
            if (t[x1][y1 + 1] == t[x1 + 1][y1 + 1]) return true;
            if (t[x1 + 1][y1] == t[x1 + 1][y1 + 1]) return true;
        }
        else if (x1 + 3 == x2)
        {
            if (t[x1 + 1][y1] == t[x1 + 2][y1]) return true;
        }

        return false;
    }

    bool Query(int x1, int y1, int x2, int y2)
    {
        Console.WriteLine($"? {x1 + 1} {y1 + 1} {x2 + 1} {y2 + 1}");
        return Sc.NextInt() == 1;
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