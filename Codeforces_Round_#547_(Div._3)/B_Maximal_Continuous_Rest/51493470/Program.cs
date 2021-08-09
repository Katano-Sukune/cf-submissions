using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Contest
{
    class Program
    {
        public void Solve()
        {
            var sc = new Scanner();
            int N = sc.NextInt();
            int[] A = sc.IntArray();

            int max = 0;
            bool b = false;
            int cnt = 0;
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    if (A[i] == 1)
                    {
                        if (!b)
                        {
                            b = true;
                            cnt = 0;
                        }

                        cnt++;
                        max = Math.Max(cnt, max);
                    }
                    else if (A[i] == 0)
                    {
                        if (b)
                        {
                            b = false;
                        }
                    }
                }
            }

            if (N == max / 2)
            {
                Console.WriteLine(max / 2);
                return;
            }
            Console.WriteLine(max);

        }

        static void Main(string[] args) => new Program().Solve();
    }

    class Scanner
    {
        private char _separator = ' ';
        private StreamReader _stream = new StreamReader(Console.OpenStandardInput());
        private int _pos = 0;
        private string[] _line = new string[0];
        public string Next()
        {
            if (_pos >= _line.Length)
            {
                _line = _stream.ReadLine().Split(_separator);
                _pos = 0;
            }
            return _line[_pos++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public string[] Array()
        {
            if (_pos >= _line.Length)
                _line = _stream.ReadLine().Split(_separator);
            _pos = _line.Length;
            return _line;
        }
        public int[] IntArray()
        {
            var arr = Array();
            var res = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                res[i] = int.Parse(arr[i]);
            }
            return res;
        }
        public long[] LongArray()
        {
            var arr = Array();
            var res = new long[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                res[i] = long.Parse(arr[i]);
            }
            return res;
        }
        public double[] DoubleArray()
        {
            var arr = Array();
            var res = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                res[i] = double.Parse(arr[i]);
            }
            return res;
        }
    }
}
