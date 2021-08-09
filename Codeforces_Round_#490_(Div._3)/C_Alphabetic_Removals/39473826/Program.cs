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
        private int N, K;
        private string S;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            K = sc.NextInt();
            S = sc.Next();
        }


        public void Solve()
        {
            Scan();
            var b = new bool[N];
            int cnt = 0;
            for (char c = 'a'; c <= 'z'; c++)
            {
                for (int i = 0; i < N; i++)
                {
                    if (S[i] == c)
                    {
                        b[i] = true;
                        cnt++;
                        if (cnt >= K) break;
                    }
                }
                if (cnt >= K) break;
            }
            var list = new List<char>();
            for (int i = 0; i < N; i++)
            {
                if (!b[i]) list.Add(S[i]);
            }
            Console.WriteLine(new string(list.ToArray()));
        }

        static void Main() => new Program().Solve();
    }
}
