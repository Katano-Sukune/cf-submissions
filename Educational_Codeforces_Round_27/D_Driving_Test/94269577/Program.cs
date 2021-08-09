using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        /*
         * 標識
         * ・速度制限
         * ・追い越し可
         * ・速度制限なし
         * ・追い越し禁止
         *
         * n個イベント
         * 1 s 速度をsにする
         * 2 追い越し
         * 3 s 制限速度sの標識
         * 4 追い越し可の標識
         * 5 制限なし
         * 6 追い越し禁止
         *
         * 守ってない標識の個数
         */


        Stack<int> seigen = new Stack<int>();
        Stack<bool> oikoshi = new Stack<bool>();
        seigen.Push(int.MaxValue);
        oikoshi.Push(true);
        int ans = 0;
        int speed = 0;
        for (int i = 0; i < n; i++)
        {
            int t = sc.NextInt();
            switch (t)
            {
                case 1:
                    speed = sc.NextInt();
                    break;
                case 2:
                    while (!oikoshi.Peek())
                    {
                        ans++;
                        oikoshi.Pop();
                    }
                    break;
                case 3:
                    seigen.Push(sc.NextInt());
                    break;
                case 4:
                    oikoshi.Push(true);
                    break;
                case 5:
                    seigen.Push(int.MaxValue);
                    break;
                case 6:
                    oikoshi.Push(false);
                    break;
            }

            while (seigen.Peek() < speed)
            {
                ans++;
                seigen.Pop();
            }
        }

        Console.WriteLine(ans);
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