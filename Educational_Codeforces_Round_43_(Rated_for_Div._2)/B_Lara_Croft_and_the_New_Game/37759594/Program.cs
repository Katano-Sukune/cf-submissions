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
        private int N, M;
        private long K;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            M = sc.NextInt();
            K = sc.NextLong();
        }

        public void Solve()
        {
            Scan();
            if (K < N)
            {
                Console.WriteLine($"{K + 1} {1}");
            }
            else
            {
                long d = K - N;
                long x;
                long y = N - d / (M - 1);
                if ((d / (M - 1)) % 2 == 0)
                {
                    x = d % (M - 1) + 2;

                }
                else
                {
                    x = M - d % (M - 1);

                }
                Console.WriteLine($"{y} {x}");
            }
        }


        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}