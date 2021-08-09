using System;
using System.Linq;
using System.Threading;

public class Program
{
    private int N;

    public void Solve()
    {
        N = int.Parse(Console.ReadLine());
        // (0,1) が0,1の場合
        int[][] O = new int[N][];
        int[][] I = new int[N][];

        for (int i = 0; i < N; i++)
        {
            O[i] = new int[N];
            I[i] = new int[N];
            Array.Fill(O[i], -1);
            Array.Fill(I[i], -1);
        }

        #region 左上4つ + 右下

        O[0][0] = 1;
        I[0][0] = 1;

        if (W(0, 0, 1, 1))
        {
            O[1][1] = 1;
            I[1][1] = 1;
        }
        else
        {
            O[1][1] = 0;
            I[1][1] = 0;
        }


        O[0][1] = 0;
        I[0][1] = 1;

        if (W(0, 1, 1, 2))
        {
            O[1][2] = 0;
            I[1][2] = 1;
        }
        else
        {
            O[1][2] = 1;
            I[1][2] = 0;
        }

        if (W(1, 0, 1, 2))
        {
            O[1][0] = O[1][2];
            I[1][0] = I[1][2];
        }
        else
        {
            O[1][0] = 1 - O[1][2];
            I[1][0] = 1 - I[1][2];
        }

        O[N - 1][N - 1] = 0;
        I[N - 1][N - 1] = 0;

        #endregion

        #region 左2列

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                if (i + 2 < N && O[i + 2][j] == -1)
                {
                    if (W(i, j, i + 2, j))
                    {
                        O[i + 2][j] = O[i][j];
                        I[i + 2][j] = I[i][j];
                    }
                    else
                    {
                        O[i + 2][j] = 1 - O[i][j];
                        I[i + 2][j] = 1 - I[i][j];
                    }
                }
            }
        }

        #endregion

        #region 全体

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (j + 2 < N && O[i][j + 2] == -1)
                {
                    if (W(i, j, i, j + 2))
                    {
                        O[i][j + 2] = O[i][j];
                        I[i][j + 2] = I[i][j];
                    }
                    else
                    {
                        O[i][j + 2] = 1 - O[i][j];
                        I[i][j + 2] = 1- I[i][j];
                    }
                }
            }
        }

        #endregion

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                // 下に移動する回数
                // 回文になる方法が存在するか?
                bool[] ii = new bool[4];
                bool[] oo = new bool[4];
                for (int k = 0; k < 8; k++)
                {
                    int tmp = k;
                    int[] xx = new int[4];
                    int[] yy = new int[4];
                    xx[0] = i;
                    yy[0] = j;

                    int idx = 0;
                    for (int l = 0; l < 3; l++)
                    {
                        xx[l + 1] = xx[l];
                        yy[l + 1] = yy[l];
                        if (tmp % 2 == 0)
                        {
                            xx[l + 1]++;
                            idx++;
                        }
                        else
                        {
                            yy[l + 1]++;
                        }
                        tmp /= 2;
                    }

                    if (xx[3] >= N || yy[3] >= N)
                    {
                        continue;
                    }

                    ii[idx] |= I[xx[0]][yy[0]] == I[xx[3]][yy[3]] && I[xx[1]][yy[1]] == I[xx[2]][yy[2]];
                    oo[idx] |= O[xx[0]][yy[0]] == O[xx[3]][yy[3]] && O[xx[1]][yy[1]] == O[xx[2]][yy[2]];
                }

                for (int k = 0; k <= 3 ; k++)
                {
                    if (ii[k] != oo[k])
                    {
                        // Console.WriteLine($"{i} {j} {k} {ii[k]} {oo[k]}");
                        if (W(i, j, i + k, j + (3 - k)) == ii[k])
                        {
                            Anser(I);
                        }
                        else
                        {
                            Anser(O);
                        }
                        return;
                    }
                }
            }
        }
    }

    void Anser(int[][] t)
    {
        Console.WriteLine("!");
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(string.Join("", t[i]));
        }
    }


    bool W(int x1, int y1, int x2, int y2)
    {
        Console.WriteLine($"? {x1 + 1} {y1 + 1} {x2 + 1} {y2 + 1}");
        return Console.ReadLine() == "1";
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}