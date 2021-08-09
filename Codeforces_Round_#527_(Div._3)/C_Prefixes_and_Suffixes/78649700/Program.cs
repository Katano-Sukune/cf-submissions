using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int len = (n - 1) * 2;
        string[] s = new string[len];
        int[] index = new int[len];
        for (int i = 0; i < len; i++)
        {
            s[i] = sc.Next();
            index[i] = i;
        }

        Array.Sort(index, (l, r) => s[r].Length.CompareTo(s[l].Length));

        // 一番長い2つ
        // t,uとする

        {
            string t = s[index[0]];
            string u = s[index[1]];
            var ans = new char[len];
            char[] str = new char[n];
            for (int i = 0; i < t.Length; i++) str[i] = t[i];
            str[n - 1] = u[n - 2];
            bool f = true;
            for (int i = 0; i < t.Length; i++) f &= str[i + 1] == u[i];
            if (f)
            {
                ans[index[0]] = 'P';
                ans[index[1]] = 'S';
                for (int i = 2; i < len; i += 2)
                {
                    bool fs = true;
                    bool ft = true;
                    string ss = s[index[i]];
                    string tt = s[index[i + 1]];
                    for (int j = 0; j < ss.Length; j++)
                    {
                        fs &= ss[j] == str[j] && tt[j] == str[j + i / 2 + 1];
                        ft &= tt[j] == str[j] && ss[j] == str[j + i / 2 + 1];
                    }
                    if (fs)
                    {
                        ans[index[i]] = 'P';
                        ans[index[i + 1]] = 'S';
                    }
                    else if (ft)
                    {

                        ans[index[i]] = 'S';
                        ans[index[i + 1]] = 'P';
                    }
                    else
                    {
                        f = false;
                        break;
                    }
                }
                if (f)
                {
                    Console.WriteLine(new string(ans));
                    return;
                }
            }
        }

        {
            string t = s[index[1]];
            string u = s[index[0]];
            var ans = new char[len];
            char[] str = new char[n];
            for (int i = 0; i < t.Length; i++) str[i] = t[i];
            str[n - 1] = u[n - 2];
            bool f = true;
            for (int i = 0; i < t.Length; i++) f &= str[i + 1] == u[i];
            if (f)
            {
                ans[index[1]] = 'P';
                ans[index[0]] = 'S';
                for (int i = 2; i < len; i += 2)
                {
                    bool fs = true;
                    bool ft = true;
                    string ss = s[index[i]];
                    string tt = s[index[i + 1]];
                    for (int j = 0; j < ss.Length; j++)
                    {
                        fs &= ss[j] == str[j] && tt[j] == str[j + i / 2 + 1];
                        ft &= tt[j] == str[j] && ss[j] == str[j + i / 2 + 1];
                    }
                    if (fs)
                    {
                        ans[index[i]] = 'P';
                        ans[index[i + 1]] = 'S';
                    }
                    else if (ft)
                    {

                        ans[index[i]] = 'S';
                        ans[index[i + 1]] = 'P';
                    }
                    else
                    {
                        f = false;
                        break;
                    }
                }
                if (f)
                {
                    Console.WriteLine(new string(ans));
                    return;
                }
            }
        }
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
