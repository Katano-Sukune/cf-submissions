using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    private int N;
    private int Q;
    private int[] A;
    private int[] T, L, R, X;

    private const int B = 700;

    public void Solve()
    {
        var sc = new Scanner();
        checked
        {
            N = sc.NextInt();
            Q = sc.NextInt();
            A = sc.IntArray();
            T = new int[Q];
            L = new int[Q];
            R = new int[Q];
            X = new int[Q];
            for (int i = 0; i < Q; i++)
            {
                T[i] = sc.NextInt();
                if (T[i] == 1)
                {
                    L[i] = sc.NextInt() - 1;
                    R[i] = sc.NextInt();
                    X[i] = sc.NextInt();
                }
                else if (T[i] == 2)
                {
                    X[i] = sc.NextInt();
                }
            }

            // 以降のクエリ Y最大
            var max = new int[Q + 1];
            max[Q] = int.MinValue;
            for (int i = Q - 1; i >= 0; i--)
            {
                if (T[i] == 1)
                {
                    max[i] = max[i + 1];
                }
                else if (T[i] == 2)
                {
                    max[i] = Math.Max(X[i], max[i + 1]);
                }
            }

            int block = (N + B - 1) / B;
            long[] tmp = new long[block];
            var cnt = new HashMap<int, int>[block];
            for (int i = 0; i < block; i++)
            {
                cnt[i] = new HashMap<int, int>();
            }

            bool[] flag = new bool[N];

            for (int i = 0; i < N; i++)
            {
                if (A[i] > max[0])
                {
                    flag[i] = true;
                }
                else
                {
                    cnt[i / B][A[i]]++;
                }
            }

#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif
            for (int i = 0; i < Q; i++)
            {
                if (T[i] == 1)
                {
                    int m1 = (L[i] + B - 1) / B;
                    if (R[i] < m1 * B)
                    {
                        for (int j = L[i]; j < R[i]; j++)
                        {
                            if (flag[j]) continue;
                            cnt[j / B][A[j]]--;
                            A[j] += X[i];
                            if (A[j] + tmp[j / B] <= max[i])
                            {
                                cnt[j / B][A[j]]++;
                            }
                            else
                            {
                                flag[j] = true;
                            }
                        }

                        continue;
                    }

                    for (int j = L[i]; j < m1 * B; j++)
                    {
                        if (flag[j]) continue;
                        cnt[j / B][A[j]]--;
                        A[j] += X[i];
                        if (A[j] + tmp[j / B] <= max[i])
                        {
                            cnt[j / B][A[j]]++;
                        }
                        else
                        {
                            flag[j] = true;
                        }
                    }

                    int m2 = R[i] / B;
                    for (int j = m1; j < m2; j++)
                    {
                        tmp[j] += X[i];
                    }

                    for (int j = m2 * B; j < R[i]; j++)
                    {
                        if (flag[j]) continue;
                        cnt[j / B][A[j]]--;
                        A[j] += X[i];
                        if ((long) A[j] + tmp[j / B] <= max[i])
                        {
                            cnt[j / B][A[j]]++;
                        }
                        else
                        {
                            flag[j] = true;
                        }
                    }
                }
                else
                {
                    int left = -1;
                    for (int j = 0; j < block; j++)
                    {
                        if (X[i] - tmp[j] >= 0 && cnt[j][(int) (X[i] - tmp[j])] > 0)
                        {
                            for (int k = 0; k < B; k++)
                            {
                                int idx = j * B + k;
                                if (idx >= N) break;
                                if (flag[idx]) continue;
                                if ((long) A[idx] + tmp[j] == X[i])
                                {
                                    left = idx;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    if (left == -1)
                    {
                        Console.WriteLine("-1");
                        continue;
                    }

                    int right = -1;
                    for (int j = block - 1; j >= 0; j--)
                    {
                        if (X[i] - tmp[j] >= 0 && cnt[j][(int) (X[i] - tmp[j])] > 0)
                        {
                            for (int k = B - 1; k >= 0; k--)
                            {
                                int idx = j * B + k;
                                if (idx >= N) continue;
                                if (flag[idx]) continue;
                                if ((long) A[idx] + tmp[j] == X[i])
                                {
                                    right = idx;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    Console.WriteLine(right - left);
                }
            }

            Console.Out.Flush();
        }
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