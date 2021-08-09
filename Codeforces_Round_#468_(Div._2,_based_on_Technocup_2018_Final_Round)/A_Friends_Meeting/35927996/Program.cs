using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return Array().Select(int.Parse).ToArray();
        }
        public long[] LongArray()
        {
            return Array().Select(long.Parse).ToArray();
        }
    }

    class Program
    {
        private int A, B;
        private void Scan()
        {
            var sc = new Scanner();
            A = sc.NextInt();
            B = sc.NextInt();
        }

        private int AA(int n)
        {
            return n * (n + 1) / 2;
        }

        public void Solve()
        {
            Scan();
            var dist = Math.Abs(A - B);
            Console.WriteLine(AA(dist / 2) + AA((dist + 1) / 2));
        }

        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}