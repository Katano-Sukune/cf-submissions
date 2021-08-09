using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        // iを選ぶ
        // a_iをaのMEXに置き換える a_iも含む
        // 広義単調増加にできるか?
        List<int> ans = new List<int>();
        int[] cnt = new int[n + 2];
        
        bool[] flag = new bool[n];
        for (int i = 0; i < n; i++)
        {
            cnt[a[i]]++;
            if (a[i] == i) flag[i] = true;
        }

        while (true)
        {
            int mex = 0;
            while (cnt[mex] > 0)
            {
                mex++;
            }

            if (mex >= n)
            {
                int j = 0;
                while (j < n && flag[j])
                {
                    j++;
                }

                if (j >= n)
                {
                    break;
                }

                cnt[a[j]]--;
                a[j] = mex;
                cnt[a[j]]++;
                ans.Add(j + 1);
            }
            else
            {
                cnt[a[mex]]--;
                a[mex] = mex;
                cnt[a[mex]]++;
                flag[mex] = true;
                ans.Add(mex + 1);
            }
        }

        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join(" ", ans));
        // Console.WriteLine(string.Join(" ", a));
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