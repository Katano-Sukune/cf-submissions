using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;

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
        private int N;
        private string[] S;

        private Dictionary<string, int> col = new Dictionary<string, int>
        {
            {"purple", 0},
            {"green", 1},
            {"blue", 2},
            {"orange", 3},
            {"red", 4},
            {"yellow", 5},

        };
        private string[] Gem = new string[] { "Power", "Time", "Space", "Soul", "Reality", "Mind" };
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            S = new string[N];
            for (int i = 0; i < N; i++)
            {
                S[i] = sc.Next();
            }
        }

        public void Solve()
        {
            Scan();
            bool[] b = new bool[6];
            foreach (var s in S)
            {
                b[col[s]] = true;
            }
            Console.WriteLine(b.Count(bb => !bb));
            for (int i = 0; i < 6; i++)
            {
                if (!b[i])
                    Console.WriteLine(Gem[i]);
            }
        }

        static void Main() => new Program().Solve();
    }
}