using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int pos = sc.NextInt();
        int l = sc.NextInt();
        int r = sc.NextInt();

        // nこタブ
        // 現在カーソルpos

        // カーソルと隣に移動させる
        // i+1から右のタブ iまで左のタブを消す

        // [l,r]以外消す最小
        if (l == 1)
        {
            if (r == n)
            {
                Console.WriteLine(0);
            }
            else
            {
                // rまで移動 消す
                Console.WriteLine(Math.Abs(r - pos) + 1);
            }
        }
        else
        {
            if (r == n)
            {
                // lまで移動
                // 消す
                Console.WriteLine(Math.Abs(pos - l) + 1);
            }
            else
            {
                //lまで行く　消す rまで行く　消す
                int ll = Math.Abs(pos - l) + 1 + r - l + 1;

                // rまで , , l
                int rr = Math.Abs(r - pos) + 1 + r - l + 1;
                Console.WriteLine(Math.Min(ll,rr));


            }
        }
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
