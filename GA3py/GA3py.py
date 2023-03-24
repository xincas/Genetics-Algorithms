
import numpy as np
import random as rd
import matplotlib.pyplot as plt

"" "Вычислить общую длину пути, соответствующего хромосоме" ""
def count_len(chromo, distance, cityNum):
    lenth = 0
    for iii in range(cityNum - 1):
        lenth += distance[chromo[iii]][chromo[iii + 1]]
    return lenth

def count_len_my(chromo, distance, cityNum):
    lenth = 0
    for iii in range(cityNum):
        lenth += distance[iii][chromo[iii]]
    return lenth

def is_chromo_valid(chromo, cityNum):
    p = set()
    way = []
    iii = 0
    for _ in range(cityNum):
        if chromo[iii] not in p: 
            p.add(iii)
            way.append(iii)
        else:
            return False
        iii = chromo[iii]
    return True

def generate_chromo(cityNum):
    way = np.random.permutation(range(1, cityNum)).tolist()
    chromo = np.zeros(cityNum, dtype=int).tolist()
    chromo[0] = way[0]
    for ii in range(len(way) - 1):
        chromo[way[ii]] = way[ii+1]
    return chromo

def to_normal_way(chromo):
    way = [0]
    ii = 0
    for _ in range(len(chromo) - 1):
        way.append(chromo[ii])
        ii = chromo[ii]
    return way

def to_chromo(way):
    chromo = np.zeros(len(way), dtype=int).tolist()
    chromo[0] = way[0]
    for ii in range(len(way) - 1):
        chromo[way[ii]] = way[ii+1]
    return chromo

"" "Создать начальную популяцию" ""
def create_population(cityNum, M):
    popula = []  # Численность популяции
    for ii in range(M):
        chromo = generate_chromo(cityNum)
        #chromo = np.random.permutation(cityNum).tolist()   # Хромосома
        popula.append(chromo)
    return popula

"" "Рассчитайте совокупную вероятность каждого человека в популяции" ""
def count_probabily(popula,distance,cityNum):
    evall = []
    for chromo in popula:
        eval = max(30000 - count_len(to_normal_way(chromo), distance,cityNum),0)  # Фитнес-функция
        evall.append(eval)
    seval = sum(evall)
    probabil = evall/seval   # Единственная вероятность
    probabily = probabil.copy()
    for i in range(1,len(popula)):
        probabily[i]=probabily[i]+probabily[i-1]  # Кумулятивная вероятность
    return probabily

"" "Рулетка отбирает потомство особей" ""
def lpd(popula,probabily,M):
    newpopula=[]
    for i in range(M):
        proba = rd.random()
        for ii in range(len(probabily)):
            if probabily[ii] >= proba:
                selechromo = popula[ii]
                break
        newpopula.append(selechromo)
    return newpopula

"" "Эвристическое пересечение: метод ближайшего соседа" ""
def crossover_nn(father1, father2, cityNum, distance):
    #father_1 = father1.copy()
    father_1 = to_normal_way(father1)
    #father_2 = father2.copy()
    father_2 = to_normal_way(father2)
    city0 = rd.randint(0, cityNum-1)  # Случайно выбрать город в качестве отправной точки
    son = [city0]
    while len(son) < len(father1):
        ord1 = father_1.index(city0)
        ord2 = father_2.index(city0)
        if ord1 == len(father_1)-1 :
            ord1 = -1
        if ord2 == len(father_1)-1 :
            ord2 = -1
        city1 = father_1[ord1 + 1]
        city2 = father_2[ord2 + 1]
        father_1.remove(city0)
        father_2.remove(city0)
        if distance[city0][city1] <= distance[city0][city2]:
            son.append(city1)
            city0 = city1
        else:
            son.append(city2)
            city0 = city2
    return to_chromo(son)

def crossover_nn_my(father1, father2, cityNum, distance):
    father_1 = father1.copy()
    father_2 = father2.copy()
    son = []
    for i in range(len(father1)):
        city1 = father_1[i]
        city2 = father_2[i]
        if distance[i][city1] <= distance[i][city2]:
            son.append(city1)
        else:
            son.append(city2)



    print("crossover return " + str(len(set(son))))
    return son

"" "Эвристическая вариация" ""
import itertools
def variat2(father,cityNum,distance):
    or1 = rd.randint(0, cityNum - 1) # Случайно создать 5 местоположений
    or2 = rd.randint(0, cityNum - 1)
    or3 = rd.randint(0, cityNum - 1)
    or4 = rd.randint(0, cityNum - 1)
    or5 = rd.randint(0, cityNum - 1)
    nosame = list(set([or1, or2, or3, or4, or5]))
    ords = list(itertools.permutations(nosame, len(nosame)))
    sons = []               # Показать всех подчиненных представителей
    #sonn = father.copy()
    sonn = to_normal_way(father)
    ff = sonn.copy()
    for ord in ords:
        for ii in range(len(nosame)):
            #sonn[nosame[ii]] = father[ord[ii]]
            sonn[nosame[ii]] = ff[ord[ii]]
        sons.append(sonn)
    son_leng = []       # Рассчитать расстояние до всех детей
    for sonn in sons:
        leng = count_len(sonn, distance, cityNum)
        son_leng.append(leng)
    n = son_leng.index(min(son_leng))   # Выберите минимальное расстояние
    return sons[n]


