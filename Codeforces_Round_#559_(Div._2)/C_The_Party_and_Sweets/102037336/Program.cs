using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N, M;
    int[] B, G;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        B = sc.IntArray();
        G = sc.IntArray();

        // A_i_j
        // 男iが女jに上げた個数

        // B_i 
        // Aのi行目最小

        // G_j
        // Aのj列目最大

        // Aの総和最小

        // 1 2 1
        // 3 4

        // 1 1
        // 2 2
        // 1 1


        // 各行 b_iで埋める
        long Ans = 0;
        for (int i = 0; i < N; i++)
        {
            Ans += (long)B[i] * M;
        }

        var tmp = B.ToArray();
        Array.Sort(tmp);

        // j列目
        // 最大の行
        // G_jより大きかったら -1
        //  
        bool f = false;
        for (int j = 0; j < M; j++)
        {
            f |= tmp[^1] == G[j];
            if (tmp[^1] > G[j])
            {
                Console.WriteLine("-1");
                return;
            }
            Ans -= tmp[^1];
            Ans += G[j];
        }

        if (!f)
        {
            Ans -= tmp[^2];
            Ans += tmp[^1];
        }

        Console.WriteLine(Ans);

        // B
        // 列
        // G_jより大きいのが1個だけ
        // それで置き換える


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
