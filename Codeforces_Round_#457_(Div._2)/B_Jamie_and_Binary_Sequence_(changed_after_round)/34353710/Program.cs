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

    class HashMap<K, V> : Dictionary<K, V>
    {
        new public V this[K i]
        {
            get
            {
                V v;
                return TryGetValue(i, out v) ? v : base[i] = default(V);
            }
            set { base[i] = value; }
        }
    }

    class Program
    {
        private long N;
        private int K;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextLong();
            K = sc.NextInt();
        }

        public void Solve()
        {
            Scan();
            var map = new HashMap<int, int>();
            int min = int.MaxValue;
            int max = 0;
            int ii = 0;
            int cnt = 0;
            while (N > 0)
            {
                if (N % 2 == 1)
                {
                    map[ii] = 1;
                    min = Math.Min(min, ii);
                    max = Math.Max(max, ii);
                    cnt++;
                }
                N /= 2;
                ii++;
            }
            if (cnt > K)
            {
                Console.WriteLine("No");
                return;
            }
            while (K - cnt >= map[max])
            {
                cnt += map[max];
                map[max - 1] += map[max] * 2;
                map[max] = 0;
                max--;
                min = Math.Min(max, min);
            }
            for (; cnt < K; cnt++)
            {
                map[min]--;
                map[min - 1] += 2;
                min--;
            }
            Console.WriteLine("Yes");
            var l = new List<int>();
            for (int i = max; i >= min; i--)
            {
                for (int j = 0; j < map[i]; j++)
                {
                    l.Add(i);
                }
            }
            Console.WriteLine(string.Join(" ", l));
        }

        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}
