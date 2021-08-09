using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;
using System.Xml.Schema;

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

        public ulong NextUlong()
        {
            return ulong.Parse(Next());
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
        private int N;
        private int[] A, B;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            A = sc.IntArray().Select(i => i - 1).ToArray();
            B = sc.IntArray().Select(i => i - 1).ToArray();
        }

        public void Solve()
        {
            Scan();
            int max = -1;
            int[] map = new int[N];
            for (int i = 0; i < N; i++)
            {
                map[A[i]] = i;
            }

            int[] ans = new int[N];

            for (int i = 0; i < N; i++)
            {
                int index = map[B[i]];
                ans[i] = Math.Max(0, index - max);
                max = Math.Max(max, index);
            }
            Console.WriteLine(string.Join(" ", ans));
        }
        static void Main() => new Program().Solve();
    }
}