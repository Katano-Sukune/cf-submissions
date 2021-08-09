using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CompLib.Util;

public class Program
{
    int N;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();

        // Sの0,1,2の数を同じになるように変える
        // 変更箇所最小
        // 辞書順最小

        // 0,1,2の数　数える
        int[] cnt = new int[3];
        for (int i = 0; i < N; i++)
        {
            cnt[S[i] - '0']++;
        }
        char[] ans = S.ToCharArray();



        // 確定したとこ
        bool[] flag = new bool[N];
        for (int i = 0; i < 3; i++)
        {
            char c = (char)(i + '0');
            if (cnt[i] > N / 3)
            {
                // iが多い
                int tmp = 0;
                for (int j = 0; j < N && tmp < N / 3; j++)
                {
                    if (ans[j] == c)
                    {
                        flag[j] = true;
                        tmp++;
                    }
                }
            }
            else
            {
                // iが足りない
                for (int j = 0; j < N && cnt[i] < N / 3; j++)
                {
                    if (ans[j] == c)
                    {
                        flag[j] = true;
                    }
                    else if (cnt[ans[j] - '0'] > N / 3 && !flag[j])
                    {
                        flag[j] = true;
                        cnt[ans[j] - '0']--;
                        ans[j] = c;
                        cnt[i]++;
                    }
                }
            }
        }
        Console.WriteLine(new string(ans));
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
