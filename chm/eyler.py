# Elizabeth Blyumska
# 05.11.16
# Eyler method

import numpy as np
import math
import matplotlib.pyplot as plt

def f(x, y):
	return x*x - 2*y

def f1(x):
	return 0.75*np.exp(-2*x) + 0.5*x*x - 0.5*x + 0.25

x0 = 0
y0 = 1
a = 0 
b = 1
h = 0.1

x = np.arange(a, b, h)
x1 = np.arange(0, 1, 0.01)
y = []
y.append(y0)

for i in range(1, len(x)):
	y.append(y[i-1] + h*f(x[i-1], y[i-1]))

print(x)
print(y)

plt.plot(x, y, 'ro', x1, f1(x1), 'k')
plt.show()
