using System;
using ClassLibrary;

namespace LabRab12
{
    public class DemoProgram
    {
        public static void Main(string[] args)
        {
            RedBlackTree<Persona> tree = new RedBlackTree<Persona>();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Удалить элемент");
                Console.WriteLine("3. Поиск элемента");
                Console.WriteLine("4. Глубокое клонирование коллекции");
                Console.WriteLine("5. Поверхностное копирование коллекции");
                Console.WriteLine("6. Очистить коллекцию");
                Console.WriteLine("7. Вывести коллекцию");
                Console.WriteLine("8. Найти количество элементов с заданным ключом");
                Console.WriteLine("9. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddElement(tree);
                        break;
                    case "2":
                        RemoveElement(tree);
                        break;
                    case "3":
                        SearchElement(tree);
                        break;
                    case "4":
                        DeepCloneCollection(tree);
                        break;
                    case "5":
                        ShallowCopyCollection(tree);
                        break;
                    case "6":
                        ClearCollection(tree);
                        break;
                    case "7":
                        ShowCollection(tree);
                        break;
                    case "8":
                        CountElementsByKey(tree);
                        break;
                    case "9":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void AddElement(RedBlackTree<Persona> tree)
        {
            Console.WriteLine("Выберите тип элемента:");
            Console.WriteLine("1. Persona");
            Console.WriteLine("2. Employee");
            Console.WriteLine("3. Worker");
            Console.WriteLine("4. Engineer");

            string typeChoice = Console.ReadLine();

            Persona persona = null;

            switch (typeChoice)
            {
                case "1":
                    persona = new Persona();
                    break;
                case "2":
                    persona = new Employee();
                    break;
                case "3":
                    persona = new Worker();
                    break;
                case "4":
                    persona = new Engineer();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    return;
            }

            Console.WriteLine("Введите данные вручную? (y/n)");
            string input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                persona.Init();
            }
            else
            {
                persona.RandomInit();
            }

            tree.Add(persona);
            Console.WriteLine("Элемент добавлен.");
        }

        private static void RemoveElement(RedBlackTree<Persona> tree)
        {
            Console.WriteLine("Введите имя элемента для удаления:");
            string name = Console.ReadLine();

            var persona = new Persona { Name = name };
            if (tree.Contains(persona))
            {
                tree.Remove(persona);
                Console.WriteLine("Элемент удален.");
            }
            else
            {
                Console.WriteLine("Элемент не найден.");
            }
        }

        private static void SearchElement(RedBlackTree<Persona> tree)
        {
            Console.WriteLine("Введите имя элемента для поиска:");
            string name = Console.ReadLine();

            var persona = new Persona { Name = name };
            if (tree.Contains(persona))
            {
                Console.WriteLine("Элемент найден:");
                persona.Show();
            }
            else
            {
                Console.WriteLine("Элемент не найден.");
            }
        }

        private static void DeepCloneCollection(RedBlackTree<Persona> tree)
        {
            RedBlackTree<Persona> clonedTree = tree.DeepClone();
            Console.WriteLine("Коллекция глубоко клонирована:");
            ShowCollection(clonedTree);
        }

        private static void ShallowCopyCollection(RedBlackTree<Persona> tree)
        {
            RedBlackTree<Persona> copiedTree = tree.ShallowCopy();
            Console.WriteLine("Коллекция поверхностно скопирована:");
            ShowCollection(copiedTree);
        }

        private static void ClearCollection(RedBlackTree<Persona> tree)
        {
            tree.Clear();
            Console.WriteLine("Коллекция очищена.");
        }

        private static void ShowCollection(RedBlackTree<Persona> tree)
        {
            if (tree.Count() == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            Console.WriteLine("Элементы коллекции:");
            foreach (var persona in tree)
            {
                persona.Show();
                Console.WriteLine("\n");
            }
        }

        private static void CountElementsByKey(RedBlackTree<Persona> tree)
        {
            Console.WriteLine("Введите ключ (имя) для поиска:");
            string key = Console.ReadLine();

            int count = tree.CountByKey(p => p.Name, key);
            Console.WriteLine($"Количество элементов с ключом '{key}': {count}");
        }
    }
}