using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Collections;
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
        int[] p = sc.IntArray();

        // 配列a,b

        // m(a, ∅) = a
        // m(∅, b) = b
        // a_1 < b_1なら
        // m(a, b) = a_1 + m(a[2:n], b)
        // else
        // m(a, b) = b_1 + m(a, b[2:m])

        // m(a, b) = pとなる 長さn a,bが存在するか?

        // aの先頭、bの先頭 aに今いくつあるか?

        // aの先頭はp[i] aにj個あるときbの先頭の最大値 t[j]
        var t = new int[n + 1];
        for (int i = 0; i <= n; i++)
        {
            t[i] = -1;
        }

        t[1] = int.MaxValue;

        for (int i = 2 * n - 2; i >= 0; i--)
        {
            var next = new int[n + 1];
            for (int j = 0; j <= n; j++)
            {
                next[j] = -1;
            }

            for (int j = 0; j <= n; j++)
            {
                if (t[j] == -1) continue;

                // aに追加
                if (j + 1 <= n && p[i] < t[j])
                {
                    next[j + 1] = Math.Max(next[j + 1], t[j]);
                }

                // bに追加
                int k = 2 * n - 1 - i - j;
                if (k + 1 <= n && p[i] < p[i + 1])
                {
                    next[k + 1] = Math.Max(next[k + 1], p[i + 1]);
                }
            }

            t = next;
        }

        Console.WriteLine(t.Any(num => num != -1) ? "YES" : "NO");
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections
{
    using System.Collections.Generic;

    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue o;
                return TryGetValue(key, out o) ? o : default(TValue);
            }
            set { base[key] = value; }
        }
    }
}

struct S
{
    public int A, B, CntA, CntB;

    public S(int a, int b, int c, int d)
    {
        if (a > b)
        {
            a ^= b;
            b ^= a;
            a ^= b;

            c ^= d;
            d ^= c;
            c ^= d;
        }

        A = a;
        B = b;
        CntA = c;
        CntB = d;
    }
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