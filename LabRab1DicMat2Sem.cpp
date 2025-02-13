#include <iostream>
#include <string>
#include <cstdlib>
#include <iomanip> // Для форматирования вывода

void deleteMat(int** graf, int size) {
    for (int i = 0; i < size; i++) {
        delete[] graf[i];
    }
    delete[] graf;
}

void drawMat(int** graf, int size) {
    char symbol;
    std::cout << "  ";
    for (int i = 0; i < size; i++) {
        symbol = ('a' + i);
        std::cout << symbol << ' ';
    }
    std::cout << '\n';
    for (int i = 0; i < size; i++) {
        symbol = ('a' + i);
        std::cout << symbol << ' ';
        for (int j = 0; j < size; j++) {
            std::cout << graf[i][j] << ' ';
        }
        std::cout << '\n';
    }
    std::cout << '\n';
}

int** createGraf(int size) {
    int** graf = new int* [size];
    for (int i = 0; i < size; i++) {
        graf[i] = new int[size];
    }
    std::cout << "Выберите формат заполнения матрицы смежности графа \n1-Ввести вручную\n2-Сгенерировать случайно\n";
    int createOption = 0;
    std::cin >> createOption;
    while (createOption != 1 && createOption != 2) {
        std::cout << "Введено недопустимое значение\n1-Ввести вручную\n2-Сгенерировать случайно\n";
        std::cin >> createOption;
    }
    switch (createOption) {
    case 1:
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                char symbol1 = 'A' + i;
                char symbol2 = 'A' + j;
                std::cout << "Введите связь узла " << symbol1 << " c узлом " << symbol2 << "\n";
                int grafValue; std::cin >> grafValue;
                if (grafValue >= 1) grafValue = 1; else grafValue = 0;
                graf[i][j] = grafValue;
            }
        }
        break;
    case 2:
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                graf[i][j] = rand() % 2;
            }
        }
        break;
    default:
        break;
    }
    std::cout << "\nПолучившаяся матрица смежности\n"; drawMat(graf, size);

    return graf;
}

int** multiplyMatrices(int** A, int** B, int size) {
    int** result = new int* [size];
    for (int i = 0; i < size; i++) {
        result[i] = new int[size];
    }
    for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
            result[i][j] = 0;
            for (int k = 0; k < size; k++) {
                result[i][j] += A[i][k] * B[k][j];
            }
        }
    }

    return result;
}

int** transpouseMat(int** firstMat, int size) {
    int** result = new int* [size];
    for (int i = 0; i < size; i++) {
        result[i] = new int[size];
        for (int j = 0; j < size; j++) {
            result[i][j] = firstMat[j][i];
        }
    }
    return result;
}

int** createIdentityMatrix(int size) {
    int** identity = new int* [size];
    for (int i = 0; i < size; i++) {
        identity[i] = new int[size];
        for (int j = 0; j < size; j++) {
            identity[i][j] = (i == j) ? 1 : 0;
        }
    }
    return identity;
}

int** matrixPower(int** matrix, int size, int power) {
    int** result = createIdentityMatrix(size);

    int** base = new int* [size];
    for (int i = 0; i < size; i++) {
        base[i] = new int[size];
        for (int j = 0; j < size; j++) {
            base[i][j] = matrix[i][j];
        }
    }

    while (power > 0) {
        if (power % 2 == 1) {
            int** temp = multiplyMatrices(result, base, size);
            deleteMat(result, size);
            result = temp;
        }
        int** temp = multiplyMatrices(base, base, size);
        deleteMat(base, size);
        base = temp;
        power /= 2;
    }
    deleteMat(base, size);

    return result;
}

int** sumMat(int** firstMat, int** secondMat, int size) {
    int** result = new int* [size];
    for (int i = 0; i < size; i++) {
        result[i] = new int[size];
        for (int j = 0; j < size; j++) {
            result[i][j] = firstMat[i][j] + secondMat[i][j];
        }
    }
    return result;
}

