using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        private string S;
        private void Scan()
        {
            var sc = new Scanner();
            S = sc.Next();
        }


        public void Solve()
        {
            Scan();
            int[] A = new int[S.Length];
            for (int i = 0; i < S.Length; i++)
            {
                switch (S[i])
                {
                    case 'A':
                        if (i > 0)
                        {
                            A[i - 1] += 1;
                        }

                        if (i < S.Length - 1)
                        {
                            A[i + 1] += 1;
                        }

                        A[i] += 1;
                        break;
                    case 'B':
                        if (i > 0)
                        {
                            A[i - 1] += 2;
                        }

                        if (i < S.Length - 1)
                        {
                            A[i + 1] += 2;
                        }

                        A[i] += 2;
                        break;
                    case 'C':
                        if (i > 0)
                        {
                            A[i - 1] += 4;
                        }

                        if (i < S.Length - 1)
                        {
                            A[i + 1] += 4;
                        }

                        A[i] += 4;
                        break;
                }
            }
            Console.WriteLine(A.Any(i => i == 7) ? "Yes" : "No");
        }

        static void Main() => new Program().Solve();
    }

}
