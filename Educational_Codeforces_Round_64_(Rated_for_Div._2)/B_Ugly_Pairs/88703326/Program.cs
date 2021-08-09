using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < N; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        var s = sc.NextCharArray();

        // 隣り合う2種 or 3種 no

        var map = new HashMap<char, int>();
        foreach (char c in s)
        {
            map[c]++;
        }

        var ar = map.ToArray();
        Array.Sort(ar, (l, r) => l.Key.CompareTo(r.Key));
        // 1種そのまま
        if (ar.Length == 1)
        {
            Console.WriteLine(new string(s));
            return;
        }

        var sb = new StringBuilder();
        // 隣り合う2種
        if (ar.Length == 2)
        {
            if (ar[0].Key + 1 == ar[1].Key)
            {
                Console.WriteLine("No answer");
                return;
            }
            else
            {
                Console.WriteLine(new string(s));
                return;
            }
        }

        // 3種
        else if (ar.Length == 3)
        {
            if (ar[0].Key + 1 == ar[1].Key && ar[1].Key + 1 == ar[2].Key)
            {
                Console.WriteLine("No answer");
                return;
            }

            if (ar[0].Key + 1 == ar[1].Key)
            {
                // a b d
                // a d b
                sb.Append(ar[0].Key, ar[0].Value);
                sb.Append(ar[2].Key, ar[2].Value);
                sb.Append(ar[1].Key, ar[1].Value);
            }
            else if (ar[1].Key + 1 == ar[2].Key)
            {
                // a c d
                // c a d
                sb.Append(ar[1].Key, ar[1].Value);
                sb.Append(ar[0].Key, ar[0].Value);
                sb.Append(ar[2].Key, ar[2].Value);
            }
            else
            {
                Console.WriteLine(new string(s));
                return;
            }
        }
        else
        {
            // 4種以上
            // n/2個 1から奇数番目
            // n/2から
            for (int i = 0; i < ar.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sb.Append(ar[i / 2 + ar.Length / 2].Key, ar[i / 2 + ar.Length / 2].Value);
                }
                else
                {
                    sb.Append(ar[i / 2].Key, ar[i / 2].Value);
                }
            }
        }

        Console.WriteLine(sb);
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