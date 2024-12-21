using ClassLibrary;
using System;
class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Создать объекты и вывести их (сортировка по возрасту)");
            Console.WriteLine("2 - Демонстрация клонирования объектов");
            Console.WriteLine("3 - Работа с интерфейсом IInit");
            Console.WriteLine("4 - Выход");

            int choice = LibConvert.TryToConvertInt();

            switch (choice)
            {
                case 1:
                    HandleObjectCreation();
                    break;

                case 2:
                    DemonstrateCloning();
                    break;

                case 3:
                    WorkWithIInit();
                    break;

                case 4:
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void HandleObjectCreation()
    {
        Console.WriteLine("Введите количество объектов:");
        int count = LibConvert.TryToConvertIntMoreZero();

        Persona[] personas = new Persona[count];

        Console.WriteLine("Выберите способ заполнения объектов:");
        Console.WriteLine("1 - Ручной ввод");
        Console.WriteLine("2 - Случайная генерация");
        int fillChoice = LibConvert.TryToConvertInt();

        for (int i = 0; i < count; i++)
        {
            switch (i % 4)
            {
                case 0:
                    personas[i] = new Persona();
                    break;
                case 1:
                    personas[i] = new Employee();
                    break;
                case 2:
                    personas[i] = new Worker();
                    break;
                case 3:
                    personas[i] = new Engineer();
                    break;
            }

            if (fillChoice == 1)
            {
                personas[i].Init();
            }
            else if (fillChoice == 2)
            {
                personas[i].RandomInit();
            }
            else
            {
                Console.WriteLine("Некорректный выбор. Используется случайная генерация.");
                personas[i].RandomInit();
            }
        }

        Console.WriteLine("Виртуальные и невиртуальные методы:");
        foreach (Persona persona in personas)
        {
            Console.WriteLine("Виртуальный метод Show:");
            persona.Show();
            Console.WriteLine("Не виртуальный метод ShowNonVirtual:");
            persona.ShowNonVirtual();
            Console.WriteLine();
        }

        Console.WriteLine("Сортировка по имени:");
        Array.Sort(personas, new Comparer(SortCriteria.Name));
        Console.WriteLine("\nМассив после сортировки по имени:");
        foreach (var person in personas)
        {
            person.Show();
        }
        Console.WriteLine("\nБинарный поиск:");
        Console.WriteLine("\nВведите имя для поиска:");
        string searchName = Console.ReadLine();

        
        string[] names = new string[personas.Length];
        for (int i = 0; i < personas.Length; i++)
        {
            names[i] = personas[i].Name;
        }
        int index = Array.BinarySearch(names, searchName, StringComparer.OrdinalIgnoreCase);

        if (index >= 0)
        {
            Console.WriteLine($"Объект с именем \"{searchName}\" найден:");
            personas[index].Show();
        }
        else
        {
            Console.WriteLine($"Объект с именем \"{searchName}\" не найден.");
        }
    }
    


    static void DemonstrateCloning()
    {

        
            Persona[] objects = new Persona[4]
            {
        new Persona(),
        new Employee(),
        new Worker(),
        new Engineer()
            };

            foreach (var obj in objects)
            {
                obj.RandomInit();

                Console.WriteLine("Оригинальный объект:");
                obj.Show();
                Console.WriteLine("Не виртуальный метод ShowNonVirtual:");
                obj.ShowNonVirtual();

                var shallowCopy = obj.ShallowCopy();
                Console.WriteLine("\nПоверхностная копия:");
                shallowCopy.Show();
                Console.WriteLine("Не виртуальный метод ShowNonVirtual для поверхностной копии:");
                shallowCopy.ShowNonVirtual();

                var deepCopy = (Persona)obj.Clone();
                Console.WriteLine("\nГлубокая копия:");
                deepCopy.Show();
                Console.WriteLine("Не виртуальный метод ShowNonVirtual для глубокой копии:");
                deepCopy.ShowNonVirtual();

                Console.WriteLine("\nИзменяем оригинальный объект...");
                obj.Name = "Changed Name";
                if (obj is Employee employee)
                {
                    employee.Address.City = "Los Angeles";
                    employee.Address.Street = "Sunset Boulevard";
                }

                Console.WriteLine("\nОригинальный объект после изменения:");
                obj.Show();
                Console.WriteLine("Не виртуальный метод ShowNonVirtual:");
                obj.ShowNonVirtual();

                Console.WriteLine("\nПоверхностная копия после изменения оригинального объекта:");
                shallowCopy.Show();
                Console.WriteLine("Не виртуальный метод ShowNonVirtual:");
                shallowCopy.ShowNonVirtual();

                Console.WriteLine("\nГлубокая копия после изменения оригинального объекта:");
                deepCopy.Show();
                Console.WriteLine("Не виртуальный метод ShowNonVirtual:");
                deepCopy.ShowNonVirtual();

                Console.WriteLine("\n--------------------------------\n");
            }
        }

   


    static void WorkWithIInit()
    {
        IInit[] initObjects = new IInit[5]
        {
        new Persona(),
        new Employee(),
        new Worker(),
        new Engineer(),
        new Vehicle()
        };

        foreach (var obj in initObjects)
        {
            obj.RandomInit();
            obj.Show();

            if (obj is Persona personaObj)
            {
                Console.WriteLine("Не виртуальный метод ShowNonVirtual:");
                personaObj.ShowNonVirtual();
            }

            Console.WriteLine();
        }
    }

}
