using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.DataStructure;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }


        var dx = new[] { 1, 0, -1, 0 };
        var dy = new[] { 0, 1, 0, -1 };

        UnionFind uf = new UnionFind(n * m);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (s[i][j] == '.')
                {
                    int cur = i * m + j;
                    for (int k = 0; k < 4; k++)
                    {
                        int ni = i + dx[k];
                        int nj = j + dy[k];
                        if (ni < 0 || nj < 0) continue;
                        if (ni >= n || nj >= m) continue;
                        if (s[ni][nj] == '*') continue;
                        int to = ni * m + nj;
                        uf.Connect(cur, to);
                    }
                }
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < n; i++)
        {
            char[] ln = new char[m];
            for (int j = 0; j < m; j++)
            {
                if (s[i][j] == '.')
                {
                    ln[j] = '.';
                }
                else
                {
                    var hs = new HashSet<int>(4);
                    int cnt = 1;

                    for (int k = 0; k < 4; k++)
                    {
                        int ni = i + dx[k];
                        int nj = j + dy[k];
                        if (ni < 0 || nj < 0) continue;
                        if (ni >= n || nj >= m) continue;
                        if (s[ni][nj] == '*') continue;
                        int to = ni * m + nj;
                        var f = uf.Find(to);
                        if (hs.Add(f)) cnt += uf.GetSize(f);
                    }

                    ln[j] = (char)((cnt % 10) + '0');
                }


            }

            Console.WriteLine(new string(ln));
        }
        Console.Out.Flush();
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}


namespace CompLib.DataStructure
{
    using System.Collections.Generic;
    using System.Linq;
    class UnionFind
    {
        private readonly int _n;
        private readonly int[] _parent, _size;

        /// <summary>
        /// n??????????????????????????? 1.????????????, 2.2???????????????????????????????????????????????? ???????????????????????????
        /// </summary>
        /// <param name="n">???????????????</param>
        public UnionFind(int n)
        {
            _n = n;
            _parent = new int[_n];
            _size = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        /// <summary>
        /// i?????????????????????????????????
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Find(int i) => _parent[i] == i ? i : Find(_parent[i]);

        /// <summary>
        /// x,y??????????????????????????????????
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Same(int x, int y) => Find(x) == Find(y);

        /// <summary>
        /// (x, y)?????????????????????
        /// </summary>
        /// <remarks>
        /// ACL?????????????????????????????????????????????????????????????????????????????????????????????????
        /// </remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>x,y????????????????????????????????????true?????????</returns>
        public bool Connect(int x, int y)
        {
            x = Find(x);
            y = Find(y);
            if (x == y) return false;

            // ???????????????????????????????????????????????????
            if (_size[x] > _size[y])
            {
                _parent[y] = x;
                _size[x] += _size[y];
            }
            else
            {
                _parent[x] = y;
                _size[y] += _size[x];
            }

            return true;
        }

        /// <summary>
        /// i?????????????????????????????????
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetSize(int i) => _size[Find(i)];

        /// <summary>
        /// ????????????????????????
        /// </summary>
        /// <returns></returns>
        List<int>[] Groups()
        {
            var leaderBuf = new int[_n];
            var groupSize = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                leaderBuf[i] = Find(i);
                groupSize[leaderBuf[i]]++;
            }

            var result = new List<int>[_n];
            for (int i = 0; i < _n; i++)
            {
                result[i] = new List<int>(groupSize[i]);
            }

            for (int i = 0; i < _n; i++)
            {
                result[leaderBuf[i]].Add(i);
            }

            return result.Where(ls => ls.Count > 0).ToArray();
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
