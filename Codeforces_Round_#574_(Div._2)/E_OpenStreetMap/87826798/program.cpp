#include <algorithm>
#include <climits>
#include <iostream>
#include <stack>

typedef long long ll;

constexpr int MAX_L = 3000;

class min_stack {
   public:
    std::stack<std::pair<ll, ll>> s;

    ll min() {
        return s.size() == 0 ? LLONG_MAX : s.top().second;
    }

    void push(ll n) {
        s.push(std::make_pair(n, std::min(n, min())));
    }

    void pop() {
        s.pop();
    }

    ll top() {
        return s.top().first;
    }

    int size() {
        return s.size();
    }
};

class min_queue {
   public:
    min_stack s1, s2;

    ll min() {
        return std::min(s1.min(), s2.min());
    }

    void push(ll n) {
        s2.push(n);
    }

    void exec() {
        if (s1.size() == 0) {
            while (s2.size() > 0) {
                s1.push(s2.top());
                s2.pop();
            }
        }
    }

    ll front() {
        exec();
        return s1.top();
    }

    void pop() {
        exec();
        s1.pop();
    }
};

int N, M, A, B;
int G0, X, Y, Z;

ll H[MAX_L * MAX_L];

int main(int, char **) {
    std::cin >> N >> M >> A >> B;
    std::cin >> G0 >> X >> Y >> Z;
    H[0] = G0;

    for (int i = 1; i < N * M; i++) {
        H[i] = ((H[i - 1] * X + Y) % Z);
    }

    min_queue q[MAX_L];
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < B - 1; j++) {
            q[i].push(H[i * M + j]);
        }
    }

    ll ans = 0;
    for (int j = B - 1; j < M; j++) {
        min_queue q2;
        for (int i = 0; i < N; i++) {
            q[i].push(H[i * M + j]);
            q2.push(q[i].min());
            if (i >= A - 1) {
                ans += q2.min();
                q2.pop();
            }
            q[i].pop();
        }
    }
    std::cout << ans << std::endl;
}
