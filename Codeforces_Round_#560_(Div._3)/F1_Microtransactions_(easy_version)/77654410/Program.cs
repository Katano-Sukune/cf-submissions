using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] K;
    int[] D, T;
    int[] index;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.IntArray();
        D = new int[M];
        T = new int[M];
        for (int i = 0; i < M; i++)
        {
            D[i] = sc.NextInt();
            T[i] = sc.NextInt() - 1;
        }
        index = new int[M];
        for (int i = 0; i < M; i++)
        {
            index[i] = i;
        }

        Array.Sort(index, (l, r) => D[l].CompareTo(D[r]));

        // n種類ある
        // 毎日1獲得
        // 通常2 セール中は1支払えば買える
        // 種類iを丁度k[i]個買う
        // 毎日0個以上注文できる
        // 種類t[j]はd[j]日にセール

        // 
        int ng = -1;
        int ok = 400000;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (C(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(ok);
    }

    // day日に達成できるか?
    bool C(int day)
    {
        // 区間内セール
        // 種類ごと一番最後
        // 前から

        int[] s = new int[N];
        for (int i = 0; i < N; i++)
        {
            s[i] = int.MaxValue;
        }

        for (int i = 0; i < M; i++)
        {
            if (D[index[i]] <= day)
            {
                s[T[index[i]]] = D[index[i]];
            }
        }

        var tmp = new int[N];
        for (int i = 0; i < N; i++)
        {
            tmp[i] = i;
        }
        Array.Sort(tmp, (l, r) => s[l].CompareTo(s[r]));
        //if (day == 8)
        //{
        //    Console.WriteLine("-----");
        //    Console.WriteLine(day);
        //    Console.WriteLine(string.Join(" ", s));
        //    Console.WriteLine(string.Join(" ", tmp));
        //}
        int nokori = 0;
        int m = 0;
        int p = 0;
        for (int i = 0; i < N; i++)
        {
            if (s[tmp[i]] == int.MaxValue)
            {
                nokori += K[tmp[i]];
            }
            else
            {
                // 買える分
                // s[i] - m 
           
                int tt = Math.Min(K[tmp[i]], s[tmp[i]] - m);
                p += tt;
                m += tt;
                nokori += K[tmp[i]] - tt;
                //if (day == 8)
                //{
                //    Console.WriteLine($"{tmp[i]} {tt} {m}");
                //}
            }
        }
        //if(day == 8)
        //{
        //    Console.WriteLine("aaaa" + m);
        //}
        return nokori * 2 <= day - m;
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
