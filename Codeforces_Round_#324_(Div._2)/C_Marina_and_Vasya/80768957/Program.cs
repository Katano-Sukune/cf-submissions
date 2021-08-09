using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int tt = sc.NextInt();
        int t = n - tt;
        string s1 = sc.Next();
        string s2 = sc.Next();

        // 長さ n 違う文字の数 t個のs3を構成

        // s1_i == s2_i 揃える +1, +1　else +0, +0
        // else s1に揃える +1, +0 s2に　+0, +1 

        // s1_i = s2_iな位置
        var l1 = new List<int>();
        // l1以外
        var l2 = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (s1[i] == s2[i]) l1.Add(i);
            else l2.Add(i);
        }
        char[] ans = new char[n];
        if (t <= l1.Count)
        {
            // l1の前t個を合わせる
            // それ以外は違うやつに
            for (int i = 0; i < t; i++)
            {
                ans[l1[i]] = s1[l1[i]];
            }
            for (int i = t; i < l1.Count; i++)
            {
                ans[l1[i]] = s1[l1[i]] == 'a' ? 'b' : 'a';
            }

            foreach (int i in l2)
            {
                if (s1[i] != 'a' && s2[i] != 'a') ans[i] = 'a';
                else if (s1[i] != 'b' && s2[i] != 'b') ans[i] = 'b';
                else ans[i] = 'c';
            }
        }
        else
        {
            int d = t - l1.Count;
            if (2 * d <= l2.Count)
            {
                foreach (int i in l1)
                {
                    ans[i] = s1[i];
                }

                for (int i = 0; i < d; i++)
                {
                    ans[l2[i]] = s1[l2[i]];
                }
                for (int i = d; i < 2 * d; i++)
                {
                    ans[l2[i]] = s2[l2[i]];
                }
                for (int i = 2 * d; i < l2.Count; i++)
                {
                    int j = l2[i];
                    if (s1[j] != 'a' && s2[j] != 'a') ans[j] = 'a';
                    else if (s1[j] != 'b' && s2[j] != 'b') ans[j] = 'b';
                    else ans[j] = 'c';
                }
            }
            else
            {
                Console.WriteLine("-1");
                return;
            }
        }

        Console.WriteLine(new string(ans));
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
