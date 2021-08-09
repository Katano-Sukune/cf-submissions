using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;

public class Program
{
    private long N, M, X, Y, VX, VY;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        X = sc.NextInt();
        Y = sc.NextInt();
        VX = sc.NextInt();
        VY = sc.NextInt();

        /*
         * N*M
         *
         * 縁に当たったら反射
         *
         * X,Y開始
         * 当たる隅
         *
         * 
         */

        if (VX == 0)
        {
            if (X != 0 && X != N)
            {
                Console.WriteLine("-1");
            }
            else if (VY > 0)
            {
                Console.WriteLine($"{X} {M}");
            }
            else if (VY < 0)
            {
                Console.WriteLine($"{X} 0");
            }

            return;
        }

        if (VY == 0)
        {
            if (Y != 0 && Y != M)
            {
                Console.WriteLine("-1");
            }
            else if (VX > 0)
            {
                Console.WriteLine($"{N} {Y}");
            }
            else if (VX < 0)
            {
                Console.WriteLine($"0 {Y}");
            }

            return;
        }

        bool xx = false;
        if (VX < 0)
        {
            xx = true;
            VX = -VX;
            X = N - X;
        }

        bool yy = false;
        if (VY < 0)
        {
            yy = true;
            VY = -VY;
            Y = M - Y;
        }

        if ((VX, VY) != (1, 1))
        {
            throw new Exception();
        }

        // Nで割ると N-X余る
        // Mで割ると M-Y余る

        var t = MathACL.CRT(new long[] {N - X, M - Y}, new long[] {N, M});

        if (t == (0, 0))
        {
            Console.WriteLine("-1");
            return;
        }

        long tmp = t.rem;

        long dx = (tmp - (N - X)) / N;
        long dy = (tmp - (M - Y)) / M;

        bool x2 = true;
        bool y2 = true;
        if (dx % 2 == 1)
        {
            x2 ^= true;
        }

        if (dy % 2 == 1)
        {
            y2 ^= true;
        }

        if (xx) x2 ^= true;
        if (yy) y2 ^= true;

        Console.WriteLine($"{(x2 ? N : 0)} {(y2 ? M : 0)}");
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Mathematics
{
    using System.Diagnostics;

    public static partial class MathACL
    {
        /// <summary>
        /// xのn乗をmで割った余り
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long PowMod(long x, long n, int m)
        {
            Debug.Assert(0 <= n && 1 <= m);
            if (m == 1) return 0;
            x = SafeMod(x, m);
            long r = 1;
            while (n != 0)
            {
                if ((n & 1) > 0)
                {
                    r *= x;
                    r %= m;
                }

                x *= x;
                x %= m;
                n >>= 1;
            }

            return r;
        }

        /// <summary>
        /// xの逆数をmで割った余り
        /// </summary>
        /// <param name="x"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long InvMod(long x, long m)
        {
            Debug.Assert(1 <= m);
            var z = InvGCD(x, m);
            Debug.Assert(z.g == 1);
            return z.x;
        }

        /// <summary>
        /// x = r[i] (mod m[i]), ∀i ∈ {0,1,...,n-1} なxを求め、(mod lcm(m))で返します
        /// </summary>
        /// <param name="r"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static (long rem, long mod) CRT(long[] r, long[] m)
        {
            Debug.Assert(r.Length == m.Length);
            int n = r.Length;
            long r0 = 0;
            long m0 = 1;
            for (int i = 0; i < n; i++)
            {
                Debug.Assert(1 <= m[i]);
                long r1 = SafeMod(r[i], m[i]);
                long m1 = m[i];

                if (m0 < m1)
                {
                    long t = r0;
                    r0 = r1;
                    r1 = t;

                    t = m0;
                    m0 = m1;
                    m1 = t;
                }

                if (m0 % m1 == 0)
                {
                    if (r0 % m1 != r1) return (0, 0);
                    continue;
                }


                (long g, long im) = InvGCD(m0, m1);

                long u1 = m1 / g;

                if ((r1 - r0) % g != 0) return (0, 0);

                long x = (r1 - r0) / g % u1 * im % u1;

                r0 += x * m0;
                m0 *= u1;
                if (r0 < 0) r0 += m0;
            }

            return (r0, m0);
        }

        /// <summary>
        /// Σ floor((a*i + b)/m)を返します
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long FloorSum(long n, long m, long a, long b)
        {
            long ans = 0;
            if (a >= m)
            {
                ans += (n - 1) * n * (a / m) / 2;
                a %= m;
            }

            if (b >= m)
            {
                ans += n * (b / m);
                b %= m;
            }

            long yMax = (a * n + b) / m;
            long xMax = (yMax * m - b);
            if (yMax == 0) return ans;
            ans += (n - (xMax + a - 1) / a) * yMax;
            ans += FloorSum(yMax, a, m, (a - xMax % a) % a);
            return ans;
        }

        private static long SafeMod(long n, long m)
        {
            n %= m;
            if (n < 0) n += m;
            return n;
        }

        // g = gcd (a, b)
        // xa = g (mod b)
        private static (long g, long x) InvGCD(long a, long b)
        {
            a = SafeMod(a, b);
            if (a == 0) return (b, 0);
            long s = b;
            long t = a;
            long m0 = 0;
            long m1 = 1;

            while (t > 0)
            {
                long u = s / t;
                s -= t * u;
                m0 -= m1 * u;

                long tmp = s;
                s = t;
                t = tmp;

                tmp = m0;
                m0 = m1;
                m1 = tmp;
            }

            if (m0 < 0) m0 += b / s;
            return (s, m0);
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