using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        string s = sc.Next();

        // s_i = 1 s_{i+1} = 0なら好きな方消せる

        // 操作後 長さ最小のうち、辞書順最小


        char target = s[0];
        int cnt = 1;
        var ls = new List<string>();
        for (int i = 1; i < n; i++)
        {
            if (target != s[i])
            {
                ls.Add(new string(target, cnt));
                target = s[i];
                cnt = 0;
            }

            cnt++;
        }

        ls.Add(new string(target, cnt));

        if (ls.Count == 1)
        {
            Console.WriteLine(ls[0]);
            return;
        }

        // 10がある
        var ans = new StringBuilder();
        int b = 0;
        int e = ls.Count;
        if (ls[0][0] == '0')
        {
            ans.Append(ls[0]);
            b++;
        }
        if (ls[ls.Count - 1][0] == '1')
        {
            e--;
        }
        if (e - b > 0)
        {
            ans.Append('0');
        }

  

        if (ls[ls.Count - 1][0] == '1')
        {
            ans.Append(ls[ls.Count - 1]);
        }
        
        Console.WriteLine(ans);
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

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