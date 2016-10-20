# Elizabeth Blyumska
# 09.10.16
# Matrix Jacobi eigen method

import numpy as np
import math
from numpy.linalg import inv, matrix_power

a = np.array([[5, 1, 2], [1, 4, 1], [2, 1, 3]])
k = 0
eps = 0.001
fi = 0
a_list = []
h_list = []
a_list.append(a)

def h (i, j, fi):
	h = np.eye(3)
	h[i, i] = h[j, j] = math.cos(fi)
	h[i, j] = -1 * math.sin(fi)
	h[j, i] = math.sin(fi)

	return h

while (np.amax(np.triu(a_list[-1], 1)) > eps):
	print '\nIteration number', k, ":\n"
	last_a = a_list[-1]
	print (last_a)
	upper = np.triu(last_a, 1)
	(i, j) = np.unravel_index(upper.argmax(), upper.shape)
	print ('i , j ', i, j)
	print last_a[i,j], last_a[i, i], last_a[j,j]
	print 2 * last_a[i, j] / (last_a[i, i] - last_a[j, j])
	fi = 0.5 * math.atan(2 * last_a[i, j] / (last_a[i, i] - last_a[j, j]))
	print ('fi ', fi)

	h1 = h(i, j, fi)
	print ('h', h1)

	h_list.append(h1)
	a_list.append(np.dot(np.dot(np.transpose(h1), last_a), h1))

	k += 1

for i in range(0, len(a_list[-1])):
	print "Vlasni chisla:", a_list[-1][i, i]

eig = h_list[0]
for i in range(1, len(h_list[-1])):
	eig = np.dot (eig, h_list[i])

print "Vlasni vectory:\n", eig











