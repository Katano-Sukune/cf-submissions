using System;
using System.Collections.Generic;
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
        private int N, M;
        private int[] X, Y;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            M = sc.NextInt();
            X = sc.IntArray();
            Y = sc.IntArray();
        }


        public void Solve()
        {
            Scan();
            List<int> l = new List<int>();
            foreach (var i in X)
            {
                if (Y.Contains(i))
                {
                    l.Add(i);
                }
            }
            Console.WriteLine(string.Join(" ", l));
        }


        static void Main() => new Program().Solve();
    }

}
