using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Contest
{
    class Program
    {
        public void Solve()
        {
            var sc = new Scanner();
            int N = sc.NextInt();
            string L = sc.Next();
            string R = sc.Next();
            var lMap = new List<int>[256];
            var rMap = new List<int>[256];
            var lCnt = new int[256];
            var rCnt = new int[256];
            lMap['?'] = new List<int>();
            rMap['?'] = new List<int>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                lMap[c] = new List<int>();
                rMap[c] = new List<int>();
            }

            for (int i = 0; i < N; i++)
            {
                lMap[L[i]].Add(i);
                rMap[R[i]].Add(i);
            }

            int cnt = 0;
            var sb = new StringBuilder();

            for (char c = 'a'; c <= 'z'; c++)
            {
                var ll = lMap[c];
                var rr = rMap[c];
                while (true)
                {

                    if (lCnt[c] >= ll.Count || rCnt[c] >= rr.Count)
                    {
                        break;
                    }
                    sb.AppendLine($"{ll[lCnt[c]] + 1} {rr[rCnt[c]] + 1}");
                    lCnt[c]++;
                    rCnt[c]++;
                    cnt++;
                }
            }

            //sb.AppendLine("-------");
            for (char c = 'a'; c <= 'z'; c++)
            {
                var ll = lMap['?'];
                var rr = rMap[c];
                while (true)
                {

                    if (lCnt['?'] >= ll.Count || rCnt[c] >= rr.Count)
                    {
                        break;
                    }
                    sb.AppendLine($"{ll[lCnt['?']] + 1} {rr[rCnt[c]] + 1}");
                    lCnt['?']++;
                    rCnt[c]++;
                    cnt++;
                }
            }
            //sb.AppendLine("-------");
            for (char c = 'a'; c <= 'z'; c++)
            {
                var ll = lMap[c];
                var rr = rMap['?'];
                while (true)
                {

                    if (lCnt[c] >= ll.Count || rCnt['?'] >= rr.Count)
                    {
                        break;
                    }
                    sb.AppendLine($"{ll[lCnt[c]] + 1} {rr[rCnt['?']] + 1}");
                    lCnt[c]++;
                    rCnt['?']++;
                    cnt++;
                }
            }
            //sb.AppendLine("-------");
            {
                var ll = lMap['?'];
                var rr = rMap['?'];
                while (true)
                {

                    if (lCnt['?'] >= ll.Count || rCnt['?'] >= rr.Count)
                    {
                        break;
                    }
                    sb.AppendLine($"{ll[lCnt['?']] + 1} {rr[rCnt['?']] + 1}");
                    lCnt['?']++;
                    rCnt['?']++;
                    cnt++;
                }
            }

            Console.WriteLine(cnt);
            Console.Write(sb.ToString());
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
