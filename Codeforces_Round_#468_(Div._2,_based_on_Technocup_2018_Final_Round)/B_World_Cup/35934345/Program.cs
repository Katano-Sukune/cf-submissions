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
        private int N, A, B;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            A = sc.NextInt() - 1;
            B = sc.NextInt() - 1;
        }

        private bool BB(int n, int i)
        {
            return (n & (1 << i)) > 0;
        }


        public void Solve()
        {
            Scan();
            for (int i = 16; i >= 0; i--)
            {
                if (BB(A, i) ^ BB(B, i))
                {
                    int m = i + 1;
                    if ((int)Math.Log(N, 2) == m)
                    {
                        Console.WriteLine("Final!");
                    }
                    else
                    {
                        Console.WriteLine(m);
                    }
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}