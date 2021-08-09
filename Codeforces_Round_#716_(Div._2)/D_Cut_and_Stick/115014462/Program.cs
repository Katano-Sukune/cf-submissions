using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Collections;

public class Program
{
    int N, Q;
    int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        A = sc.IntArray();

        var wm = new WaveletMatrix(A);

#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = false});
#endif
        for (int i = 0; i < Q; i++)
        {
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();

            int len = r - l;
            int t = len / 2 + 1;

            var ls = wm.Filter(l, r, t);
            if (ls.Count == 0)
            {
                Console.WriteLine("1");
            }
            else
            {
                int cnt = ls[0].Freq;
                int other = len - cnt;
                Console.WriteLine(cnt - other);
            }
        }

        System.Console.Out.Flush();

    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}
namespace CompLib.Collections
{
    using System.Collections.Generic;
    using System;
    using System.Diagnostics;
    using Num = System.Int32;

    public class WaveletMatrix
    {
        public readonly int Count;

        // i桁目のbit
        private readonly SuccinctBitArray[] BitArrays;
        // i桁目 0の個数
        private readonly int[] Cnt0;
        // 各数 開始位置
        private readonly StaticMap Map;

        // 2進 桁数
        private readonly int D;
        public WaveletMatrix(Num[] t)
        {
            Count = t.Length;

            int max = 0;
            foreach (int i in t)
            {
                Debug.Assert(i >= 0);
                max = Math.Max(max, i);
            }
            D = GetDigit(max);

            Cnt0 = new int[D];
            BitArrays = new SuccinctBitArray[D];
            for (int i = D - 1; i >= 0; i--)
            {
                BitArrays[i] = new SuccinctBitArray(Count);
                Num d = (Num)1 << i;
                Num[] next = new Num[Count];
                int ptr = 0;

                // 0のやつ見る
                for (int j = 0; j < Count; j++)
                {
                    if ((t[j] & d) == 0)
                    {
                        Cnt0[i]++;
                        next[ptr++] = t[j];
                    }
                }
                // 1のやつ見る
                for (int j = 0; j < Count; j++)
                {
                    if ((t[j] & d) != 0)
                    {
                        next[ptr++] = t[j];
                        BitArrays[i][j] = true;
                    }
                }
                BitArrays[i].Build();
                t = next;
            }

            Map = new StaticMap();
            for (int i = 0; i < Count; i++)
            {
                if (i == 0 || t[i - 1] != t[i])
                {
                    Map.Add(t[i], i);
                }
            }
            Map.Build();
        }

        public Num Get(int i)
        {
            Num result = 0;
            for (int j = D - 1; j >= 0; j--)
            {
                if (BitArrays[j][i])
                {
                    result |= (Num)1 << j;
                    // 立ってる
                    // iは0の個数+iまでの1の個数に飛ぶ
                    i = Cnt0[j] + BitArrays[j].Sum(i);
                }
                else
                {
                    // 立ってない
                    // iはiまでの0の個数に飛ぶ
                    i = i - BitArrays[j].Sum(i);
                }
            }
            return result;
        }

        /// <summary>
        /// [0,r)にnがいくつあるか?
        /// </summary>
        /// <param name="r"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Rank(int r, Num n)
        {
            if (!Map.ContainsKey(n)) return 0;
            for (int i = D - 1; i >= 0; i--)
            {
                Num b = (Num)1 << i;
                if ((n & b) != 0)
                {
                    // 立ってる
                    // rは0の個数+rまでの1の個数に飛ぶ
                    r = Cnt0[i] + BitArrays[i].Rank(r, true);
                }
                else
                {
                    // 立ってない
                    // rはrまでの0の個数に飛ぶ
                    r = BitArrays[i].Rank(r, false);
                }
            }

            return r - Map[n];
        }

