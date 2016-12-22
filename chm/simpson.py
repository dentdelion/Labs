# Elizabeth Blyumska
# 05.11.16
# Simpson method

import numpy as np
from numpy.linalg import inv, matrix_power
import math
import matplotlib.pyplot as plt

def f(x):
	return np.sin(x)

a = 0
b = 10.0
n = 10

h = (b - a) / (2 * n)

x = np.arange(a, b, h)
fx = []
fx_even = []
fx_neven = []
for x0 in x:
	fx.append(f(x0))


for i in range(0, len(fx)):
	if (not i % 2):
		fx_even.append(fx[i])
	else:
		fx_neven.append(fx[i])

print (fx_even)
print (fx_neven)

res = h / 3 * (fx[0] + fx[-1] 
	+ 2 * sum(fx_even) 
	+ 4 * sum(fx_neven))

print(res)
