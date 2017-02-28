# Elizabeth Blyumska
# 26 February 2017 (Sunday)
# Linear regression

import numpy as np
import math
import matplotlib.pyplot as plt

def generate_wave_set(n_support=1000, n_train=25, std=0.3):
    x = np.linspace(0, 10, num=n_support)

    y = 2*np.exp(x) + 6
    
    x_train = np.sort(np.random.choice(x, size=n_train, replace=True))

    y_train = 2*np.exp(x_train) + 6 + np.random.normal(0, std, size=x_train.shape[0])
    return (x_train, y_train)

# (x, y) = generate_wave_set();

x = [1, 2, 3, 4, 5, 6]
y = [6, 5, 8, 6, 8, 0]

X = np.array([np.ones(len(x)), x]).T

w = np.dot(np.dot(np.linalg.inv(np.dot(X.T, X)), X.T), y)

y_hat = np.dot(w, X.T)

print y_hat
plt.plot(x, y_hat, 'r', label='fitted')
plt.plot(x, y, 'go', label='data') 
plt.axhline(0, color='black')
plt.axvline(0, color='black')
plt.xlim(-1, 10)
plt.ylim(-1, 10)
plt.title('Fitted linear regression')
plt.legend(loc='upper right', prop={'size': 20})
plt.show()