        /// <summary>
        /// i番目(0-origin)のnの位置
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Select(int i, Num n)
        {
            int begin;
            {
                int low = Map.LowerBound(n);
                if (Map.At(low).Key != n) throw new ArgumentOutOfRangeException();
                begin = Map.At(low).Value;
                if (i < 0) throw new ArgumentOutOfRangeException();
            }

            int idx = begin + i;
            for (int j = 0; j < D; j++)
            {
                bool b = (n & (Num)1 << j) != 0;
                if (b)
                {
                    // 立ってる
                    // idxは 0の個数+idxまでの1の個数から飛んできた
                    idx -= Cnt0[j];
                }
                // 立ってない
                // idxはidxまでの0の個数から飛んできた
                idx = BitArrays[j].Select(idx - 1, b) + 1;
            }
            return idx;
        }

        /// <summary>
        /// sorted(t[begin..end])[i]
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public Num Quantile(int begin, int end, int i)
        {
            if (i >= end - begin) throw new ArgumentOutOfRangeException();
            Num result = 0;
            for (int j = D - 1; j >= 0; j--)
            {
                // 範囲内の0の個数
                int c0 = BitArrays[j].Rank(end, false) - BitArrays[j].Rank(begin, false);
                if (i < c0)
                {
                    // j桁目は0
                    begin = BitArrays[j].Rank(begin, false);
                    end = BitArrays[j].Rank(end, false);
                }
                else
                {
                    // j桁目は1
                    result |= (Num)1 << j;
                    // 下位c0個飛ばす
                    i -= c0;
                    // beginはcnt0 + rank(begin,1)に飛ぶ
                    begin = Cnt0[j] + BitArrays[j].Rank(begin, true);
                    // endはcnt0 + rank(end,1)に飛ぶ
                    end = Cnt0[j] + BitArrays[j].Rank(end, true);
                }
            }
            return result;
        }

        /// <summary>
        /// [begin,end)の出現回数上位k個
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public PairNumFreq[] TopK(int begin, int end, int k)
        {
            var pq = new PriorityQueue<Segment>((l, r) => (r.End - r.Begin).CompareTo(l.End - l.Begin));
            pq.Enqueue(new Segment(begin, end, D - 1, 0));
            PairNumFreq[] result = new PairNumFreq[k];
            int ptr = 0;

            while (ptr < k)
            {
                var dq = pq.Dequeue();
                int b = dq.Begin;
                int e = dq.End;
                int d = dq.Digit;
                Num n = dq.N;
                if (d < 0)
                {
                    result[ptr++] = new PairNumFreq(n, e - b);
                    continue;
                }

                // 区間内の0の個数

                int l0 = BitArrays[d].Rank(b, false);
                int r0 = BitArrays[d].Rank(e, false);
                if (l0 < r0)
                {
                    // 0
                    pq.Enqueue(new Segment(l0, r0, d - 1, n));
                }

                // 1
                int l1 = Cnt0[d] + BitArrays[d].Rank(b, true);
                int r1 = Cnt0[d] + BitArrays[d].Rank(e, true);
                if (l1 < r1)
                {
                    pq.Enqueue(new Segment(l1, r1, d - 1, n | ((Num)1 << d)));
                }
            }

            return result;
        }

        /// <summary>
        /// [begin,end)の最頻値、回数
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public PairNumFreq Mode(int begin, int end)
        {
            var pq = new PriorityQueue<Segment>((l, r) => (r.End - r.Begin).CompareTo(l.End - l.Begin));
            pq.Enqueue(new Segment(begin, end, D - 1, 0));

            while (true)
            {
                var dq = pq.Dequeue();
                int b = dq.Begin;
                int e = dq.End;
                int d = dq.Digit;
                Num n = dq.N;
                if (d < 0)
                {
                    return new PairNumFreq(n, e - b);
                }

                // 区間内の0の個数

                int l0 = BitArrays[d].Rank(b, false);
                int r0 = BitArrays[d].Rank(e, false);
                if (l0 < r0)
                {
                    // 0
                    pq.Enqueue(new Segment(l0, r0, d - 1, n));
                }

                // 1
                int l1 = Cnt0[d] + BitArrays[d].Rank(b, true);
                int r1 = Cnt0[d] + BitArrays[d].Rank(e, true);
                if (l1 < r1)
                {
                    pq.Enqueue(new Segment(l1, r1, d - 1, n | ((Num)1 << d)));
                }
            }
        }


