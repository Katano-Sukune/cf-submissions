#pragma GCC optimize("Ofast")

#include <bits/stdc++.h>

using namespace std;
constexpr int MaxN = 1000000;

int N, K;
vector<int> E[MaxN];
vector<int> A[MaxN];
bitset<MaxN> F;
int Depth[MaxN];

int main(int, char **) {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);
    cin >> N >> K;
    for(int i = 0; i < N - 1; i++) {
        int a, b;
        cin >> a >> b;
        a--;
        b--;
        E[a].push_back(b);
        E[b].push_back(a);
    }

    for(int i = 0; i < N; i++) {
        Depth[i] = -1;
    }

    queue<int> q;
    q.push(N - 1);
    Depth[N - 1] = 0;

    while(!q.empty()) {
        int cur = q.front();
        q.pop();
        for(int to : E[cur]) {
            if(Depth[to] != -1)
                continue;
            q.push(to);
            Depth[to] = Depth[cur] + 1;
            A[to].push_back(cur);
        }

        if(!A[cur].empty()) {
            while(A[cur].size() <= A[A[cur][A[cur].size() - 1]].size()) {
                int c = A[cur].size() - 1;
                A[cur].push_back(A[A[cur][c]][c]);
            }
        }
    }

    int l = N - K;
    l--;
    F[N - 1] = true;
    for(int t = N - 2; t >= 0 && l > 0; t--) {
        int cur = t;
        for(int i = A[t].size() - 1; i >= 0; i--) {
            if(i >= A[cur].size())
                continue;
            if(F[A[cur][i]])
                continue;
            cur = A[cur][i];
        }

        int dist = Depth[t] - Depth[cur] + 1;
        if(dist <= l) {
            int cur2 = t;
            while(!F[cur2]) {
                F[cur2] = true;
                l--;
                cur2 = A[cur2][0];
            }
        }
    }

    vector<int> ans;
    for(int i = 0; i < N; i++) {
        if(!F[i])
            ans.push_back(i + 1);
    }

    for(int i = 0; i < ans.size() - 1; i++) {
        cout << ans[i] << " ";
    }
    cout << ans[ans.size() - 1] << endl;

    return 0;
}
