#include<iostream>

typedef long long ll;

struct P
{
public:
	ll M;
	int Index;
	P(ll m, int i) {
		M = m;
		Index = i;
	}
	P() {
		M = LLONG_MAX;
		Index = -1;
	}
};

constexpr int SIZE = 1 << 19;

P node[SIZE * 2];

P ID = P(LLONG_MAX, -1);

P Merge(P l, P r) {
	return l.M <= r.M ? l : r;
}

void Update(int i, ll m)
{
	int tmp = i + SIZE;
	node[tmp] = P(m, i);
	while (tmp > 1)
	{
		tmp /= 2;
		node[tmp] = Merge(node[tmp * 2], node[tmp * 2 + 1]);
	}
}

P Query(int left, int right, int k, int l, int r)
{
	if (r <= left || right <= l)
	{
		return ID;
	}

	if (left <= l && r <= right)
	{
		return node[k];
	}
	return Merge(Query(left, right, k * 2, l, (l + r) / 2),
		Query(left, right, k * 2 + 1, (l + r) / 2, r));
}

constexpr int MAXN = 500000;

int N;

ll ANS[MAXN];

ll Search(int l, int r)
{
	if (r - l <= 0) return 0;

	P min = Query(l, r, 1, 0, SIZE);
	ll left = min.M * (r - min.Index) + Search(l, min.Index);
	ll right = min.M * (min.Index + 1 - l) + Search(min.Index + 1, r);
	if (left >= right) {
		for (int i = min.Index; i < r; i++) {
			ANS[i] = min.M;
		}
		return left;
	}
	else {
		for (int i = l; i <= min.Index; i++)
		{
			ANS[i] = min.M;
		}
		return right;
	}
}

int main() {
	std::cin >> N;
	for (int i = 0; i < N; i++) {
		ll m;
		std::cin >> m;
		Update(i, m);
	}

	Search(0, N);

	for (int i = 0; i < N - 1; i++) {
		std::cout << ANS[i] << " ";
	}
	std::cout << ANS[N - 1] << std::endl;

	return 0;
}