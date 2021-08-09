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
        private int N;
        private int[] A;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            A = sc.IntArray();
        }

        public void Solve()
        {
            Scan();
            var b = new bool[1000001];
            for (int i = 0; i <= 1000; i++)
            {
                b[i * i] = true;
            }
            int ans = int.MinValue;
            foreach (int i in A)
            {
                if (i < 0 || !b[i])
                {
                    ans = Math.Max(i, ans);
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