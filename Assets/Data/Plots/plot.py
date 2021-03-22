import matplotlib.pyplot as plt
import numpy as np

p0Values = [5, 10, 30, 60, 75, 80, 95]
cellAmount = 100 * 100

x = range(5000)

# for p0 in p0Values:
#     with open('../pEquals' + str(p0) + '.txt') as f:
#         lines = f.readlines()
#         print(int(lines[4999]))
#         f.close()

for p0 in p0Values:

    f = open('../pEquals' + str(p0) + '.txt')
    y = [int(line) / cellAmount for line in f.readlines()]

    plt.plot(x, y, '+', markersize = 3)
    plt.xlabel("t")
    plt.ylabel(r'Density of alive cells $\left(\dfrac{alive}{all}\right)$')
    plt.title(r'Density of alive cells in time for $p_0 =' + str(p0 / 100) + r'$')
    plt.savefig('Plot' + str(p0) + '.png')
    plt.clf()
    f.close()
