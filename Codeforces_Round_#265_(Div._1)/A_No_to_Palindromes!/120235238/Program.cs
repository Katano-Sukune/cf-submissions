using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{
    int N, P;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = sc.NextInt();
        S = sc.Next();

        for (int i = N - 1; i >= 0; i--)
        {
            char[] tmp = new char[N];
            for (int j = 0; j < i; j++) tmp[j] = S[j];
            tmp[i] = (char)(S[i] + 1);
            while ((i - 1 >= 0 && tmp[i - 1] == tmp[i]) || (i - 2 >= 0 && tmp[i - 2] == tmp[i])) tmp[i]++;
            if (tmp[i] - 'a' >= P) continue;
            bool flag = true;
            for (int j = i + 1; flag && j < N; j++)
            {
                tmp[j] = 'a';
                while ((j - 1 >= 0 && tmp[j - 1] == tmp[j]) || (j - 2 >= 0 && tmp[j - 2] == tmp[j])) tmp[j]++;
                if (tmp[j] - 'a' >= P) flag = false;
            }
            if (flag)
            {
                Console.WriteLine(new string(tmp));
                return;
            }
        }

        Console.WriteLine("NO");
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
