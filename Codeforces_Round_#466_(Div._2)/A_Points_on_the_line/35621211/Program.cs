using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return Array().Select(int.Parse).ToArray();
        }
        public long[] LongArray()
        {
            return Array().Select(long.Parse).ToArray();
        }
    }

    class Program
    {
        private int N, D;
        private int[] X;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            D = sc.NextInt();
            X = sc.IntArray();
        }

        public void Solve()
        {
            Scan();
            Array.Sort(X);
            int ans = N;
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    if (X[j] - X[i] <= D)
                    {
                        ans = Math.Min(N - j + i - 1,ans);
                    }
                }
            }
            Console.WriteLine(ans);
        }

        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}