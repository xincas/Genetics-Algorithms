import numpy as np
import matplotlib.pyplot as plt
from matplotlib import cm

def f(x, y):
    return 10 * 2 + (x**2 - 10 * np.cos(2 * np.pi * x)) + (y**2 - 10 * np.cos(2 * np.pi * y))

x = np.linspace(-1, 1, 25)
y = np.linspace(-1, 1, 25)

X, Y = np.meshgrid(x, y)
Z = f(X, Y)

xdata = []
ydata = []
zdata = []

with open('D:/repos/GA7/points.txt') as f:
    lines = f.readlines()
    x = []
    y = []
    z = []
    for line in lines:
        if line == "\n":
            xdata.append(x)
            ydata.append(y)
            zdata.append(z)
            x = []
            y = []
            z = []
            continue
        line = line.split('\t')
        point = [i.strip().replace(',', '.') for i in line]
        
        x.append(float(point[0]))
        y.append(float(point[1]))
        z.append(float(point[2]))

fig = plt.figure()
title = ['1st generation', 'last generation']

for i in range(0,2):
    ax = fig.add_subplot(1, 2, i+1, projection='3d')
    ax.set_xlabel('x')
    ax.set_ylabel('y')
    ax.set_zlabel('z')
    ax.plot_surface(X, Y, Z, cmap='viridis', alpha=0.5)
    ax.scatter3D(xdata[i], ydata[i], zdata[i], s=50, c='#ff0000')
    ax.set_title(title[i])
    ax.set_xlim(-1, 1)
    ax.set_ylim(-1, 1)
    ax.set_zlim(-5, 50)

plt.show()