        /// <summary>
        /// [l,r)でk個以上含まれる要素
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public List<PairNumFreq> Filter(int l, int r, int k)
        {
            var q = new Queue<Segment>();
            q.Enqueue(new Segment(l, r, D - 1, 0));
            List<PairNumFreq> result = new List<PairNumFreq>();
            while (q.Count > 0)
            {
                var d = q.Dequeue();
                if (d.Digit < 0)
                {
                    result.Add(new PairNumFreq(d.N, d.End - d.Begin));
                    continue;
                }

                int l0 = BitArrays[d.Digit].Rank(d.Begin, false);
                int r0 = BitArrays[d.Digit].Rank(d.End, false);
                if (r0 - l0 >= k)
                {
                    // 0
                    q.Enqueue(new Segment(l0, r0, d.Digit - 1, d.N));
                }

                // 1
                int l1 = Cnt0[d.Digit] + BitArrays[d.Digit].Rank(d.Begin, true);
                int r1 = Cnt0[d.Digit] + BitArrays[d.Digit].Rank(d.End, true);
                if (r1 - l1 >= k)
                {
                    q.Enqueue(new Segment(l1, r1, d.Digit - 1, d.N | ((Num)1 << d.Digit)));
                }
            }
            return result;
        }

        public Num this[int i]
        { get { return Get(i); } }

        private int GetDigit(Num n)
        {
            Debug.Assert(n >= 0);
            if (n == 0) return 1;
            int r = 0;
            while (n > 0)
            {
                r++;
                n >>= 1;
            }
            return r;
        }
    }

    public class SuccinctBitArray
    {
        public int Length { get; private set; }
        private readonly ulong[] T;
        // 256個でリセット 累積和
        private readonly byte[] B1;
        // 65536/256個でリセット 256個ごとの累積和
        private readonly ushort[] B2;
        // 65535個ごとの累積和
        private int[] B3;

        private const int ULSize = sizeof(ulong);
        private const int B1Size = 256;
        private const int B2Size = 65536;
        private const int B1ToB2 = B2Size / B1Size;
        public SuccinctBitArray(int size)
        {
            Debug.Assert(Length >= 0);
            Length = size;
            T = new ulong[(Length + ULSize - 1) / ULSize];
            B1 = new byte[Length + 1];
            B2 = new ushort[Length / B1Size + 1];
            B3 = new int[Length / B2Size + 1];
        }

        /// <summary>
        /// [0,r)の和
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int Sum(int r)
        {
            Debug.Assert(0 <= r && r <= Length);
            return B1[r] + B2[r / B1Size] + B3[r / B2Size];
        }

        /// <summary>
        /// [0,r)にfがいくつあるか?
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int Rank(int r, bool f)
        {
            return f ? Sum(r) : r - (Sum(r));
        }

        // i番目(0-origin)のfの位置
        public int Select(int i, bool f)
        {
            i++;
            // b3を検索
            int ok3 = 0;
            int ng3 = B3.Length;
            while (ng3 - ok3 > 1)
            {
                int mid = (ok3 + ng3) / 2;
                if (f)
                {
                    if (B3[mid] <= i) ok3 = mid;
                    else ng3 = mid;
                }
                else
                {
                    if (B2Size * mid - B3[mid] <= i) ok3 = mid;
                    else ng3 = mid;
                }
            }

            // b2
            int ok2 = ok3 * B1ToB2;
            int ng2 = Math.Min(ng3 * B1ToB2, B2.Length);
            while (ng2 - ok2 > 1)
            {
                int mid = (ok2 + ng2) / 2;
                if (f)
                {
                    if (B3[ok3] + B2[mid] <= i) ok2 = mid;
                    else ng2 = mid;
                }
                else
                {
                    if (mid * B1Size - B3[ok3] - B2[mid] <= i) ok2 = mid;
                    else ng2 = mid;
                }
            }

            // b1;
            int ok1 = ok2 * B1Size;
            int ng1 = Math.Min(ng2 * B1Size, B1.Length);
            while (ng1 - ok1 > 1)
            {
                int mid = (ok1 + ng1) / 2;
                if (f)
                {
                    if (B3[ok3] + B2[ok2] + B1[mid] <= i) ok1 = mid;
                    else ng1 = mid;
                }
                else
                {
                    if (mid - B3[ok3] - B2[ok2] - B1[mid] <= i) ok1 = mid;
                    else ng1 = mid;
                }
            }
            return ok1 - 1;
        }

