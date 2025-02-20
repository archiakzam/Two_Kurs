using ClassLibrary;
using LabRab11;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;

namespace LabRab11
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainMenu();
        }

        public static void MainMenu()
        {
            bool is_life = true;

            Console.WriteLine("1. Работа с сортированным словарем объектов String - Persona\n2. Работа с упорядоченным списком объектов Persona\n3. Работа с коллекциями LinkedList и SortedDictionary\nДругое значение - выход");

            switch (Console.ReadLine())
            {
                case "1": WorkSortedDictionary(); break;
                case "2": WorkLinkedList(); break;
                case "3": WorkTestCollections(); break;
                default: is_life = false; break;
            }

            if (is_life) MainMenu();
        }

        public static void WorkTestCollections()
        {
            TestCollections coll = new TestCollections();

            bool is_life = true;

            while (is_life)
            {
                Console.WriteLine("\n1. Добавить элементы в коллекции\n2. Показать элементы списка строк\n3. Показать элементы списка person\n4. Показать элементы словаря String - Employee\n5. Показать элементы словаря Persona - Employee\n6. Работа с тестированием коллекций\nДругое значение - выход");

                switch (Console.ReadLine())
                {
                    case "1": coll.AddElement(); break;
                    case "2": coll.ShowLinkedListString(); break;
                    case "3": coll.ShowLinkedList(); break;
                    case "4": coll.ShowStringEmployeeDictionary(); break;
                    case "5": coll.ShowPersonaEmployeeDictionary(); break;
                    case "6":
                        TestCollections testCollections = new TestCollections(1000);
                        testCollections.FindElementLinkedListEmployee();
                        testCollections.FindElementLinkedListString();
                        testCollections.FindElementSortedDictionaryPersonaEmployee();
                        testCollections.FindElementDictionaryStringEmployee();
                        break;
                    default: is_life = false; break;
                }
            }
        }

        public static void WorkSortedDictionary()
        {
            SortedDictionary<string, Persona> dictionary = new SortedDictionary<string, Persona>();
            bool is_life = true;

            while (is_life)
            {
                Console.WriteLine("\n1. Добавить элементы в словарь\n2. Показать словарь \n3. Удалить элементы из словаря \n4. Вывести элементы определенного типа \n5. Вывод словаря при помощи for each\n6. Клонировать словарь\n7. Найти нужный элемент\nДругое значение - выход");

                switch (Console.ReadLine())
                {
                    case "1":
                        FillCollection(new LinkedList<Persona>(), dictionary, TypeOfFillList(), false);
                        break;
                    case "2": ShowDictionary(dictionary); break;
                    case "3": DelDictionary(dictionary); break;
                    case "4": PrintOnlyTypeDictionary(dictionary, EnterType()); break;
                    case "5": ShowForEachCollection(null, dictionary, false); break;
                    case "6": ShowCloningDictionary(dictionary); break;
                    case "7": FindElementDictionary(dictionary); break;
                    default: is_life = false; break;
                }
            }
        }

        public static void WorkLinkedList()
        {
            LinkedList<Persona> list = new LinkedList<Persona>();

            bool is_life = true;

            while (is_life)
            {
                Console.WriteLine("\n1. Добавить элементы в список\n2. Показать список \n3. Удалить элементы из списка \n4. Вывести элементы определенного типа \n5. Вывести количество элементов определенного типа\n6. Клонировать список\n7. Найти нужный элемент\nДругое значение - выход");

                switch (Console.ReadLine())
                {
                    case "1": FillCollection(list, new SortedDictionary<string, Persona>(), TypeOfFillList(), true); break;
                    case "2": ShowLinkedList(list); break;
                    case "3": DelLinkedList(list); break;
                    case "4": PrintOnlyTypeLinkedList(list, EnterType()); break;
                    case "5": CountTypeLinkedList(list, EnterType()); break;
                    case "6": ShowCloningLinkedList(list); break;
                    case "7": FindElementLinkedList(list); break;
                    default: is_life = false; break;
                }
            }
        }
        public static void FillCollection(LinkedList<Persona> list, SortedDictionary<string, Persona> dictionary, int autofill, bool collection)
        {
            bool isLife = true;

            while (isLife)
            {
                Console.WriteLine("Добавьте элементы в словарь:\n1. Persona\n2. Employee \n3. Worker \n4. Engineer\nДругое значение - ничего не добавлять");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        {
                            Persona persona = new Persona();
                            if (autofill == 1)
                            {
                                persona.RandomInit();
                            }
                            else
                            {
                                persona.Init();
                            }
                            persona.Show();
                            Console.WriteLine("\n");

                            if (collection)
                            {
                                list.AddLast(persona); // Добавление в LinkedList
                            }
                            else
                            {
                                dictionary.Add(persona.Name + dictionary.Count.ToString(), persona); // Добавление в словарь
                            }
                            break;
                        }

                    case "2":
                        {
                            Employee employee = new Employee();
                            if (autofill == 1)
                            {
                                employee.RandomInit();
                            }
                            else
                            {
                                employee.Init();
                            }
                            employee.Show();
                            Console.WriteLine("\n");

                            if (collection)
                            {
                                list.AddLast(employee); // Добавление в LinkedList
                            }
                            else
                            {
                                dictionary.Add(employee.Name + dictionary.Count.ToString(), employee); // Добавление в словарь
                            }
                            break;
                        }

                    case "3":
                        {
                            Worker worker = new Worker();
                            if (autofill == 1)
                            {
                                worker.RandomInit();
                            }
                            else
                            {
                                worker.Init();
                            }
                            worker.Show();
                            Console.WriteLine("\n");

                            if (collection)
                            {
                                list.AddLast(worker); // Добавление в LinkedList
                            }
                            else
                            {
                                dictionary.Add(worker.Name + dictionary.Count.ToString(), worker); // Добавление в словарь
                            }
                            break;
                        }

                    case "4":
                        {
                            Engineer engineer = new Engineer();
                            if (autofill == 1)
                            {
                                engineer.RandomInit();
                            }
                            else
                            {
                                engineer.Init();
                            }
                            engineer.Show();
                            Console.WriteLine("\n");

                            if (collection)
                            {
                                list.AddLast(engineer); // Добавление в LinkedList
                            }
                            else
                            {
                                dictionary.Add(engineer.Name + dictionary.Count.ToString(), engineer); // Добавление в словарь
                            }
                            break;
                        }

                    default:
                        isLife = false;
                        break;
                }
            
        
    }
        }


        public static void FindElement(SortedList list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст"); return;
            }

            Console.WriteLine("Введите фамилию человека, которого надо найти:");

            string? sn = Console.ReadLine();
            bool is_found = false;

            foreach (DictionaryEntry i in list)
            {
                if (StringSurname(i.Key.ToString()) == sn)
                {
                    Console.WriteLine("\nНайден элемент:");
                    is_found = true;
                    Persona Persona = (Persona)i.Value;
                    Persona.Show(); Console.WriteLine();
                }
            }
            if (!is_found) Console.WriteLine("Элементы не найдены");
        }

        public static void ShowCloningList(SortedList list)
        {
            Persona p = new Persona();
            p.RandomInit();
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст"); return;
            }

            SortedList cloned = Clone(list);
            Console.WriteLine("\nИсходный список клонирован");

            list.Add(p.Name + list.Count.ToString(), p);

            Console.WriteLine("\nИсходный список изменен");

            ShowList(cloned);
            Console.WriteLine("\nКлонированный список не затронут");
        }

        public static SortedList Clone(SortedList list)
        {
            return new SortedList(list);
        }

        public static void ShowForEachCollection(SortedList list, SortedDictionary<string, Persona> dictionary, bool collection)
        {
            if (collection)
            {
                foreach (DictionaryEntry i in list)
                {
                    if (i.Value is Persona p)
                    {
                        Console.WriteLine("\nКлюч: " + StringSurname(i.Key.ToString()));
                        Console.Write("Объект: ");
                        p.Show();
                        Console.WriteLine();
                    }
                }
            }
            else foreach (KeyValuePair<string, Persona> i in dictionary)
                {
                    Persona p = (Persona)i.Value;
                    Console.WriteLine("\nКлюч: " + StringSurname(i.Key.ToString()));
                    Console.Write("Объект: ");
                    p.Show();
                    Console.WriteLine();
                }
        }

        public static void CountType(SortedList list, Type TypeObject)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст"); return;
            }
            int counter = 0;

            for (int i = 0; i < list.Count; i++)
            {

                if (list.GetByIndex(i).GetType() == TypeObject)
                {
                    counter++;
                }
            }
            if (counter > 0)
            {
                Console.WriteLine("Обнаружено элементов: " + counter);
            }
            else Console.WriteLine("Элементов данного типа нет");
        }

        public static Type EnterType()
        {
            Console.WriteLine("Выберите тип элемента: \n1. persona\n2. Employee \n3. worker \n4. engineer");

            int theType = EnterInt();

            while (theType < 1 || theType > 4)
            {
                Console.WriteLine("Введите число от 1 до 4");
                theType = EnterInt();
            }
            switch (theType)
            {
                case 1: return typeof(Persona);
                case 2: return typeof(Worker);
                case 3: return typeof(Employee);
                case 4: return typeof(Engineer);
                default: return typeof(int);
            }
        }

        public static void PrintOnlyType(SortedList list, Type TypeObject)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст"); return;
            }
            bool is_found = false;

            for (int i = 0; i < list.Count; i++)
            {

                if (list.GetByIndex(i).GetType() == TypeObject)
                {
                    Console.WriteLine("\nКлюч: " + StringSurname(list.GetKey(i).ToString()));
                    Console.Write("Объект: ");

                    if (list.GetByIndex(i) is Persona p)
                    {
                        p.Show();
                        Console.WriteLine();
                    }
                    is_found = true;
                }
            }
            if (!is_found)
            {
                Console.WriteLine("Ничего не найдено");
            }
        }

        public static void DelList(SortedList list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст"); return;
            }

            Console.Write("Введите фамилию(ключ) элемента, который надо удалить: ");
            string? sn = Console.ReadLine();

            bool is_deleted = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (StringSurname(list.GetKey(i).ToString()) == sn)
                {
                    Console.WriteLine("Элемент ");
                    Persona? p = (Persona)list.GetByIndex(i);
                    if (p != null)
                    {
                        p.Show();
                        Console.WriteLine("\nудален\n\n Список объектов: ");
                        list.RemoveAt(i);
                        is_deleted = true;
                    }
                }
            }
            if (!is_deleted)
            {
                Console.WriteLine("Элемент " + sn + " не найден");
            }
        }

        public static void ShowList(SortedList list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст"); return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("\nКлюч: " + StringSurname(list.GetKey(i).ToString()));
                Console.Write("Объект: ");
                if (list.GetByIndex(i) is Persona p)
                {
                    p.Show();
                    Console.WriteLine();
                }
            }
        }

        public static string StringSurname(string surname)
        {
            char[] chars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            return surname.TrimEnd(chars);
        }

        public static int TypeOfFillList()
        {
            Console.WriteLine("Выберите способ формирования списка:\n1. Автоматически\n Другое значение - вручную");
            return EnterInt();
        }

        public static int EnterInt()
        {
            string? str = Console.ReadLine();

            while (!int.TryParse(str, out int x))
            {
                Console.WriteLine("Введите целое число");
                str = Console.ReadLine();
            }

            return int.Parse(str);
        }

        public static int EnterIndex(SortedList list)
        {
            int index = EnterInt();
            while (index < 0 || index > list.Count)
            {
                Console.WriteLine("Число должно быть более 0 и менее" + list.Count + 1);
                index = EnterInt();
            }
            return index - 1;
        }

        public static void ShowDictionary(SortedDictionary<string, Persona> dictionary)
        {
            if (dictionary.Count == 0)
            {
                Console.WriteLine("Словарь пуст"); return;
            }
            foreach (KeyValuePair<string, Persona> kv in dictionary)
            {
                Console.WriteLine("\nКлюч = " + StringSurname(kv.Key.ToString()));
                Persona p = (Persona)kv.Value;
                Console.Write("Объект: ");
                p.Show();
                Console.WriteLine();
            }
        }

        public static void DelDictionary(SortedDictionary<string, Persona> dictionary)
        {
            if (dictionary.Count == 0)
            {
                Console.WriteLine("Словарь пуст");
                return;
            }

            Console.Write("Введите имя (ключ) элемента, который надо удалить: ");
            string nameKey = Console.ReadLine();

            string fullKey = dictionary.Keys.FirstOrDefault(k => k.StartsWith(nameKey));

            if (fullKey != null && dictionary.Remove(fullKey))
            {
                Console.WriteLine($"Элемент с ключом {fullKey} удален");
            }
            else
            {
                Console.WriteLine($"Элемент с ключом {nameKey} не найден");
            }
        }

        public static void PrintOnlyTypeDictionary(SortedDictionary<string, Persona> dictionary, Type TypeObject)
        {
            if (dictionary.Count == 0)
            {
                Console.WriteLine("Словарь пуст"); return;
            }
            bool is_found = false;

            foreach (KeyValuePair<string, Persona> kv in dictionary)
            {
                if (kv.Value.GetType() == TypeObject)
                {
                    Persona p = (Persona)kv.Value;
                    Console.WriteLine("Ключ: " + StringSurname(kv.Key.ToString()));
                    Console.Write("Объект: ");
                    p.Show();
                    Console.WriteLine("\n");
                    is_found = true;
                }
            }

            if (!is_found)
            {
                Console.WriteLine("Ничего не найдено");
            }
        }
        public static SortedDictionary<string, Persona> CloneDictionary(SortedDictionary<string, Persona> dictionary)
        {
            return new SortedDictionary<string, Persona>(dictionary);
        }

        public static void ShowCloningDictionary(SortedDictionary<string, Persona> dictionary)
        {
            Persona newPersona = new Persona();
            newPersona.RandomInit();

            SortedDictionary<string, Persona> clonedDictionary = CloneDictionary(dictionary);
            Console.WriteLine("\nИсходный словарь клонирован");

            dictionary.Add(newPersona.Name, newPersona);
            Console.WriteLine("\nИсходный словарь изменен");

            Console.WriteLine("\nСловарь-клон:");
            ShowDictionary(clonedDictionary);
            Console.WriteLine("\nКлонированный словарь не затронут");
        }

        public static void FindElementDictionary(SortedDictionary<string, Persona> dictionary)
        {
            if (dictionary.Count == 0)
            {
                Console.WriteLine("Словарь пуст"); return;
            }

            Console.WriteLine("Введите имя (ключ) человека, которого надо найти");
            string sn = Console.ReadLine();

            bool is_found = false;
            Console.WriteLine();

            foreach (KeyValuePair<string, Persona> i in dictionary)
            {
                if (sn == StringSurname(i.Key.ToString()))
                {
                    Persona p = (Persona)i.Value;
                    p.Show();
                    Console.WriteLine();
                    is_found = true;
                }
            }
            if (!is_found) Console.WriteLine("Элементы не найдены");
        }
        public static void ShowLinkedList(LinkedList<Persona> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            foreach (Persona persona in list)
            {
                persona.Show();
                Console.WriteLine();
            }
        }
        public static void DelLinkedList(LinkedList<Persona> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Console.WriteLine("Введите имя элемента, который нужно удалить:");
            string nameToDelete = Console.ReadLine();

            LinkedListNode<Persona> currentNode = list.First;

            while (currentNode != null)
            {
                if (currentNode.Value.Name == nameToDelete)
                {
                    list.Remove(currentNode);
                    Console.WriteLine($"Элемент {nameToDelete} удален");
                    return;
                }
                currentNode = currentNode.Next;
            }

            Console.WriteLine($"Элемент {nameToDelete} не найден");
        }
        public static void PrintOnlyTypeLinkedList(LinkedList<Persona> list, Type type)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            bool found = false;

            foreach (Persona persona in list)
            {
                if (persona.GetType() == type)
                {
                    persona.Show();
                    Console.WriteLine();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Элементов указанного типа не найдено");
            }
        }
        public static void CountTypeLinkedList(LinkedList<Persona> list, Type type)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            int count = 0;

            foreach (Persona persona in list)
            {
                if (persona.GetType() == type)
                {
                    count++;
                }
            }

            Console.WriteLine($"Количество элементов типа {type.Name}: {count}");
        }
        public static void ShowCloningLinkedList(LinkedList<Persona> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            LinkedList<Persona> clonedList = new LinkedList<Persona>(list);

            Console.WriteLine("Список успешно клонирован. Вот его содержимое:");
            ShowLinkedList(clonedList);
        }
        public static void FindElementLinkedList(LinkedList<Persona> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Console.WriteLine("Введите имя для поиска:");
            string nameToFind = Console.ReadLine();

            foreach (Persona persona in list)
            {
                if (persona.Name == nameToFind)
                {
                    Console.WriteLine("Элемент найден:");
                    persona.Show();
                    return;
                }
            }

            Console.WriteLine("Элемент не найден");
        }

    }
}