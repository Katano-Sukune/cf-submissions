using System;
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
            int M = sc.NextInt();
            if (M % N != 0)
            {
                Console.WriteLine("-1");
                return;
            }

            M /= N;
            int cnt = 0;
            while (M % 2 == 0)
            {
                M /= 2;
                cnt++;
            }

            while (M % 3 == 0)
            {
                M /= 3;
                cnt++;
            }
            Console.WriteLine(M == 1 ? cnt : -1);
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
