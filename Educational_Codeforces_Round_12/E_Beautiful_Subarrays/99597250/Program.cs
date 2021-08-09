using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    private int N;
    private int K;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();

        long ans = 0;
        int xor = 0;

        var trie = new Trie();
        trie.Add(0);
        for (int i = 0; i < N; i++)
        {
            xor ^= A[i];

            trie.Xor(xor);
            ans += (i + 1 - trie.LowerBound(K));
            trie.Xor(xor);
            trie.Add(xor);
            // xor ^ 
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Trie
{
    public readonly int Depth;
    private int X;

    private List<int> L, R;
    private List<int> Cnt;

    public Trie(int depth = 31)
    {
        Depth = depth;
        L = new List<int>();
        R = new List<int>();
        Cnt = new List<int>();
        L.Add(-1);
        R.Add(-1);
        Cnt.Add(0);
        X = 0;
    }

    public void Add(int num)
    {
        num ^= X;
        AddR(num, Depth - 1, 0);
    }

    private void AddR(int num, int h, int cur)
    {
        if (h < 0)
        {
            Cnt[cur]++;
            return;
        }

        if ((num & (1 << h)) == 0)
        {
            if (L[cur] == -1)
            {
                L[cur] = L.Count;
                L.Add(-1);
                R.Add(-1);
                Cnt.Add(0);
            }

            AddR(num, h - 1, L[cur]);
        }
        else
        {
            if (R[cur] == -1)
            {
                R[cur] = L.Count;
                L.Add(-1);
                R.Add(-1);
                Cnt.Add(0);
            }

            AddR(num, h - 1, R[cur]);
        }

        Cnt[cur] = (L[cur] == -1 ? 0 : Cnt[L[cur]]) + (R[cur] == -1 ? 0 : Cnt[R[cur]]);
    }

    public void Xor(int num)
    {
        X ^= num;
    }

    public int LowerBound(int num)
    {
        int ans = 0;
        int cur = 0;
        for (int i = Depth - 1; i >= 0; i--)
        {
            if (cur == -1) break;
            if ((num & (1 << i)) == 0)
            {
                if ((X & (1 << i)) == 0)
                {
                    cur = L[cur];
                }
                else
                {
                    cur = R[cur];
                }
            }
            else
            {
                if ((X & (1 << i)) == 0)
                {
                    ans += L[cur] == -1 ? 0 : Cnt[L[cur]];
                    cur = R[cur];
                }
                else
                {
                    ans += R[cur] == -1 ? 0 : Cnt[R[cur]];
                    cur = L[cur];
                }
            }
        }

        return ans;
    }
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