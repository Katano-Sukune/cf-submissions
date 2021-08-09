using System;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;
    private int Q;
    private const int MaxA = 100000;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        Q = sc.NextInt();

        /*
         * +x xの板を受け取る
         * -x xの板を消す
         *
         * 最初A_iの板を持ってる
         *
         * 正方形と長方形が作れるか?
         */

        int[] cnt = new int[MaxA + 1];
        foreach (int i in A)
        {
            cnt[i]++;
        }

        // 4つある板の種類
        int four = 0;
        // 2つある
        int two = 0;

        for (int i = 1; i <= MaxA; i++)
        {
            four += cnt[i] / 4;
            two += cnt[i] / 2;
        }

        var sb = new StringBuilder();
        for (int i = 0; i < Q; i++)
        {
            char t = sc.NextChar();
            int x = sc.NextInt();

            if (t == '+')
            {
                cnt[x]++;
                if (cnt[x] % 2 == 0) two++;
                if (cnt[x] % 4 == 0) four++;
            }
            else if (t == '-')
            {
                cnt[x]--;
                if (cnt[x] % 2 == 1) two--;
                if (cnt[x] % 4 == 3) four--;
            }

            if (four >= 1 && (two - 2) >= 2)
            {
                sb.AppendLine("YES");
            }
            else
            {
                sb.AppendLine("NO");
            }
        }

        Console.Write(sb);
    }

    public static void Main(string[] args) => new Program().Solve();
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