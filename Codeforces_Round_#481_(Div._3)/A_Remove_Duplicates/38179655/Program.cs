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
            var map = new Dictionary<int, int>();
            for (int i = 0; i < N; i++)
            {
                map[A[i]] = i;
            }
            var arr = map.ToArray();
            Array.Sort(arr, (a, b) => a.Value.CompareTo(b.Value));
            Console.WriteLine(arr.Length);
            Console.WriteLine(string.Join(" ", arr.Select(a => a.Key)));
        }

        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}