        public void Build()
        {
            for (int i = 0; i < Length; i++)
            {
                if ((i + 1) % B1Size != 0)
                {
                    B1[i + 1] = B1[i];
                    if (Get(i)) B1[i + 1]++;
                }
            }
            for (int i = 0; i < Length / B1Size; i++)
            {
                if ((i + 1) % (B1ToB2) != 0)
                {
                    B2[i + 1] = B2[i];
                    for (int j = 0; j < B1Size; j++)
                    {
                        if (Get(i * B1Size + j)) B2[i + 1]++;
                    }
                }
            }
            for (int i = 0; i < Length / B2Size; i++)
            {
                B3[i + 1] = B3[i];
                for (int j = 0; j < B2Size; j++)
                {
                    if (Get(i * B2Size + j)) B3[i + 1]++;
                }
            }
        }

        public void Set(int i, bool f)
        {
            Debug.Assert(0 <= i && i < Length);
            if (f) T[i / ULSize] |= 1UL << (i % ULSize);
            else T[i / ULSize] &= ~(1UL << (i % ULSize));
        }

        public bool Get(int i)
        {
            Debug.Assert(0 <= i && i < Length);
            return (T[i / ULSize] & (1UL << (i % ULSize))) != 0;
        }

        public bool this[int i]
        {
            get { return Get(i); }
            set { Set(i, value); }
        }
    }

    // 変更不可 連想配列
    class StaticMap
    {
        public readonly List<KeyValuePair<Num, int>> L;
        public StaticMap()
        {
            L = new List<KeyValuePair<Num, int>>();
        }
        public void Add(Num key, int value)
        {
            L.Add(new KeyValuePair<Num, int>(key, value));
        }

        public KeyValuePair<Num, int> At(int i)
        {
            return L[i];
        }

        public int Get(Num key)
        {
            // key以下
            int ok = 0;
            int ng = L.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (L[mid].Key <= key) ok = mid;
                else ng = mid;
            }
            if (L[ok].Key != key) throw new KeyNotFoundException();
            return L[ok].Value;
        }

