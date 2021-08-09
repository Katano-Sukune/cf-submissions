using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


        for (int i = 0; i < t; i++)
        {
            Console.WriteLine(Q(sc.Next(), sc.Next(), sc.Next()));
        }
        Console.Out.Flush();
    }

    string Q(string s, string t, string p)
    {
        int index = 0;
        for (int i = 0; i < t.Length && index < s.Length; i++)
        {
            if (s[index] == t[i])
            {
                index++;
            }
        }
        if (index < s.Length) return "NO";

        int[] sCnt = new int[26];
        int[] tCnt = new int[26];
        int[] pCnt = new int[26];
        foreach (var c in s)
        {
            sCnt[c - 'a']++;
        }
        foreach (var c in t)
        {
            tCnt[c - 'a']++;
        }
        foreach (var c in p)
        {
            pCnt[c - 'a']++;
        }

        for (int i = 0; i < 26; i++)
        {
            if (sCnt[i] + pCnt[i] < tCnt[i]) return "NO";
        }

        return "YES";
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
