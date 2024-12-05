#include <iostream>
#include <vector>
#include <string>
#include <cmath>
#include <cstdlib>
#include <ctime>
#include <iomanip>
#include <algorithm>
using namespace std;

void generateTruthTable(vector<vector<int>> & table) {
    int n = table[0].size(); 
    int rows = table.size();
    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < n; j++) {
            table[i][j] = (i >> (n - j - 1)) & 1; 
        }
    }
    
}
vector<int> inputFunctionValues(int rows) {
    vector<int> values(rows);
    cout << "Введите значения функции (0 или 1) для каждой строки таблицы истинности:\n";
    for (int i = 0; i < rows; i++) {
        int values1 = -1;
        while (values1 != 0 && values1 != 1) {  
            cout << "Введите значение для строки " << i + 1 << " (0 или 1): ";
            cin >> values1;
            if (values1 != 0 && values1 != 1) {
            cout << "Неверный ввод. Повторите попытку.\n";
            }
        }
        values[i]=values1;
    }
    return values;
}

vector<int> generateRandomFunctionValues(int rows) {
    vector<int> values(rows);
    srand(time(0));
    for (int i = 0; i < rows; i++) {
        values[i] = rand() % 2;
    }
    return values;
}


string getSDNF(const vector<vector<int>>& table, const vector<int>& values) {
    string sdnf;
    for (size_t i = 0; i < values.size(); i++) {
        if (values[i] == 1) {
            string term = "";
            for (size_t j = 0; j < table[i].size(); j++) {
                if (!term.empty()) term += "&";
                char variable = 'a' + j;
                term += table[i][j] ? string(1, variable) : "!" + string(1, variable);
            }
            if (!sdnf.empty()) sdnf += " v ";
            sdnf += term;
        }
    }
    return sdnf.empty() ? "0" : sdnf;
}


string getSKNF(const vector<vector<int>>& table, const vector<int>& values) {
    string sknf;
    for (size_t i = 0; i < values.size(); i++) {
        if (values[i] == 0) {
            string clause = "(";
            for (size_t j = 0; j < table[i].size(); j++) {
                if (j > 0) clause += "v";
                char variable = 'a' + j;
                clause += table[i][j] ? "!" + string(1, variable) : string(1, variable);
            }
            clause += ")";
            if (!sknf.empty()) sknf += " & ";
            sknf += clause;
        }
    }
    return sknf.empty() ? "1" : sknf;
}


string minimizeSDNF(const vector<vector<int>>& table, const vector<int>& values) {
    vector<string> primeImplicants;

    
    for (size_t i = 0; i < values.size(); i++) {
        if (values[i] == 1) {
            string term = "";
            for (size_t j = 0; j < table[i].size(); j++) {
                char variable = 'a' + j;
                term += table[i][j] ? string(1, variable) : "!" + string(1, variable);
            }
            primeImplicants.push_back(term);
        }
    }

    
    bool merged;
    do {
        merged = false;
        vector<string> nextImplicants;
        vector<bool> used(primeImplicants.size(), false);

        for (size_t i = 0; i < primeImplicants.size(); i++) {
            for (size_t j = i + 1; j < primeImplicants.size(); j++) {
                string mergedTerm = "";
                int diffCount = 0;

                
                for (size_t k = 0; k < primeImplicants[i].size(); k++) {
                    if (primeImplicants[i][k] != primeImplicants[j][k]) {
                        diffCount++;
                        mergedTerm += "-";
                    } else {
                        mergedTerm += primeImplicants[i][k];
                    }
                }

                if (diffCount == 1) {
                    merged = true;
                    used[i] = true;
                    used[j] = true;
                    if (find(nextImplicants.begin(), nextImplicants.end(), mergedTerm) == nextImplicants.end()) {
                        nextImplicants.push_back(mergedTerm);
                    }
                }
            }
        }

        for (size_t i = 0; i < primeImplicants.size(); i++) {
            if (!used[i] && find(nextImplicants.begin(), nextImplicants.end(), primeImplicants[i]) == nextImplicants.end()) {
                nextImplicants.push_back(primeImplicants[i]);
            }
        }

        primeImplicants = nextImplicants;
    } while (merged);

    
    string mdnf;
    for (const string& term : primeImplicants) {
        if (!mdnf.empty()) mdnf += " v ";
        for (char ch : term) {
            if (ch == '-') continue; 
            mdnf += ch;
        }
    }
    return mdnf.empty() ? "0" : mdnf;
}

int main() {
    system ("chcp 1251");
    setlocale(LC_ALL,"RU");
    system("cls");
    int n;
    
    cout << "Введите количество переменных (n <= 5): ";
    cin >> n;

    if (n < 1 || n > 5) {
        cout << "Ошибка: n должно быть в диапазоне от 1 до 5.\n";
        return 1;
    }
    
    int choise=0;
    
    int rows = pow(2, n);
    vector<vector<int>> table(rows, vector<int>(n, 0));
    generateTruthTable(table);   

    cout << "Таблица истинности:\n";
    for (const auto& row : table) {
        for (int val : row) {
            cout << val << " ";
        }
        cout << endl;
    }

    choise=0;
    cout << "Выберите способ задания значений функции:\n1 - ввод с клавиатуры\n2 - случайное заполнение\nВаш выбор: ";
    while (choise != 1 && choise != 2) {
        cin >> choise;
    if (choise != 1 && choise != 2) {
        cout << "Неверный выбор. Повторите ввод: ";
    }
}

    vector<int> functionValues;
    switch (choise)
    {
    case 1:
        functionValues = inputFunctionValues(rows);
        break;
    case 2:
        functionValues = generateRandomFunctionValues(rows);
        
        break;
    default:
        break;
    }
    cout<<"\n\nТаблица истинности и значения функции"<<endl;
    for (int i=0;i<rows;i++) {
        for (int j=0;j<n;j++) {
            cout << table[i][j] << " ";
        }
        cout<<"\t"<<functionValues[i]<< endl;
        
    }
    cout << "\nСДНФ: " << getSDNF(table, functionValues) << "\n";
    cout << "СКНФ: " << getSKNF(table, functionValues) << "\n";

    cout << "\nМДНФ: " << minimizeSDNF(table, functionValues) << "\n";

    return 0;
}
