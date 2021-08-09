using System;
using System.Collections.Generic;
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
        private int N;
        private string S;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            S = sc.Next();
        }


        public void Solve()
        {
            Scan();
            char[] ans = S.ToCharArray();
            for (int i = 1; i <= N; i++)
            {
                if (N % i == 0)
                {
                    var next = ans.ToArray();
                    for (int j = 0; j < i; j++)
                    {
                        next[j] = ans[i - 1 - j];
                    }

                    ans = next.ToArray();
                }
            }
            Console.WriteLine(new string(ans));
        }

        static void Main() => new Program().Solve();
    }
}
