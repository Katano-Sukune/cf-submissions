using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Remoting.Contexts;

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

        public int[] IntArray(int n)
        {
            var array = Array();
            var result = new int[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = int.Parse(array[i]);
            }

            return result;
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

        public uint NextUint()
        {
            return uint.Parse(Next());
        }
    }

    class Program
    {
        private int N, Q;
        private int[] A;
        private int[] B;

        private int[] Count;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            Q = sc.NextInt();
            A = sc.IntArray();
            B = new int[Q];
            for (int i = 0; i < Q; i++)
            {
                B[i] = sc.NextInt();
            }
        }

        private int Query(int i)
        {
            int result = 0;
            int n = B[i];
            var aa = ToBit(n);
            //Console.WriteLine(string.Join(" ", aa));
            for (int ii = 30; ii >= 0; ii--)
            {
                if (Count[ii] >= aa[ii])
                {
                    result += aa[ii];
                }
                else
                {
                    if (ii == 0) return -1;
                    result += Count[ii];
                    aa[ii - 1] += (aa[ii] - Count[ii]) * 2;
                }
            }

            return result;
        }

        public void Solve()
        {
            Scan();
            Count = new int[31];
            var cpA = A.Select(Base2Log).ToArray();
            for (int i = 0; i < N; i++)
            {
                Count[cpA[i]]++;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Q; i++)
            {
                sb.AppendLine(Query(i).ToString());
            }
            Console.Write(sb.ToString());
        }

        private int[] ToBit(int n)
        {
            var l = new List<int>();
            for (int i = 0; i <= 30; i++)
            {
                if ((n & (1 << i)) > 0)
                {
                    l.Add(1);
                }
                else
                {
                    l.Add(0);
                }
            }

            return l.ToArray();
        }

        private int Base2Log(int n)
        {
            for (int i = 0; i <= 30; i++)
            {
                if ((1 << i) == n)
                {
                    return i;
                }
            }

            return 31;
        }

        static void Main() => new Program().Solve();
    }
}