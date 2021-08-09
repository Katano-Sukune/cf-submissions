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
        private long N, K, A, B;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            K = sc.NextInt();
            A = sc.NextInt();
            B = sc.NextInt();
        }

        public void Solve()
        {
            Scan();
            long ans = 0;
            if (K == 1)
            {
                Console.WriteLine((N - 1) * A);
                return;
            }
            while (N > 1)
            {
                if (N % K == 0)
                {
                    long aa = (N / K * (K - 1)) * A;
                    ans += Math.Min(aa, B);
                    N /= K;
                }
                else
                {
                    if (N / K == 0)
                    {
                        ans += (N - 1) * A;
                        N = 1;
                    }
                    else
                    {
                        ans += (N % K) * A;
                        N -= N % K;
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