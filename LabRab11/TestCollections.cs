using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace LabRab11
{
    public class TestCollections
    {
        private LinkedList<Employee> linkedListEmployee = new LinkedList<Employee>();
        private LinkedList<string> linkedListString = new LinkedList<string>();
        private SortedDictionary<Persona, Employee> dictPersonaEmployee = new SortedDictionary<Persona, Employee>();
        private SortedDictionary<string, Employee> dictStringEmployee = new SortedDictionary<string, Employee>();

        public TestCollections() { }
        public TestCollections(int count)
        {
            ClearCollections();

            for (int i = 0; i < count; i++)
            {
                Employee employee = new Employee();
                employee.RandomInit();

                string uniqueKey = GenerateUniqueKey(employee);

                linkedListEmployee.AddLast(employee);
                linkedListString.AddLast(uniqueKey);

                Persona personaKey = new Persona
                {
                    Name = $"{employee.BasePersona.Name}_{i}",
                    Age = employee.BasePersona.Age
                };

                if (dictPersonaEmployee.ContainsKey(personaKey))
                {
                    Console.WriteLine("Ошибка: Сотрудник с такими данными уже существует.");
                    return;
                }

                dictPersonaEmployee.Add(personaKey, employee);

                if (dictStringEmployee.ContainsKey(uniqueKey))
                {
                    Console.WriteLine("Ошибка: Сотрудник с таким именем уже существует.");
                    return;
                }

                dictStringEmployee.Add(uniqueKey, employee);
            }
        }


        private void ClearCollections()
        {
            linkedListEmployee.Clear();
            linkedListString.Clear();
            dictPersonaEmployee.Clear();
            dictStringEmployee.Clear();
        }
        private string GenerateUniqueKey(Employee employee)
        {
            return $"{employee.BasePersona.Name}_{new Random().Next(1, 100000)}";
        }

        public void AddElement()
        {
            Console.WriteLine("Добавление нового элемента:\nВыберите способ ввода:\n1. Автоматический ввод\nЛюбое другое значение - ручной ввод");

            Employee employee = new Employee();

            if (Console.ReadLine() == "1")
            {
                employee.RandomInit();
            }
            else
            {
                employee.Init();
            }

            string uniqueKey = GenerateUniqueKey(employee);
                
            if (dictStringEmployee.ContainsKey(uniqueKey))
            {
                Console.WriteLine("Ошибка: Сотрудник с таким именем уже существует.");
                return;
            }

            linkedListEmployee.AddLast(employee);
            linkedListString.AddLast(uniqueKey);
            dictPersonaEmployee.Add(employee.BasePersona, employee);
            dictStringEmployee.Add(uniqueKey, employee);

            Console.WriteLine("Элемент добавлен:");
            ShowEmployee(employee);
        }

        public void DelElement()
        {
            Console.WriteLine("Удаление элемента: Введите имя сотрудника для удаления:");
            string name = Console.ReadLine();

            if (!dictStringEmployee.ContainsKey(name))
            {
                Console.WriteLine("Сотрудник с таким именем не найден.");
                return;
            }

            Employee employee = dictStringEmployee[name];

            linkedListEmployee.Remove(employee);
            linkedListString.Remove(name);
            dictPersonaEmployee.Remove(employee.BasePersona);
            dictStringEmployee.Remove(name);

            Console.WriteLine("Сотрудник удален:");
            ShowEmployee(employee);
        }

        private Employee CreateRandomEmployee()
        {
            Employee employee = new Employee();
            employee.RandomInit();
            return employee;
        }



        public void ShowPersonaEmployeeDictionary()
        {
            if (dictPersonaEmployee.Count == 0)
            {
                Console.WriteLine("Словарь пуст.");
                return;
            }

            foreach (var pair in dictPersonaEmployee)
            {
                Console.Write("\nКлюч: ");
                ShowPersona(pair.Key);
                Console.Write("\nОбъект: ");
                ShowEmployee(pair.Value);
            }
        }

        public void ShowStringEmployeeDictionary()
        {
            if (dictStringEmployee.Count == 0)
            {
                Console.WriteLine("Словарь пуст.");
                return;
            }

            foreach (var pair in dictStringEmployee)
            {
                Console.WriteLine($"\nКлюч-строка: {pair.Key}");
                Console.Write("\nОбъект: ");
                ShowEmployee(pair.Value);
            }
        }

        public void ShowLinkedList()
        {
            if (linkedListEmployee.Count == 0)
            {
                Console.WriteLine("Связанный список пуст.");
                return;
            }

            foreach (var employee in linkedListEmployee)
            {
                ShowEmployee(employee);
            }
        }
        public void ShowLinkedListString()
        {
            if (linkedListString.Count == 0)
            {
                Console.WriteLine("Связанный список строк пуст.");
                return;
            }

            Console.WriteLine("Содержимое связанного списка строк:");
            foreach (var str in linkedListString)
            {
                Console.WriteLine(str);
            }
        }

        public void FindElementLinkedListString()
        {
            bool f = false;
            Stopwatch sw = new Stopwatch();
            List<string> list = linkedListString.ToList();

            string first = list[0];
            string middle = list[list.Count / 2];
            string final = list[list.Count - 1];
            string notexists = "Василевский Игорь Владимирович возраст: 195 ПНИПУ курс: 9";

            Console.WriteLine("Связанный список строк:\n");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Попытка № " + (i + 1) + ":" + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListString.Contains(first);
                sw.Stop();
                Console.WriteLine(f ? "Первый элемент найден" : "Первый элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListString.Contains(middle);
                sw.Stop();
                Console.WriteLine(f ? "Средний элемент найден" : "Средний элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListString.Contains(final);
                sw.Stop();
                Console.WriteLine(f ? "Последний элемент найден" : "Последний элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListString.Contains(notexists);
                sw.Stop();
                Console.WriteLine(f ? "Несуществующий элемент найден" : "Несуществующий элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");
            }
        }

        public void FindElementLinkedListEmployee()
        {
            bool f = false;
            Stopwatch sw = new Stopwatch();
            List<Employee> list = linkedListEmployee.ToList();

            Employee first = list[0];
            Employee middle = list[list.Count / 2];
            Employee final = list[list.Count - 1];
            Employee notexists = new Employee();
            notexists.RandomInit();
            notexists.Salary = 150000;

            Console.WriteLine("Связанный список сотрудников:\n");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Попытка № " + (i + 1) + ":" + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListEmployee.Contains(first);
                sw.Stop();
                Console.WriteLine(f ? "Первый элемент найден" : "Первый элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListEmployee.Contains(middle);
                sw.Stop();
                Console.WriteLine(f ? "Средний элемент найден" : "Средний элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListEmployee.Contains(final);
                sw.Stop();
                Console.WriteLine(f ? "Последний элемент найден" : "Последний элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = linkedListEmployee.Contains(notexists);
                sw.Stop();
                Console.WriteLine(f ? "Несуществующий элемент найден" : "Несуществующий элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");
            }
        }

        public void FindElementSortedDictionaryPersonaEmployee()
        {
            bool f = false;
            Stopwatch sw = new Stopwatch();

            string firstPersonaKey = dictPersonaEmployee.Keys.First().Name;
            string middlePersonaKey = dictPersonaEmployee.ElementAt(dictPersonaEmployee.Count / 2).Key.Name;
            string finalPersonaKey = dictPersonaEmployee.Keys.Last().Name;
            string nonexistsPersonaKey = "Несуществующее Имя";

            Console.WriteLine("Сортированный словарь Persona - Employee:\n");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Попытка № " + (i + 1) + ":\n");

                sw.Reset();
                sw.Start();
                f = dictPersonaEmployee.Keys.Any(p => p.Name == firstPersonaKey);
                sw.Stop();
                Console.WriteLine(f ? "Первый элемент (по имени) найден" : "Первый элемент (по имени) не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = dictPersonaEmployee.Keys.Any(p => p.Name == middlePersonaKey);
                sw.Stop();
                Console.WriteLine(f ? "Средний элемент (по имени) найден" : "Средний элемент (по имени) не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = dictPersonaEmployee.Keys.Any(p => p.Name == finalPersonaKey);
                sw.Stop();
                Console.WriteLine(f ? "Последний элемент (по имени) найден" : "Последний элемент (по имени) не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = dictPersonaEmployee.Keys.Any(p => p.Name == nonexistsPersonaKey);
                sw.Stop();
                Console.WriteLine(f ? "Несуществующий элемент (по имени) найден" : "Несуществующий элемент (по имени) не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");
            }
        }

        public void FindElementDictionaryStringEmployee()
        {
            bool f = false;
            Stopwatch sw = new Stopwatch();

            string firstString = dictStringEmployee.Keys.First();
            string middleString = dictStringEmployee.ElementAt(dictStringEmployee.Count / 2).Key;
            string finalString = dictStringEmployee.Keys.Last();
            string nonexistsString = "Несуществующее Имя";

            Console.WriteLine("Словарь String - Employee:\n");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Попытка № " + (i + 1) + ":\n");

                sw.Reset();
                sw.Start();
                f = dictStringEmployee.ContainsKey(firstString);
                sw.Stop();
                Console.WriteLine(f ? "Первый элемент найден" : "Первый элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = dictStringEmployee.ContainsKey(middleString);
                sw.Stop();
                Console.WriteLine(f ? "Средний элемент найден" : "Средний элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = dictStringEmployee.ContainsKey(finalString);
                sw.Stop();
                Console.WriteLine(f ? "Последний элемент найден" : "Последний элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");

                sw.Reset();
                sw.Start();
                f = dictStringEmployee.ContainsKey(nonexistsString);
                sw.Stop();
                Console.WriteLine(f ? "Несуществующий элемент найден" : "Несуществующий элемент не найден");
                Console.WriteLine("Затраченное время = " + sw.ElapsedTicks + "\n");
            }
        }


        public void ShowEmployee(Employee employee)
        {
            Persona persona = employee.BasePersona;
            Console.WriteLine($"{persona.Name}, Возраст: {persona.Age}, Должность: {employee.Position}, Зарплата: {employee.Salary}");
        }

        public void ShowPersona(Persona persona)
        {
            Console.WriteLine($"{persona.Name}, Возраст: {persona.Age}");
        }

        public string StrDigit(string str)
        {
            string str2 = "";
            int i = 0;
            while (str[i] != ' ' && i < str.Length)
            {
                if (!Char.IsDigit(str[i]))
                {
                    str2 += str[i];
                }
                i++;
            }
            for (; i < str.Length; i++)
            {
                str2 += str[i];
            }
            return str2;
        }
    }

}