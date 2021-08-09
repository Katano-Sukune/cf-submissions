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
        private int A, B, X;
        private void Scan()
        {
            var sc = new Scanner();
            A = sc.NextInt();
            B = sc.NextInt();
            X = sc.NextInt();
        }


        public void Solve()
        {
            Scan();
            int n = A + B;
            if (X == 1)
            {
                Console.WriteLine(new string('1', B) + new string('0', A));
                return;
            }
            string ans = "";

            if (A > B)
            {
                ans += '0';
                A--;
                int aa = X / 2;
                int bb = X - aa;
                for (int i = 0; i < X; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (bb == 1)
                        {
                            ans += new string('1', B);
                        }
                        else
                        {
                            ans += '1';
                            bb--;
                            B--;
                        }
                    }
                    else
                    {
                        if (aa == 1)
                        {
                            ans += new string('0', A);
                        }
                        else
                        {


                            ans += '0';
                            aa--;
                            A--;
                        }
                    }
                    //Console.WriteLine($"{A} {B}");
                }

            }
            else
            {
                ans += '1';
                B--;
                int bb = X / 2;
                int aa = X - bb;
                for (int i = 0; i < X; i++)
                {
                    if (i % 2 == 1)
                    {
                        if (bb == 1)
                        {
                            ans += new string('1', B);
                        }
                        else
                        {
                            ans += '1';
                            bb--;
                            B--;
                        }
                    }
                    else
                    {
                        if (aa == 1)
                        {
                            ans += new string('0', A);
                        }
                        else
                        {


                            ans += '0';
                            aa--;
                            A--;
                        }
                    }
                    //Console.WriteLine($"{A} {B}");
                }
            }
            Console.WriteLine(ans);
        }

        static void Main() => new Program().Solve();
    }
}