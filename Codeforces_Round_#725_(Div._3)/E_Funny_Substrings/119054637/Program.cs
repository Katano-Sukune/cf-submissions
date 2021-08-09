using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualBasic;

public class Program
{

    Regex A, B;
    public void Solve()
    {

        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
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
        var map = new Dictionary<string, (long cnt, string f3, string b3)>();
        var map2 = new Dictionary<string, string>();
        const string target = "haha";
        string q = "";
        for (int i = 0; i < n; i++)
        {
            string s = sc.ReadLine();
            string w;
            string e;
            if (T1(s, out q, out w))
            {
                map2[q] = w;
            }
            else if (T2(s, out q, out w, out e))
            {
                string strW;
                string strE;
                if (map2.TryGetValue(w, out strW))
                {
                    if (map2.TryGetValue(e, out strE))
                    {
                        string we = strW + strE;
                        if (we.Length < 6)
                        {
                            map2[q] = we;
                        }
                        else
                        {
                            map2.Remove(q);
                            map[q] = (C(we), we.Substring(0, 3), we.Substring(we.Length - 3, 3));
                        }
                    }
                    else
                    {

                        (long cntE, string fE, string bE) = map[e];
                        map2.Remove(q);
                        string we = strW + fE;
                        map[q] = (cntE + C(we), we.Substring(0, 3), bE);
                    }
                }
                else
                {
                    if (map2.TryGetValue(e, out strE))
                    {
                        (long cntW, string fW, string bW) = map[w];
                        string we = bW + strE;
                        map2.Remove(q);
                        map[q] = (cntW + C(we), fW, we.Substring(we.Length - 3));
                    }
                    else
                    {
                        (long cntW, string fW, string bW) = map[w];
                        (long cntE, string fE, string bE) = map[e];
                        map2.Remove(q);
                        string we = bW + fE;
                        map[q] = (cntW + cntE + C(we), fW, bE);
                    }
                }
            }
        }
        (long cntQ, string fQ, string bQ) o;
        if (map.TryGetValue(q, out o))
        {
            Console.WriteLine(o.cntQ);
        }
        else
        {
            Console.WriteLine(C(map2[q]));
        }
    }

    int C(string s)
    {
        int cnt = 0;
        const string target = "haha";
        for (int i = 0; i + target.Length <= s.Length; i++)
        {
            bool flag = true;
            for (int j = 0; flag && j < target.Length; j++)
            {
                flag &= s[i + j] == target[j];
            }
            if (flag) cnt++;
        }
        return cnt;
    }

    bool T1(string s, out string q, out string w)
    {
        if (!s.Contains(":"))
        {
            q = null;
            w = null;
            return false;
        }
        var sbQ = new StringBuilder();
        int ptr = 0;
        while (s[ptr] != ' ')
        {
            sbQ.Append(s[ptr++]);
        }
        ptr += 4;
        q = sbQ.ToString();
        w = s.Substring(ptr);
        return true;
    }

    bool T2(string s, out string q, out string w, out string e)
    {
        if (!s.Contains('+'))
        {
            q = null;
            w = null;
            e = null;
            return false;
        }

        int ptr = 0;
        q = "";
        while (s[ptr] != ' ') q += s[ptr++];
        ptr += 3;
        w = "";
        while (s[ptr] != ' ') w += s[ptr++];
        ptr += 3;
        e = s.Substring(ptr);
        return true;
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