def main():
    M = 100  # Численность населения
    cityNum = 52  # Количество городов, длина хромосомы
    "" "Координаты 52 городов" ""
    cities = [[565, 575],[25, 185],[345, 750],[945, 685],[845, 655],[880, 660],[25, 230],[525, 1000],[580, 1175],
              [650, 1130],[1605, 620],[1220, 580],[1465, 200],[1530, 5],[845, 680],[725, 370],[145, 665],[415, 635],
              [510, 875],[560, 365],[300, 465],[520, 585],[480, 415],[835, 625],[975, 580],[1215, 245],[1320, 315],
              [1250, 400],[660, 180],[410, 250],[420, 555],[575, 665],[1150, 1160],[700, 580],[685, 595],[685, 610],
              [770, 610],[795, 645],[720, 635],[760, 650],[475, 960],[95, 260],[875, 920],[700, 500],[555, 815],
              [830, 485],[1170, 65],[830, 610],[605, 625],[595, 360],[1340, 725],[1740, 245]]
    optim = [0, 48, 31, 44, 18, 40, 7, 8, 9, 42, 32, 50, 10, 51, 13, 12, 46, 25, 
            26, 27, 11, 24, 3, 5, 14, 4, 23, 47, 37, 36, 39, 38, 35, 34, 33, 43, 45, 
            15, 28, 49, 19, 22, 29, 1, 6, 41, 20, 16, 2, 17, 30, 21]
    "" "Создать матрицу расстояний" ""
    distance = np.zeros([cityNum,cityNum])
    for i in range(cityNum):
        for j in range(cityNum):
            distance[i][j] = pow((pow(cities[i][0]-cities[j][0],2)+pow(cities[i][1]-cities[j][1],2)),0.5)

    "" "Инициализировать популяцию" ""
    popula = create_population(cityNum, M)

    for n in range(100):  # Циклов
        "" "Эволюция населения" ""
        pc = 0.8  # Скорость кроссовера
        pv = 0.25  # Скорость мутации
        son = []
        ##пересекать
        for c in popula:
            way = to_normal_way(c)
            print(c)
            print(len(set(c)))
            print(way)
            print(len(set(way)))
        print('\n\n\n')

        crossgroup = []
        for i in range(M):
            cpb = rd.random()
            if cpb < pc:  # Меньше кросс-курса для пересечения
                crossgroup.append(popula[i])
        if len(crossgroup) % 2 == 1:  # Если нечетное
            del crossgroup[-1]
        if crossgroup != []:  # Эвристический кроссовер
            for ii in range(0, len(crossgroup), 2):
                sonc = crossover_nn_my(crossgroup[ii], crossgroup[ii + 1], cityNum, distance)
                son.append(sonc)
        ## Мутации
        #variatgroup = []
        #for j in range(M):
        #    vpb = rd.random()
        #    if vpb < pv:  # Мутация меньше скорости мутации
        #        variatgroup.append(popula[j])

        #if variatgroup != []:
        #    for vag in variatgroup:
        #        sonv = variat2(vag, cityNum, distance)  # Эвристическая мутация
        #        son.append(sonv)

        "" "Рассчитайте совокупную вероятность каждой хромосомы" ""
        populapuls = popula + son
        probabily = count_probabily(populapuls, distance, cityNum)

        "" "Рулетка выбирает новый вид" ""
        popula = lpd(populapuls, probabily, M)

    "" "Выберите лучшую хромосому" ""
    opt_chr = popula[0]
    opt_len = count_len(popula[0], distance, cityNum)
    opt_len2 = count_len(optim, distance, cityNum)
    for chr in popula:
        chrlen = count_len(chr, distance, cityNum)
        if chrlen < opt_len:
            opt_chr = chr
            opt_len = chrlen
    print("Оптимальный путь:" + str(opt_chr))
    print("Оптимальное значение:" + str(opt_len))
    print("Оптимальное значение по варианту:" + str(opt_len2))

    
    "" "Нарисуйте карту" ""
    plt.figure()
    for cor in range(len(opt_chr)-1) :
        x = [cities[opt_chr[cor]][0],cities[opt_chr[cor+1]][0]]
        y = [cities[opt_chr[cor]][1],cities[opt_chr[cor+1]][1]]
        plt.plot(x,y,"b-")
    x = [cities[opt_chr[-1]][0],cities[opt_chr[0]][0]]
    y = [cities[opt_chr[-1]][1],cities[opt_chr[0]][1]]
    plt.plot(x,y,"b-")
    plt.figure()
    for cc in range(cityNum - 1):
        x = [cities[optim[cc]][0],cities[optim[cc+1]][0]]
        y = [cities[optim[cc]][1],cities[optim[cc+1]][1]]
        plt.plot(x,y,"r-")
    x = [cities[optim[-1]][0],cities[optim[0]][0]]
    y = [cities[optim[-1]][1],cities[optim[0]][1]]
    plt.plot(x,y,"r-")
    plt.show()

if __name__ =="__main__":
    main()