using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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
        int k = sc.NextInt();
        string s = sc.Next();

        // 文字列中各文字の出現回数がkで割り切れる
        // s以上最小

        if (n == 1)
        {
            if (k == 1)
            {
                Console.WriteLine(s);
                return;
            }
        }

        if (n % k != 0)
        {
            Console.WriteLine("-1");
            return;
        }

        int[] cnt = new int[26];
        foreach (var c in s)
        {
            cnt[c - 'a']++;
        }

        var ans = s.ToCharArray();

        // i桁
        for (int i = n; i >= 0; i--)
        {
            // 残り
            int sum = 0;
            int[] tmp = new int[26];
            for (int j = 0; j < 26; j++)
            {
                tmp[j] = (k - cnt[j] % k) % k;
                sum += tmp[j];
            }

            //
            // Console.WriteLine(string.Join(" ", cnt));
            //
            // Console.WriteLine(string.Join(" ", tmp));
            int d = n - i;
            if (d >= sum)
            {
                // Console.WriteLine($"{i}");
                // 残り
                int w = d - sum;
                bool flag = true;
                // i桁目
                // s[i]より大きい

                if (i < n)
                {
                    if (s[i] == 'z')
                    {
                        flag = false;
                    }
                    else if (tmp[s[i] - 'a' + 1] > 0)
                    {
                        ans[i] = (char) (s[i] + 1);
                        tmp[s[i] - 'a' + 1]--;
                    }
                    else if (w >= k)
                    {
                        ans[i] = (char) (s[i] + 1);
                        w -= k;
                        tmp[s[i] + 1 - 'a'] += k - 1;
                    }
                    else
                    {
                        flag = false;
                        for (char j = (char) (s[i] + 1); j <= 'z'; j++)
                        {
                            if (tmp[j - 'a'] > 0)
                            {
                                ans[i] = j;
                                tmp[j - 'a']--;
                                flag = true;
                                break;
                            }
                        }
                    }
                }

                if (flag)
                {
                    int p = 0;
                    for (int j = i + 1; j < n; j++)
                    {
                        if (w > 0)
                        {
                            ans[j] = 'a';
                            w--;
                        }
                        else
                        {
                            while (tmp[p] == 0) p++;
                            ans[j] = (char) (p + 'a');
                            tmp[p]--;
                        }
                    }

                    // Console.WriteLine($"{i}");
                    Console.WriteLine(new string(ans));
                    return;
                }
            }

            if (i >= 1) cnt[s[i - 1] - 'a']--;
        }


        Console.WriteLine("-1");
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