using System;
using System.Collections.Generic;


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
        private string[] Sequence;
        void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            Sequence = new string[N];
            for (int i = 0; i < N; i++)
            {
                Sequence[i] = sc.Next();
            }
        }

        private void Count(string s, out int sum, out int min)
        {
            sum = 0;
            min = 0;
            foreach (char c in s)
            {
                if (c == '(')
                {
                    sum++;
                }
                else if (c == ')')
                {
                    sum--;
                }

                min = Math.Min(min, sum);
            }
        }

        public void Solve()
        {
            Scan();
            var minusSum = new Dictionary<int, int>();
            var plusSum = new Dictionary<int, int>();
            int zeroSum = 0;
            foreach (var s in Sequence)
            {
                int min, sum;
                Count(s, out sum, out min);
                if (sum > 0)
                {
                    if (min < 0) continue;
                    int o;
                    plusSum.TryGetValue(sum, out o);
                    plusSum[sum] = o + 1;
                }
                else if (sum < 0)
                {
                    if (min < sum) continue;
                    int o;
                    minusSum.TryGetValue(sum, out o);
                    minusSum[sum] = o + 1;
                }
                else
                {
                    if (min < 0) continue;
                    zeroSum++;
                }
            }

            int ans = 0;
            ans += zeroSum / 2;
            foreach (var i in plusSum)
            {
                int minus;
                minusSum.TryGetValue(-i.Key, out minus);
                ans += Math.Min(i.Value, minus);
            }
            Console.WriteLine(ans);
        }



        static void Main() => new Program().Solve();
    }


}