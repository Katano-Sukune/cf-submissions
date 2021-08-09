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
        private int X, Y;
        private void Scan()
        {
            var sc = new Scanner();
            X = sc.NextInt();
            Y = sc.NextInt();
        }

        public void Solve()
        {
            Scan();
            if (X == 1)
            {
                if (Y == 1)
                {
                    Console.WriteLine("=");
                }
                else
                {
                    Console.WriteLine("<");
                }
                return;
            }
            else if (Y == 1)
            {
                Console.WriteLine(">");
                return;
            }
            var a = Math.Log(X, Y);
            var b = (double)X / Y;
            //Console.WriteLine($"{a} {b}");
            if (a > b)
            {
                Console.WriteLine(">");
            }
            else if (a < b)
            {
                Console.WriteLine("<");
            }
            else
            {
                Console.WriteLine("=");
            }
        }

        static void Main() => new Program().Solve();
    }
}