        public int LowerBound(Num n)
        {
            int ok = L.Count;
            int ng = -1;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (L[mid].Key >= n) ok = mid;
                else ng = mid;
            }
            return ok;
        }

        public bool ContainsKey(Num k)
        {
            return At(LowerBound(k)).Key == k;
        }

        public int UpperBound(Num n)
        {
            int ok = L.Count;
            int ng = -1;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (L[mid].Key > n) ok = mid;
                else ng = mid;
            }
            return ok;
        }

        public int this[Num key] { get { return Get(key); } }

        public void Build()
        {
            L.Sort((l, r) => l.Key.CompareTo(r.Key));
        }
    }

    // 値、頻度(frequency)
    public struct PairNumFreq
    {
        public Num N;
        public int Freq;
        public PairNumFreq(Num n, int f)
        {
            N = n;
            Freq = f;
        }

        public override string ToString()
        {
            return $"N={N}, Freq={Freq}";
        }
    }

    public struct PairNumFreq2
    {
        public Num N;
        public int Freq1, Freq2;
        public PairNumFreq2(Num n, int f1, int f2)
        {
            N = n;
            Freq1 = f1;
            Freq2 = f2;
        }

        //public override string ToString()
        //{
        //    return $"N={N}, Freq={Freq}";
        //}
    }

    // 範囲, bit
    struct Segment
    {
        public int Begin, End, Digit;
        public Num N;
        public Segment(int b, int e, int r, Num n)
        {
            Begin = b;
            End = e;
            Digit = r;
            N = n;
        }
    }

    struct Segment2
    {
        public int Begin1, End1, Begin2, End2;
        public Num N;
        public Segment2(int b1, int e1, int b2, int e2, Num n)
        {
            Begin1 = b1;
            End1 = e1;
            Begin2 = b2;
            End2 = e2;
            N = n;
        }
    }
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Collections
{
    using System;
    using System.Collections.Generic;

    #region PriorityQueue

    /// <summary>
    /// 指定した型のインスタンスを最も価値が低い順に取り出すことが可能な可変サイズのコレクションを表します．
    /// </summary>
    /// <typeparam name="T">優先度付きキュー内の要素の型を指定します．</typeparam>
    /// <remarks>内部的にはバイナリヒープによって実装されています．</remarks>
    public class PriorityQueue<T>
    {
        readonly List<T> heap = new List<T>();
        readonly Comparison<T> cmp;

        /// <summary>
        /// デフォルトの比較子を使用してインスタンスを初期化します．
        /// </summary>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public PriorityQueue()
        {
            cmp = Comparer<T>.Default.Compare;
        }

        /// <summary>
        /// デリゲートで表されるような比較関数を使用してインスタンスを初期化します．
        /// </summary>
        /// <param name="comparison"></param>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public PriorityQueue(Comparison<T> comparison)
        {
            cmp = comparison;
        }

        /// <summary>
        /// 指定された比較子を使用してインスタンスを初期化します．
        /// </summary>
        /// <param name="comparer"></param>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public PriorityQueue(IComparer<T> comparer)
        {
            cmp = comparer.Compare;
        }

        /// <summary>
        /// 優先度付きキューに要素を追加します．
        /// </summary>
        /// <param name="item">優先度付きキューに追加される要素</param>
        /// <remarks>最悪計算量 O(log N) で実行されます．</remarks>
        public void Enqueue(T item)
        {
            var pos = heap.Count;
            heap.Add(item);
            while (pos > 0)
            {
                var par = (pos - 1) / 2;
                if (cmp(heap[par], item) <= 0)
                    break;
                heap[pos] = heap[par];
                pos = par;
            }

            heap[pos] = item;
        }

        /// <summary>
        /// 優先度付きキューから最も価値が低い要素を削除し，返します．
        /// </summary>
        /// <returns>優先度付きキューから削除された要素．</returns>
        /// <remarks>最悪計算量 O(log N) で実行されます．</remarks>
        public T Dequeue()
        {
            var ret = heap[0];
            var pos = 0;
            var x = heap[heap.Count - 1];

            while (pos * 2 + 1 < heap.Count - 1)
            {
                var lch = pos * 2 + 1;
                var rch = pos * 2 + 2;
                if (rch < heap.Count - 1 && cmp(heap[rch], heap[lch]) < 0) lch = rch;
                if (cmp(heap[lch], x) >= 0)
                    break;
                heap[pos] = heap[lch];
                pos = lch;
            }

            heap[pos] = x;
            heap.RemoveAt(heap.Count - 1);
            return ret;
        }

        /// <summary>
        ///  優先度付きキューに含まれる最も価値が低い要素を削除せずに返します．
        /// </summary>
        /// <returns>優先度付きキューに含まれる最も価値が低い要素．</returns>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public T Peek()
        {
            return heap[0];
        }

        /// <summary>
        /// 優先度付きキュー内の要素の数を取得します．
        /// </summary>
        /// <returns>優先度付キュー内にある要素の数</returns>
        /// <remarks>最悪計算量 O(1) で実行されます．</remarks>
        public int Count
        {
            get { return heap.Count; }
        }

        /// <summary>
        /// 優先度付きキュー内に要素が存在するかどうかを O(1) で判定します．
        /// </summary>
        /// <returns>優先度付キュー内にある要素が存在するならば true，そうでなければ　false．</returns>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public bool Any()
        {
            return heap.Count > 0;
        }

        /// <summary>
        /// 優先度付きキューに含まれる要素を昇順に並べて返します．
        /// </summary>
        /// <remarks>この操作は計算量 O(N log N)で実行されます．</remarks>
        public T[] Items
        {
            get
            {
                var ret = heap.ToArray();
                Array.Sort(ret, cmp);
                return ret;
            }
        }
    }

    #endregion
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
