// package com.company;

import java.util.PriorityQueue;
import java.util.Scanner;

public class Main {
    int N;
    long Z;
    long[] X;

    public void solve() {
        Scanner sc = new Scanner(System.in);
        N = sc.nextInt();
        Z = sc.nextLong();
        // ヒープソート
        var pq = new PriorityQueue<Long>();

        for (int i = 0; i < N; i++) {
            pq.add(sc.nextLong());
        }
        X = new long[N];

        for (int i = 0; i < N; i++) {
            X[i] = pq.poll();
        }

        int ok = 0;
        int ng = N / 2 + 1;
        while (ng - ok > 1) {
            int mid = (ok + ng) / 2;
            if (C(mid)) ok = mid;
            else ng = mid;
        }

        System.out.println(ok);
    }

    boolean C(int k) {
        for (int i = 0; i < k; i++) {
            if (X[N - k + i] - X[i] < Z) return false;
        }

        return true;
    }

    public static void main(String[] args) {
        new Main().solve();
    }
}