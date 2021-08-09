using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private string S, T;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();
        T = sc.Next();

        // s_iとt_jを選んで入れ替える
        // s = tにできるか?

        int a = 0;
        int b = 0;
        foreach (char c in S)
        {
            if (c == 'a') a++;
            else b++;
        }

        foreach (char c in T)
        {
            if (c == 'a') a++;
            else b++;
        }

        if (a % 2 == 1)
        {
            Console.WriteLine("-1");
            return;
        }

        // 元々同じ 操作しない

        // a...a
        // b...b
        // 操作して両方消える

        // a...b
        // b...a
        // 2回

        List<int> ab = new List<int>();
        List<int> ba = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (S[i] == 'a' && T[i] == 'b') ab.Add(i);
            else if (S[i] == 'b' && T[i] == 'a') ba.Add(i);
        }

        int k = 0;
        var sb = new StringBuilder();
        for (int i = 0; i + 1 < ab.Count; i += 2)
        {
            k++;
            sb.AppendLine($"{ab[i] + 1} {ab[i + 1] + 1}");
        }


        for (int i = 0; i + 1 < ba.Count; i += 2)
        {
            k++;
            sb.AppendLine($"{ba[i] + 1} {ba[i + 1] + 1}");
        }

        if (ab.Count % 2 == 1 && ba.Count % 2 == 1)
        {
            k++;
            sb.AppendLine($"{ab[ab.Count - 1] + 1} {ab[ab.Count - 1] + 1}");
            k++;
            sb.AppendLine($"{ab[ab.Count - 1] + 1} {ba[ba.Count - 1] + 1}");
        }

        Console.WriteLine(k);
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