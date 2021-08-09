#pragma GCC target("avx2")
#pragma GCC optimize("O3")
#pragma GCC optimize("unroll-loops")
#include<stdio.h>

constexpr int MAX = 1000000;
constexpr int Count = MAX + 2;

int node[Count];

void Add(int i, int n)
{
	i++;
	for (; i <= Count; i += (i & (-i))) {
		node[i] += n;
	}
}

int Low(int w)
{
	if (w <= 0) return 0;
	int x = 0;
	for (int k = (1 << 19); k > 0; k /= 2) {
		if (x + k < Count && node[x + k] < w)
		{
			w -= node[x + k];
			x += k;
		}
	}
	return x;
}

int main() {
	int n, q;
	scanf("%d %d", &n, &q);
	for (int i = 0; i < n; i++) {
		int a;
		scanf("%d", &a);
		Add(a, 1);
	}
	for (int i = 0; i < q; i++) {
		int k;
		scanf("%d", &k);
		if (k > 0) Add(k, 1);
		else if (k < 0) {
			int index = Low(-k);
			Add(index, -1);
		}
	}
	int ans = Low(1);
	if (ans > MAX) {
		printf("0\n");
	}
	else {
		printf("%d\n", ans);
	}
}