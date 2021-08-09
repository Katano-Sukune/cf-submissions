using System;


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
        private string card;
        private string[] myCard;
        void Scan()
        {
            var sc = new Scanner();
            card = sc.Next();
            myCard = sc.Array();
        }

        public void Solve()
        {
            Scan();
            foreach (var s in myCard)
            {
                if (card[0] == s[0] || card[1] == s[1])
                {
                    Console.WriteLine("YES");
                    return;
                }

            }
            Console.WriteLine("NO");
        }



        static void Main() => new Program().Solve();
    }


}