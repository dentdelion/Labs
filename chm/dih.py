# Elizabeth Blyumska
# 21.09.2016
# var 21
# x^3 - 2x^2 + x + 1 = 0
# Dichotomy bisection method

def f(x):
    return x**3 - 2*x**2 + x + 1

def dih(a, b, e):
    while (b - a > e):
        c = (a + b) / 2
        print c;
        if(f(b) * f(c) < 0):
            a = c
        else:
            b = c

    return (a + b) / 2


if __name__ == '__main__':
    print dih(-0.8, 0, 0.001)