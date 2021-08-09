#include <algorithm>
#include <cstdlib>
#include <iomanip>
#include <iostream>
#include <map>
#include <math.h>
#include <queue>
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <time.h>
#include <vector>

using namespace std;

#define ll long long
#define ld long double

struct T {
    int cost;
    int num;
    bool operator<(const T &right) const {
        return cost > right.cost;
    }
};

int main() {
    int N;
    cin >> N;
    vector<T> P(N);
    for (int i = 0; i < N; i++) {
        cin >> P[i].cost;
        P[i].num = i;
    }
    vector<priority_queue<T>> pq(3);
    for (int i = 0; i < N; i++) {
        int a;
        cin >> a;
        a--;
        pq[a].push(P[i]);
    }
    for (int i = 0; i < N; i++) {
        int b;
        cin >> b;
        b--;
        pq[b].push(P[i]);
    }

    map<T, bool> mp;

    int m;
    cin >> m;
    vector<int> ans(m);
    for (auto i : P) {
        mp[i] = false;
    }

    for (int i = 0; i < m; i++) {
        int col;
        cin >> col;
        col--;
        if (pq[col].empty()) {
            ans[i] = -1;
            continue;
        }
        T t;
        bool fag = false;
        while (1) {
            if (pq[col].empty()) {
                fag = true;
                break;
            }
            t = pq[col].top();
            pq[col].pop();
            if (!mp[t]) {
                break;
            }
        }
        if (fag) {
            ans[i] = -1;
            continue;
        }
        mp[t] = true;
        ans[i] = t.cost;
    }
    for (int i = 0; i < m - 1; i++) {
        cout << ans[i] << " ";
    }
    cout << ans[m - 1] << endl;
    return 0;
}