int** createDostMat(int** graf, int size) {
    int** identMat = createIdentityMatrix(size);
    int** dostMat = createIdentityMatrix(size);
    std::cout << "Единичная матрица" << "\n";
    drawMat(identMat, size);

    for (int i = 1; i <= size; i++) {
        int** pow = matrixPower(graf, size, i);
        std::cout << "Матрица в степени " << i << "\n";
        drawMat(pow, size);
        int** temp = sumMat(dostMat, pow, size);
        deleteMat(dostMat, size);
        deleteMat(pow, size);
        dostMat = temp;
    }

    int** transMat = transpouseMat(dostMat, size);
    for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
            dostMat[i][j] = dostMat[i][j] * transMat[i][j];
            if (dostMat[i][j] > 1) dostMat[i][j] = 1;
        }
    }
    deleteMat(transMat, size);
    deleteMat(identMat, size);
    return dostMat;
}

void findStronglyConnectedComponents(int** dostMat, int size, int** components, int& componentCount) {
    bool* visited = new bool[size]();
    componentCount = 0;

    for (int i = 0; i < size; ++i) {
        if (!visited[i]) {
            for (int j = 0; j < size; ++j) {
                if (dostMat[i][j] && dostMat[j][i]) {
                    components[componentCount][j] = 1;
                    visited[j] = true;
                }
            }
            componentCount++;
        }
    }

    delete[] visited;
}

int** buildCondensationGraph(int** graf, int size, int** components, int componentCount) {
    int** condGraph = new int* [componentCount];
    for (int i = 0; i < componentCount; ++i) {
        condGraph[i] = new int[componentCount]();
    }

    for (int i = 0; i < componentCount; ++i) {
        for (int j = 0; j < componentCount; ++j) {
            if (i == j) continue;
            for (int u = 0; u < size; ++u) {
                if (components[i][u]) {
                    for (int v = 0; v < size; ++v) {
                        if (components[j][v] && graf[u][v]) {
                            condGraph[i][j] = 1;
                            break;
                        }
                    }
                    if (condGraph[i][j]) break;
                }
            }
        }
    }

    return condGraph;
}

void drawCondensedMat(int** condGraph, int componentCount, int** components, int size) {
    std::string* labels = new std::string[componentCount];
    for (int i = 0; i < componentCount; ++i) {
        for (int j = 0; j < size; ++j) {
            if (components[i][j]) {
                labels[i] += ('a' + j);
            }
        }
    }

    int maxLabelWidth = 0;
    for (int i = 0; i < componentCount; ++i) {
        if (labels[i].length() > maxLabelWidth) {
            maxLabelWidth = labels[i].length();
        }
    }

    std::cout << std::setw(maxLabelWidth + 2) << " ";
    for (int i = 0; i < componentCount; ++i) {
        std::cout << std::setw(maxLabelWidth + 2) << labels[i];
    }
    std::cout << '\n';

    for (int i = 0; i < componentCount; ++i) {
        std::cout << std::setw(maxLabelWidth + 2) << labels[i] << " ";
        for (int j = 0; j < componentCount; ++j) {
            std::cout << std::setw(maxLabelWidth + 2) << condGraph[i][j];
        }
        std::cout << '\n';
    }

    delete[] labels;
}

void solve() {
    std::cout << "Введите количество узлов графа\n";
    int grafSize; std::cin >> grafSize;
    while (grafSize <= 0) {
        std::cout << "Введено недопустимое значение\nРазмер графа должен быть больше 0\n";
        std::cin >> grafSize;
    }
    int** graf = createGraf(grafSize);

    int** dostMat = createDostMat(graf, grafSize);

    std::cout << "Матрица достижимости графа\n";
    drawMat(dostMat, grafSize);

    int** components = new int* [grafSize];
    for (int i = 0; i < grafSize; ++i) {
        components[i] = new int[grafSize]();
    }
    int componentCount = 0;

    findStronglyConnectedComponents(dostMat, grafSize, components, componentCount);

    int** condGraph = buildCondensationGraph(graf, grafSize, components, componentCount);

    std::cout << "Конденсат графа\n";
    drawCondensedMat(condGraph, componentCount, components, grafSize);

    deleteMat(graf, grafSize);
    deleteMat(dostMat, grafSize);
    deleteMat(condGraph, componentCount);
    deleteMat(components, grafSize);
}

int main() {
    system("chcp 1251");
    srand(time(NULL));
    solve();
    system("PAUSE");
    return 0;
}
