using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    private int N;
    private long A, B;
    private long[] X, VX, VY;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.NextInt();
        B = sc.NextInt();

        X = new long[N];
        VX = new long[N];
        VY = new long[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            VX[i] = sc.NextInt();
            VY[i] = sc.NextInt();
        }

        // 幽霊
        // iは　(VX,VY)で移動
        // T (x, f(x))

        // f(x) = ax+b

        // ぶつかったら怖がる
        // 
        // 怖がらせた総和

        var hm = new HashMap<long, int>();
        var hm2 = new HashMap<(long vx, long vy), int>();
        for (int i = 0; i < N; i++)
        {
            // VY = -A * VX + B'
            hm[VY[i] - A * VX[i]]++;
            hm2[(VX[i], VY[i])]++;
        }

        long ans = 0;
        foreach (var pair in hm)
        {
            ans += (long) pair.Value * (pair.Value - 1);
        }

        
        
        foreach (var pair in hm2)
        {
            ans -= (long) pair.Value * (pair.Value - 1);
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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