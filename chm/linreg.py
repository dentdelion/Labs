# Elizabeth Blyumska
# 26 February 2017 (Sunday)
# Linear regression

import numpy as np
import math
import matplotlib.pyplot as plt

x = [1, 2, 3, 4, 5, 6]
y = [6, 5, 8, 6, 8, 0]

X = np.array([np.ones(len(x)), x]).T

w = np.dot(np.dot(np.linalg.inv(np.dot(X.T, X)), X.T), y)

y_hat = np.dot(w, X.T)

print y_hat
plt.plot(x, y_hat, 'r')
plt.plot(x, y, 'ro') 
plt.axhline(0, color='black')
plt.axvline(0, color='black')
plt.xlim(-1, 10)
plt.ylim(-1, 10)
plt.show()