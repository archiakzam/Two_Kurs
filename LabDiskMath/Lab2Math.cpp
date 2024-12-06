#include <iostream>
#include <vector>
#include <string>
#include <cmath>
#include <cstdlib>
#include <ctime>
#include<set>
#include<map>
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

string mergeTerms(const string& term1, const string& term2, bool& merged) {
    string result;
    int diffCount = 0;
    for (size_t i = 0; i < term1.size(); i++) {
        if (term1[i] != term2[i]) {
            diffCount++;
            result += "-";
        } else {
            result += term1[i];
        }
    }
    if (diffCount == 1) {
        merged = true;
        return result;
    }
    return "";
}

string minimizeSDNF(const vector<vector<int>>& table, const vector<int>& values) {
    vector<string> minterms;

    for (size_t i = 0; i < values.size(); i++) {
        if (values[i] == 1) {
            string term = "";
            for (size_t j = 0; j < table[i].size(); j++) {
                term += table[i][j] ? '1' : '0';
            }
            minterms.push_back(term);
        }
    }

    vector<string> primeImplicants;
    while (!minterms.empty()) {
        vector<string> nextTerms;
        vector<bool> used(minterms.size(), false);
        bool merged = false;

        for (size_t i = 0; i < minterms.size(); i++) {
            for (size_t j = i + 1; j < minterms.size(); j++) {
                bool tempMerged = false;
                string mergedTerm = mergeTerms(minterms[i], minterms[j], tempMerged);
                if (tempMerged) {
                    merged = true;
                    used[i] = true;
                    used[j] = true;
                    if (find(nextTerms.begin(), nextTerms.end(), mergedTerm) == nextTerms.end()) {
                        nextTerms.push_back(mergedTerm);
                    }
                }
            }
        }

        
        for (size_t i = 0; i < minterms.size(); i++) {
            if (!used[i] && find(primeImplicants.begin(), primeImplicants.end(), minterms[i]) == primeImplicants.end()) {
                primeImplicants.push_back(minterms[i]);
            }
        }

        minterms = nextTerms;
        if (!merged) break;
    }

    map<string, set<int>> coverage;
    for (size_t i = 0; i < table.size(); i++) {
        if (values[i] == 1) {
            for (const auto& implicant : primeImplicants) {
                bool covers = true;
                for (size_t j = 0; j < implicant.size(); j++) {
                    if (implicant[j] == '-') continue;
                    if (table[i][j] != (implicant[j] - '0')) {
                        covers = false;
                        break;
                    }
                }
                if (covers) {
                    coverage[implicant].insert(i);
                }
            }
        }
    }

    set<int> uncovered;
    for (size_t i = 0; i < values.size(); i++) {
        if (values[i] == 1) {
            uncovered.insert(i);
        }
    }

    vector<string> essentialImplicants;
    while (!uncovered.empty()) {
        
        string bestImplicant;
        size_t maxCovered = 0;
        for (const auto& [implicant, rows] : coverage) {
            size_t coveredCount = 0;
            for (int row : rows) {
                if (uncovered.count(row)) {
                    coveredCount++;
                }
            }
            if (coveredCount > maxCovered) {
                maxCovered = coveredCount;
                bestImplicant = implicant;
            }
        }

        essentialImplicants.push_back(bestImplicant);
        for (int row : coverage[bestImplicant]) {
            uncovered.erase(row);
        }
    }

    string mdnf;
    for (const auto& implicant : essentialImplicants) {
        if (!mdnf.empty()) mdnf += " v ";
        for (size_t j = 0; j < implicant.size(); j++) {
            if (implicant[j] == '-') continue;
            char variable = 'a' + j;
            if (implicant[j] == '0') mdnf += "!" + string(1, variable);
            else mdnf += variable;
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
