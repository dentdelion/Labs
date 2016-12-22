# Elizabeth Blyumska
# 24.10.16
# Lagrange interpolation

import numpy as np
from numpy.linalg import inv, matrix_power
import math
import matplotlib.pyplot as plt

x = np.arange(-3, 3, 0.01)
x1 = np.arange(-3, 3, 0.1)

def f(x):
	return np.sin(x) #x**3 + 2*x*x + 7*x + 1

def lagrange(x, x_vals, y_vals):
	lagrange_pol = 0;

	for i in range(0, len(x_vals)):
		basics_pol = 1

		for j in range(0, len(x_vals)):
			if (j != i):
				basics_pol *= (x - x_vals[j]) / (x_vals[i] - x_vals[j])	

		lagrange_pol += basics_pol * y_vals[i]
	
	return lagrange_pol

x_vals1 = [-3, -1.5, -0.5, 0, 0.5, 1.5, 3]
x_vals = np.array(x_vals1)
y_vals = np.array(map(f, x_vals1))

print ('x_vals', x_vals)
print ('y_vals', y_vals)
y = []

for x0 in x1:
	y.append(lagrange(x0, x_vals, y_vals))
	print (x0, lagrange(x0, x_vals, y_vals))

plt.plot(x1, y, 'ro', x, f(x), 'k')
plt.show()

