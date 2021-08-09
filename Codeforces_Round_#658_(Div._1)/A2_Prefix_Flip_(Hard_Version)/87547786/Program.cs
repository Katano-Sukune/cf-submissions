using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        string a = sc.Next();
        string b = sc.Next();

        var ls = new List<int>();

        // 反転してるか
        bool f = false;
        var dq = new Deque<char>();
        for (int i = 0; i < n; i++)
        {
            dq.PushBack(a[i]);
        }

        for (int i = n - 1; i >= 0; i--)
        {
            var first = dq.First();
            var last = dq.Last();
            if (f)
            {
                if (first == b[i])
                {
                    if (last != b[i])
                    {
                        ls.Add(1);
                    }

                    ls.Add(dq.Count);
                    f = false;
                }
            }
            else
            {
                if (last != b[i])
                {
                    if (first == b[i])
                    {
                        ls.Add(1);
                    }

                    ls.Add(dq.Count);
                    f = true;
                }
            }

            if (f)
            {
                dq.PopFront();
            }
            else
            {
                dq.PopBack();
            }
        }

        Console.WriteLine($"{ls.Count} {string.Join(" ", ls)}");
    }

    public static void Main(string[] args) => new Program().Solve();
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Collections
{
    using System;
    using System.Diagnostics;

    #region Deque<T>

    /// <summary>
    /// 指定した同じ型のインスタンスの，先頭または末尾への要素の追加，削除が可能な可変サイズのコレクションを表します．
    /// </summary>
    /// <typeparam name="T"><see cref="Deque{T}"/> 内の要素の型を指定します．</typeparam>
    public class Deque<T>
    {
        int dx;
        T[] buf;
        int mask;

        /// <summary>
        ///<see cref="Deque{T}"/> に格納されている要素の数を取得します．
        /// </summary>
        /// <remarks>この操作は計算量 O(1) で実行されます．</remarks>
        public int Count { get; private set; }

        /// <summary>
        /// 空であって，既定の初期容量を備えた <see cref="Deque{T}"/> クラスの新しいインスタンスを初期化します．
        /// </summary>
        public Deque() : this(8)
        {
        }

        /// <summary>
        /// 空であって，指定の初期容量を備えた <see cref="Deque{T}"/> クラスの新しいインスタンスを初期化します．
        /// </summary>
        /// <param name="capacity">作成したインスタンスが格納できる要素数の初期値．2 べきの値にしてください．</param>
        public Deque(int capacity)
        {
            Debug.Assert(capacity == (capacity & -capacity));
            mask = capacity - 1;
            buf = new T[capacity];
        }

        /// <summary>
        /// 指定したインデックスにある要素を取得または設定します．
        /// </summary>
        /// <param name="index">取得または設定する要素の，0-inexed での番号．</param>
        /// <returns>指定したインデックス位置にある要素</returns>
        /// <remarks>この操作は計算量 O(1) で実行されます．</remarks>
        public T this[int index]
        {
            get
            {
                Debug.Assert(0 <= index && index < Count);
                return buf[(dx + index) & mask];
            }
            set
            {
                Debug.Assert(0 <= index && index < Count);
                buf[(dx + index) & mask] = value;
            }
        }

        /// <summary>
        /// <see cref="Deque{T}"/> の先頭に要素を追加します．
        /// </summary>
        /// <param name="item">追加する要素</param>
        /// <remarks>この操作はならし計算量 O(1) で実行されます．</remarks>
        public void PushFront(T item)
        {
            if (Count == buf.Length) extend();
            dx = (dx + buf.Length - 1) & mask;
            buf[dx] = item;
            Count++;
        }

        /// <summary>
        /// <see cref="Deque{T}"/> の先頭から要素を削除し，返します．
        /// </summary>
        /// <remarks>この操作は計算量 O(1) で実行されます．</remarks>
        public T PopFront()
        {
            Debug.Assert(Count > 0);
            var ret = buf[dx = (dx + 1) & mask];
            Count--;
            return ret;
        }

        /// <summary>
        /// <see cref="Deque{T}"/> の末尾に要素を追加します．
        /// </summary>
        /// <param name="item">追加する要素</param>
        /// <remarks>この操作はならし計算量 O(1) で実行されます．</remarks>
        public void PushBack(T item)
        {
            if (Count == buf.Length) extend();
            buf[(dx + Count++) & mask] = item;
        }

        /// <summary>
        /// <see cref="Deque{T}"/> の末尾から要素を削除し，返します．
        /// </summary>
        /// <remarks>この操作は計算量 O(1) で実行されます．</remarks>
        public T PopBack()
        {
            Debug.Assert(Count > 0);
            var ret = buf[(dx + --Count) & mask];
            return ret;
        }

        /// <summary>
        /// <see cref="Deque{T}"/> の末尾の要素を返します．
        /// </summary>
        /// <remarks>この操作は計算量 O(1) で実行されます．</remarks>
        public T Last()
        {
            Debug.Assert(Count > 0);
            return buf[(dx + Count - 1) & mask];
        }

        /// <summary>
        /// <see cref="Deque{T}"/> の先頭の要素を返します．
        /// </summary>
        /// <remarks>この操作は計算量 O(1) で実行されます．</remarks>
        public T First()
        {
            Debug.Assert(Count > 0);
            return buf[dx];
        }

        void extend()
        {
            var nbuf = new T[buf.Length * 2];
            for (int i = 0; i < buf.Length; i++)
                nbuf[i] = buf[(dx + i) & mask];
            mask = mask * 2 + 1;
            dx = 0;
            buf = nbuf;
        }

        /// <summary>
        /// デックを空にします．
        /// </summary>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public void Clear()
        {
            Count = 0;
        }

        /// <summary>
        /// <see cref="Deque{T}"/> 内に要素が存在するかどうかを調べます．
        /// </summary>
        /// <returns>要素が存在するならば true，そうでなければ false</returns>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public bool Any()
        {
            return Count != 0;
        }

        /// <summary>
        /// 指定した位置に要素を挿入します．
        /// </summary>
        /// <param name="item">追加したい要素</param>
        /// <param name="index">追加したい位置の 0-indexed での番号</param>
        /// <remarks>この操作は最悪計算量 O(N) で実行されます．</remarks>
        public void Insert(int index, T item)
        {
            Debug.Assert(0 <= index && index <= Count);
            PushFront(item);
            for (int i = 0; i < index; i++)
                this[i] = this[i + 1];
            this[index] = item;
        }

        /// <summary>
        /// 指定した位置にある要素を削除し，返します．
        /// </summary>
        /// <param name="index">削除したい位置の 0-indexed での番号</param>
        /// <remarks>この操作は最悪計算量 O(N) で実行されます．</remarks>
        public T RemoveAt(int index)
        {
            Debug.Assert(0 <= index && index < Count);
            var ret = this[index];
            for (int i = index; i > 0; i--)
                this[i] = this[i - 1];
            PopFront();
            return ret;
        }

        /// <summary>
        /// <see cref="Deque{T}"/> 内にある要素の一覧を返します．
        /// </summary>
        /// <returns><see cref="Deque{T}"/> 内にある要素の一覧</returns>
        /// <remarks>この操作は O(N) で実行されます．</remarks>
        public T[] Items
        {
            get
            {
                var ret = new T[Count];
                for (int i = 0; i < Count; i++)
                    ret[i] = this[i];
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