#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;
constexpr int MAX_N = 200000;
int N;
int A[MAX_N];
vector<int> Edges[MAX_N];

int Memo[MAX_N];
int Ans[MAX_N];

void DFS1(int n, int p);
void DFS2(int n, int p, int pp);

int main() {
	cin >> N;
	for (int i = 0; i < N; i++) {
		cin >> A[i];
	}
	for (int i = 0; i < N - 1; i++) {
		int a, b;
		cin >> a >> b;
		Edges[a - 1].emplace_back(b - 1);
		Edges[b - 1].emplace_back(a - 1);
	}

	DFS1(0, -1);
	DFS2(0, -1, 0);
	for (int i = 0; i < N - 1; i++) {
		cout << Ans[i] << " ";
	}
	cout << Ans[N - 1] << endl;
}

void DFS1(int n, int p) {
	Memo[n] = A[n] == 1 ? 1 : -1;
	for (int to : Edges[n]) {
		if (to == p) continue;
		DFS1(to, n);
		if (Memo[to] > 0) Memo[n] += Memo[to];
	}
}

void DFS2(int n, int p, int pp) {
	Ans[n] = Memo[n] + max(0, pp);
	for (int to : Edges[n]) {
		if (to == p) continue;
		DFS2(to, n, max(0, pp) + Memo[n] - max(0, Memo[to]));
	}
}