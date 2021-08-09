using System;

public class Program
{

    public void Solve()
    {
        //  [0, 2^14)なxを選ぶ

        // 2回
        // 要素 [0, 2^14)の長さ100の配列 a を聞く
        // 1つ選んで a_i xor xの値が返る
        // 聞く 200個の要素はすべて異なる
        // 2^13

        // 奇数bitを全部立てる 偶数bitは適当
        // return 

        int ans = 0;

        int tmp = 0;
        for (int i = 0; i < 7; i++)
        {
            tmp |= (1 << (i * 2));
        }
        var q = new int[100];
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                q[i] |= (i & (1 << j)) << j;
            }
        }

        int[] q1 = new int[100];
        for (int i = 0; i < 100; i++)
        {
            q1[i] = tmp | (q[i] << 1);
        }

        Console.WriteLine($"? {string.Join(" ", q1)}");
        int ret1 = int.Parse(Console.ReadLine());
        for (int i = 0; i < 7; i++)
        {
            if ((ret1 & (1 << (i * 2))) == 0) ans |= (1 << (i * 2));
        }
        int[] q2 = new int[100];
        for (int i = 0; i < 100; i++)
        {
            q2[i] = (tmp << 1) | q[i];
        }
        Console.WriteLine($"? {string.Join(" ", q2)}");
        int ret2 = int.Parse(Console.ReadLine());
        for (int i = 0; i < 7; i++)
        {
            if ((ret2 & (1 << (i * 2 + 1))) == 0) ans |= (1 << (i * 2 + 1));
        }

        Console.WriteLine($"! {ans}");
    }

    public static void Main(string[] args) => new Program().Solve();
}

