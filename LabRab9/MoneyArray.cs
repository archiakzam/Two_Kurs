using System;

namespace LabRab9
{
    public class MoneyArray
    {
        private Money[] arr;
        private static int objectCount = 0;

        public MoneyArray()
        {
            arr = new Money[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Money();
            }
        }

        public MoneyArray(int size, Random rnd)
        {
            arr = new Money[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Money(rnd.Next(0, 100), rnd.Next(0, 100));
            }
            objectCount += arr.Length;
        }

        public MoneyArray(int size)
        {
            arr = new Money[size];
            for (int i = 0; i < arr.Length; i++)
            {
                int rubles, kopeks;
                
                do
                {
                    Console.WriteLine($"Введите рубли для элемента {i + 1} (неотрицательное число): ");
                    
                    bool tryToParse= false;
                    do
                    {
                        Console.WriteLine("Введите переменную целого значения");
                        tryToParse = int.TryParse(Console.ReadLine(), out rubles);
                    }
                    while (!tryToParse);
                    if (rubles < 0) Console.WriteLine("Рубли не могут быть отрицательными. Попробуйте снова.");
                } while (rubles < 0);

                
                do
                {
                    Console.WriteLine($"Введите копейки для элемента {i + 1} (от 0 до 99): ");
                    bool tryToParse = false;
                    do
                    {   
                        Console.WriteLine("Введите переменную целого значения");
                        tryToParse = int.TryParse(Console.ReadLine(), out kopeks);
                    }
                    while (!tryToParse);
                    if (kopeks < 0 || kopeks > 99) Console.WriteLine("Копейки должны быть от 0 до 99. Попробуйте снова.");
                } while (kopeks < 0 || kopeks > 99);

                arr[i] = new Money(rubles, kopeks);
            }
            objectCount += arr.Length;
        }

        public void Display()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"Элемент {i + 1}: ");
                arr[i].Display();
            }
        }

        public Money GetMin()
        {
            Money min = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if ((arr[i].Rubles < min.Rubles) || (arr[i].Rubles == min.Rubles && arr[i].Kopeks < min.Kopeks))
                {
                    min = arr[i];
                }
            }
            return min;
        }

        public Money this[int index]
        {
            get
            {
                if (index < 0 || index > arr.Length)
                    throw new ArgumentOutOfRangeException("Индекс выходит за пределы массива.");
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= arr.Length)
                    throw new ArgumentOutOfRangeException("Индекс выходит за пределы массива.");
                arr[index] = value;
            }
        }

        public static int GetObjectCount() => objectCount;
    }
}
