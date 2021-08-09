using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading;

namespace Contest
{
    class Scanner
    {
        private string[] line = new string[0];
        private int index = 0;
        public string Next()
        {
            if (line.Length <= index)
            {
                line = Console.ReadLine().Split(' ');
                index = 0;
            }
            var res = line[index];
            index++;
            return res;
        }
        public int NextInt()
        {
            return int.Parse(Next());
        }
        public long NextLong()
        {
            return long.Parse(Next());
        }
        public string[] Array()
        {
            line = Console.ReadLine().Split(' ');
            index = line.Length;
            return line;
        }
        public int[] IntArray()
        {
            var array = Array();
            var result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = int.Parse(array[i]);
            }

            return result;
        }
        public long[] LongArray()
        {
            var array = Array();
            var result = new long[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = long.Parse(array[i]);
            }

            return result;
        }
    }

    class Program
    {
        private int N, K;
        private string[] S;
        private int[,] Ans;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            K = sc.NextInt();
            S = new string[N];
            for (int i = 0; i < N; i++)
            {
                S[i] = sc.Next();
            }
        }

        public void Solve()
        {
            Scan();
            Ans = new int[N, N];
            for (int i = K - 1; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    bool tate = true;
                    bool yoko = true;
                    for (int k = 0; k < K; k++)
                    {
                        if (S[i - k][j] == '#')
                        {
                            tate = false;
                        }

                        if (S[j][i - k] == '#')
                        {
                            yoko = false;
                        }
                    }
                    for (int k = 0; k < K; k++)
                    {
                        if (tate)
                        {
                            Ans[i - k, j]++;
                        }

                        if (yoko)
                        {
                            Ans[j, i - k]++;
                        }
                    }
                   
                }
            }

            int y = 0, x = 0;
            int max = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (max < Ans[i, j])
                    {
                        y = i;
                        x = j;
                        max = Ans[i, j];
                    }
                }
            }

            Console.WriteLine($"{y + 1} {x + 1}");
        }


        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}