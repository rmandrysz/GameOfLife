import numpy as np
from math import sqrt

def standardDeviation(tab, L):
    n = len(tab)

    mySum = 0
    mean = (sum(tab) / n)

    for i in tab:
        mySum += (i - mean)**2
    
    deviation = sqrt(mySum/n)
    error = deviation / sqrt(n)

    return error

boardSizes = [10, 20, 50, 100, 150]
iterations = [100, 100, 30, 30, 10]
t = []

path = "D:/Projects/Unity/Game of Life/Assets/Data/Errors"

for size, i in zip(boardSizes, iterations):
    for j in range(i):
        f = open(path + "/" + str(size) + "/" + str(j) + "pEq30.txt")
        t.append(float(f.readlines()[-1]))
        f.close()

    print("For L = {} standard error equals {}".format(size, standardDeviation(t, size)))
    t.clear()