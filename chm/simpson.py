# Elizabeth Blyumska
# 05.11.16
# Simpson method

import numpy as np
from numpy.linalg import inv, matrix_power
import math
import matplotlib.pyplot as plt

def f(x):
	return np.exp(-1 * abs(x)) * np.cos(np.pi * x)

a = 0
b = 1.0
n = 1000

h = (b - a) / (n)

def simpson_full(a, b, h):
	x = np.arange(a, b, h)
	fx = []
	fx_even = []
	fx_neven = []
	for x0 in x:
		fx.append(f(x0))

	res = h / 3 * (fx[0] + fx[-1] 
		+ 2 * sum(fx[0::2]) 
		+ 4 * sum(fx[1::2]))

	return res

#1.1
def mid_rect(a, b):
	return f((a + b) / 2) * (b - a)
#1.2
def left_rect(a, b):
	return f(a) * (b - a)

def simpson(a, b):
	return (b - a) / 6 * (f(a) + 4 * f(a + b) / 2 + f(b))

def mid_rect_full(a, b, h):
	x = np.arange(a, b, h)
	fx = []
	for x0 in x:
		fx.append(f(x0))

	res = 0
	for i in range(0, len(fx) - 1):
		res = res + f((x[i] + x[i + 1]) / 2) * (x[i + 1] - x[i])
		#print res
	return res

def left_rect_full(a, b, h):
	x = np.arange(a, b, h)
	fx = []
	for x0 in x:
		fx.append(f(x0))

	res = 0
	for i in range(0, len(fx) - 1):
		res = res + f(x[i]) * (x[i + 1] - x[i])
		#print res
	return res

def gauss(a, b, n):
	ksi = [];
	gamma = [];

	ksi.append(0.0)
	ksi.append(1.0 / 3 * ((5 - 2 * ((10 / 7) ** 0.5)) ** 0.5))
	ksi.append(-1.0 / 3 * ((5 - 2 * ((10 / 7) ** 0.5)) ** 0.5))
	ksi.append(-1.0 / 3 * ((5 + 2 * ((10 / 7) ** 0.5)) ** 0.5))
	ksi.append(1.0 / 3 * ((5 + 2 * ((10 / 7) ** 0.5)) ** 0.5))

	gamma.append(128.0 / 225)
	gamma.append((322 + 13.0 * (70 ** 0.5)) / 900)
	gamma.append((322 + 13.0 * (70 ** 0.5)) / 900)
	gamma.append((322 - 13.0 * (70 ** 0.5)) / 900)
	gamma.append((322 - 13.0 * (70 ** 0.5)) / 900)

	x = list(map(lambda k: 0.5 * (a + b) + 0.5 * (b - a) * k, ksi))
	c = list(map(lambda g: 0.5 * (b - a) * g, gamma))

	# print x
	# print c

	res = 0;

	for i in range(len(x)):
		res += f(x[i]) * c[i]

	return res	
	
def runge(eps, a, b):
	n = 1;
	intn = mid_rect_full(a, b, (b - a) / n)
	int2n = mid_rect_full(a, b, (b - a) / (2 * n))

	while(abs(int2n - intn) / 3.0 > eps):
		n = n + 1
		intn = mid_rect_full(a, b, (b - a) / n)
		#print intn
		int2n = mid_rect_full(a, b, (b - a) / (2 * n))
		#print int2n
		print '{},{}'.format(n, abs(int2n - intn) / 3.0)

	return n

print gauss(a, b, 5)
print simpson_full(a, b, h)
print left_rect_full(a, b, h)
print mid_rect_full(a, b, h)

print runge(0.0001, 0.0, 1.0)