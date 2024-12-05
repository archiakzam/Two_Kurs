using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class LibConvert
    {
        public static int TryToConvertInt()
        {
            int outputInt = 0;
            bool tryToConvert = false;
            int attempts = 0; // Счетчик попыток
            const int maxAttempts = 10;

            while (!tryToConvert && attempts < maxAttempts)
            {
                Console.WriteLine("Enter value");
                tryToConvert = int.TryParse(Console.ReadLine(), out outputInt);
                attempts++;
            }

            if (!tryToConvert)
            {
                throw new InvalidOperationException("Failed to convert input to integer after multiple attempts.");
            }

            return outputInt;
        }

        public static double TryToConvertDouble()
        {
            double outputDouble = 0;
            bool tryToConvert = false;
            int attempts = 0; // Счетчик попыток
            const int maxAttempts = 10;

            while (!tryToConvert && attempts < maxAttempts)
            {
                Console.WriteLine("Enter value");
                tryToConvert = double.TryParse(Console.ReadLine(), out outputDouble);
                attempts++;
            }

            if (!tryToConvert)
            {
                throw new InvalidOperationException("Failed to convert input to double after multiple attempts.");
            }

            return outputDouble;
        }

        public static int TryToConvertIntMoreZero()
        {
            int outputInt = 0;
            bool tryToConvert = false;
            int attempts = 0; // Счетчик попыток
            const int maxAttempts = 10;

            while ((!tryToConvert || outputInt < 0) && attempts < maxAttempts)
            {
                Console.WriteLine("Enter value");
                tryToConvert = int.TryParse(Console.ReadLine(), out outputInt);
                attempts++;
            }

            if (!tryToConvert || outputInt < 0)
            {
                throw new InvalidOperationException("Failed to convert input to positive integer after multiple attempts.");
            }

            return outputInt;
        }

        public static double TryToConvertDoublMoreZero()
        {
            double outputDouble = 0;
            bool tryToConvert = false;
            int attempts = 0; // Счетчик попыток
            const int maxAttempts = 10;

            while ((!tryToConvert || outputDouble < 0) && attempts < maxAttempts)
            {
                Console.WriteLine("Enter value");
                tryToConvert = double.TryParse(Console.ReadLine(), out outputDouble);
                attempts++;
            }

            if (!tryToConvert || outputDouble < 0)
            {
                throw new InvalidOperationException("Failed to convert input to positive double after multiple attempts.");
            }

            return outputDouble;
        }

    }
}
