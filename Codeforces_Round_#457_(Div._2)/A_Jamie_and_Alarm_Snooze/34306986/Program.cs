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

    class Program
    {
        private int X;
        private int HH, MM;
        private void Scan()
        {
            var sc = new Scanner();
            X = sc.NextInt();
            HH = sc.NextInt();
            MM = sc.NextInt();
          
        }

        private bool C()
        {
            if (HH % 10 == 7)
            {
                return true;
            }
            if (MM % 10 == 7)
            {
                return true;
            }
            return false;
        }

        public void Solve()
        {
            Scan();
            for (int i = 0; i <= 86400; i++)
            {
                if (C())
                {
                    Console.WriteLine(i);
                    return;
                }
                MM -= X;
                if (MM < 0)
                {
                    HH--;
                    MM += 60;
                }
                if (HH < 0)
                {
                    HH += 24;
                }
            }
        }


        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}
