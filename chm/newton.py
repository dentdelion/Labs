# Elizabeth Blyumska
# 21.09.2016
# var 21
# x^3 - 2x^2 + x + 1 = 0
# Newton iteration method

def f(x):
	return x**3 - 2*x**2 + x + 1

def f1(x):
	return 3*x**2 - 4*x + 1

def newtons_method(x0, e):  
    while True:
        x1 = x0 - (f(x0) / f1(x0))
        print x1
        if abs(x1 - x0) < e:
            return x1
        x0 = x1



if __name__ == '__main__':
	print newtons_method(-0.2, 0.001)