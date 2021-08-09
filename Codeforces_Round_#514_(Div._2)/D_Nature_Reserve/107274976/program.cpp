#pragma GCC optimize("Ofast")

#include <bits/stdc++.h>

using namespace std;
using ll = long long int;

int n;
int x[100000], y[100000];

bool f(long double r)
{
    long double min_x = -1e20;
    long double max_x = 1e20;

    for (int i = 0; i < n; i++)
    {
        if ((long double)y[i] > 2 * r)
        {
            return false;
        }

        long double dy = (long double)y[i] - r;
        long double dx = sqrt(r * r - dy * dy);
        if (min_x < x[i] - dx)
            min_x = x[i] - dx;
        if (max_x > x[i] + dx)
            max_x = x[i] + dx;
    }
    // cout << r << (min_x <= max_x ? "t" : "f") << endl;

    return min_x <= max_x;
}

int main(int, char **)
{
    ios::sync_with_stdio(false);
    cin.tie(nullptr);
    cin >> n;
    for (int i = 0; i < n; i++)
    {
        cin >> x[i] >> y[i];
    }

    for (int i = 0; i < n; i++)
    {
        if ((y[i] < 0) != (y[0] < 0))
        {
            cout << "-1" << endl;
            return 0;
        }
    }

    if(y[0] < 0){
        for (int i = 0; i < n; i++)
        {
            y[i] = -y[i];
        }
        
    }

    long double ok = 5e13 + 1;
    long double ng = 0;

    for (int i = 0; i < 400; i++)
    {
        long double mid = (ok + ng) / 2;
        if (f(mid))
            ok = mid;
        else
            ng = mid;
        // cout << ok << endl;
    }

    cout << fixed << setprecision(15) << ok << endl;

    return 0;
}
