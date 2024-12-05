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
            
            switch(i % 4)
            {
                case 0:
                    personas[i] = new Persona();
                    break;
                case 1:
                    personas[i] = new Employee(); break;
                case 2:
                    personas[i] = new Worker(); break;
                case 3:
                    personas[i] = new Engineer(); break;
                default :
                    Console.WriteLine("Невероне значение");
                    break ;
            };

            
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

            personas[i].Show();
            Console.WriteLine();
        }

        
        Console.WriteLine("Сортировка по возрасту:");
        Array.Sort(personas);
        foreach (var persona in personas)
        {
            persona.Show();
            Console.WriteLine("\n");
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

            var shallowCopy = obj.ShallowCopy();
            Console.WriteLine("\nПоверхностная копия:");
            shallowCopy.Show();

            var deepCopy = (Persona)obj.Clone();
            Console.WriteLine("\nГлубокая копия:");
            deepCopy.Show();

            Console.WriteLine("\nИзменяем оригинальный объект...");
            obj.Name = "Changed Name";
            if (obj is Employee employee)
            {
                employee.Address.City = "Los Angeles";
                employee.Address.Street = "Sunset Boulevard";
            }

            Console.WriteLine("\nОригинальный объект после изменения:");
            obj.Show();

            Console.WriteLine("\nПоверхностная копия после изменения оригинального объекта:");
            shallowCopy.Show();

            Console.WriteLine("\nГлубокая копия после изменения оригинального объекта:");
            deepCopy.Show();

            Console.WriteLine("\n--------------------------------\n");
        }
    }


    static void WorkWithIInit()
    {
        // Работа с элементами интерфейса IInit
        IInit[] initObjects = new IInit[5]
        {
            new Persona(),
            new Employee(),
            new Worker(),
            new Engineer(),
            new Vehicle() // Если Vehicle реализует IInit
        };

        foreach (var obj in initObjects)
        {
            obj.RandomInit();
            obj.Show();
        }
    }
}
