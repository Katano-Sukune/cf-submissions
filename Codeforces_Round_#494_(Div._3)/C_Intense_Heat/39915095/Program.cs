using System;
using System.Collections.Generic;
using System.Globalization;
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
        private int N, K;
        private int[] A;
        private int[] Sum;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            K = sc.NextInt();
            A = sc.IntArray();
        }
        //[i,j)
        private int Q(int i, int j)
        {
            return Sum[j] - Sum[i];
        }


        public void Solve()
        {
            Scan();
            Sum = new int[N + 1];
            for (int i = 0; i < N; i++)
            {
                Sum[i + 1] = Sum[i] + A[i];
            }

            double ans = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = i + K; j <= N; j++)
                {
                    ans = Math.Max((double)Q(i, j) / (j - i), ans);
                }
            }
            Console.WriteLine(ans.ToString(CultureInfo.InvariantCulture));
        }

        static void Main() => new Program().Solve();
    }
}