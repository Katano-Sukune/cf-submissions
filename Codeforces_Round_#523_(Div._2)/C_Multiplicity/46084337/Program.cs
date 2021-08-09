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
            int mod = 1000000007;
            int[] cnt = new int[1000001];
            cnt[0] = 1;
            for (int i = 0; i < N; i++)
            {
                var list = new List<int>();
                for (int j = 1; j * j <= A[i]; j++)
                {
                    if (A[i] % j == 0)
                    {

                        int k = A[i] / j;
                        if (k != j)
                            list.Add(j);
                        cnt[k] += cnt[k - 1];
                        cnt[k] %= mod;
                    }
                }

                for (int j = list.Count - 1; j >= 0; j--)
                {
                    cnt[list[j]] += cnt[list[j] - 1];
                    cnt[list[j]] %= mod;
                }
            }


            long ans = 0;
            for (int i = 1; i <= 1000000; i++)
            {
                ans += cnt[i];
                ans %= mod;
            }
            Console.WriteLine(ans);
        }

        static void Main() => new Program().Solve();
    }
}