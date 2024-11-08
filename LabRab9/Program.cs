using LabRab9;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAB9
{
    internal class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            bool isFinal = false;
            int size;

            while (!isFinal)
            {
                Console.WriteLine("Как заполнить массив: 1 - Автоматически, 2 - Вручную, 3 - Выход");
                int choice = Interface.ReadElem();

                MoneyArray moneyArray = null; // Объявляем общую переменную

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите размер массива: ");
                        size = Interface.ReadElem();
                        moneyArray = new MoneyArray(size, rnd);
                        Console.WriteLine("\nМассив со случайными значениями:");
                        moneyArray.Display();
                        Console.Write("Минимальный элемент: ");
                        moneyArray.GetMin().Display();
                        break;

                    case 2:
                        Console.WriteLine("Введите размер массива: ");
                        size = Interface.ReadElem();
                        moneyArray = new MoneyArray(size);
                        Console.WriteLine("\nМассив с ручным вводом значений:");
                        moneyArray.Display();
                        Console.Write("Минимальный элемент: ");
                        moneyArray.GetMin().Display();
                        break;

                    case 3:
                        isFinal = true;
                        continue;

                    default:
                        Console.WriteLine("Повторите попытку");
                        continue;
                }

                if (moneyArray == null)
                {
                    Console.WriteLine("Ошибка создания массива. Попробуйте снова.");
                    continue;
                }

                // Демонстрация операций
                Console.WriteLine("\nВыберите номер элемента для демонстрации операций:");
                int index = int.Parse(Console.ReadLine());
                index--;    
                if (index < 0 || index > MoneyArray.GetObjectCount())

                {
                    Console.WriteLine("Индекс вне допустимого диапазона.");
                    continue;
                }

                Money selectedMoney = moneyArray[index];

                bool operationRunning = true;
                while (operationRunning)
                {
                    Console.WriteLine("\nВыберите операцию:");
                    Console.WriteLine("1 - Инкремент (++)");
                    Console.WriteLine("2 - Декремент (--)");
                    Console.WriteLine("3 - Приведение к int (рубли)");
                    Console.WriteLine("4 - Приведение к double (копейки)");
                    Console.WriteLine("5 - Бинарная операция + (добавить копейки)");
                    Console.WriteLine("6 - Бинарная операция - (вычесть копейки)");
                    Console.WriteLine("7 - Выход из операций");

                    int operationChoice = int.Parse(Console.ReadLine());

                    switch (operationChoice)
                    {
                        case 1:
                            // Инкремент
                            selectedMoney++;
                            Console.WriteLine("После операции ++:");
                            selectedMoney.Display();
                            break;

                        case 2:
                            // Декремент
                            selectedMoney--;
                            Console.WriteLine("После операции --:");
                            selectedMoney.Display();
                            break;

                        case 3:
                            // Приведение к int
                            int rublesOnly = (int)selectedMoney;
                            Console.WriteLine($"Рубли (явное приведение к int): {rublesOnly}");
                            break;

                        case 4:
                            // Приведение к double
                            double kopeksOnly = selectedMoney;
                            Console.WriteLine($"Копейки (неявное приведение к double): {kopeksOnly}");
                            break;

                        case 5:
                            // Бинарная операция +
                            Console.Write("Введите количество копеек для добавления: ");
                            int addKopeks = int.Parse(Console.ReadLine());
                            selectedMoney = selectedMoney + addKopeks;
                            Console.WriteLine("После добавления копеек:");
                            selectedMoney.Display();
                            break;

                        case 6:
                            // Бинарная операция -
                            Console.Write("Введите количество копеек для вычитания: ");
                            int subKopeks = int.Parse(Console.ReadLine());
                            selectedMoney = selectedMoney - subKopeks;
                            Console.WriteLine("После вычитания копеек:");
                            selectedMoney.Display();
                            break;

                        case 7:
                            operationRunning = false;
                            break;

                        default:
                            Console.WriteLine("Неверный выбор, попробуйте снова.");
                            break;
                    }
                }

                Console.WriteLine($"\nОбщее количество созданных объектов Money: {MoneyArray.GetObjectCount()}");
            }
        }
    }

    public static class Interface
    {
        public static int ReadElem()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input <= 0)
            {
                Console.WriteLine("Введите корректное положительное число.");
            }
            return input;
        }

        public static void Write(MoneyArray array)
        {
            array.Display();
        }

        public static void Write(Money money)
        {
            money.Display();
        }
    }
}
