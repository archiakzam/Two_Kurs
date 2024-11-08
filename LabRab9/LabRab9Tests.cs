using System;
using LabRab9;


namespace LabRab9.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private static Random rnd = new Random();

        [TestMethod]
        public void TestMoneyArrayCreationAutomatic()
        {
            // Arrange
            int size = 5;

            // Act
            MoneyArray moneyArray = new MoneyArray(size, rnd);

            // Assert
            Assert.AreEqual(size, moneyArray.Length, "Длина массива MoneyArray не совпадает с заданной");
            Assert.IsNotNull(moneyArray.GetMin(), "Минимальный элемент не должен быть null для массива с элементами");
        }

        [TestMethod]
        public void TestMoneyArrayCreationManual()
        {
            // Arrange
            int size = 3;
            MoneyArray moneyArray = new MoneyArray(size);

            // Act
            for (int i = 0; i < size; i++)
            {
                moneyArray[i] = new Money(i + 1, i * 10); // Заполняем вручную
            }

            // Assert
            Assert.AreEqual(size, moneyArray.Length, "Длина массива MoneyArray не совпадает с заданной");
            Assert.IsNotNull(moneyArray.GetMin(), "Минимальный элемент не должен быть null для вручную заполненного массива");
        }

        [TestMethod]
        public void TestIncrementOperation()
        {
            // Arrange
            Money money = new Money(10, 50);

            // Act
            money++;

            // Assert
            Assert.AreEqual(11, money.Rubles, "Неверное значение рублей после инкремента");
            Assert.AreEqual(50, money.Kopeks, "Значение копеек должно остаться без изменений после инкремента");
        }

        [TestMethod]
        public void TestDecrementOperation()
        {
            // Arrange
            Money money = new Money(10, 0);

            // Act
            money--;

            // Assert
            Assert.AreEqual(9, money.Rubles, "Неверное значение рублей после декремента");
            Assert.AreEqual(100, money.Kopeks, "Копейки должны быть 100 после декремента для корректного значения");
        }

        [TestMethod]
        public void TestExplicitConversionToInt()
        {
            // Arrange
            Money money = new Money(5, 75);

            // Act
            int rubles = (int)money;

            // Assert
            Assert.AreEqual(5, rubles, "Приведение к int должно возвращать только рубли");
        }

        [TestMethod]
        public void TestImplicitConversionToDouble()
        {
            // Arrange
            Money money = new Money(5, 75);

            // Act
            double kopeks = money;

            // Assert
            Assert.AreEqual(575, kopeks, "Приведение к double должно возвращать полное значение в копейках");
        }

        [TestMethod]
        public void TestBinaryOperationAddition()
        {
            // Arrange
            Money money = new Money(5, 75);
            int addKopeks = 25;

            // Act
            Money result = money + addKopeks;

            // Assert
            Assert.AreEqual(6, result.Rubles, "Рубли после добавления копеек должны быть 6");
            Assert.AreEqual(0, result.Kopeks, "Копейки должны быть 0 после добавления 25 копеек к 75 копейкам");
        }

        [TestMethod]
        public void TestBinaryOperationSubtraction()
        {
            // Arrange
            Money money = new Money(5, 50);
            int subKopeks = 100;

            // Act
            Money result = money - subKopeks;

            // Assert
            Assert.AreEqual(4, result.Rubles, "Рубли после вычитания 100 копеек должны быть 4");
            Assert.AreEqual(50, result.Kopeks, "Копейки должны быть 50 после вычитания 100 копеек");
        }

        [TestMethod]
        public void TestInterfaceReadElem_ValidInput()
        {
            // Act
            using (var reader = new System.IO.StringReader("5"))
            {
                Console.SetIn(reader);
                int result = Interface.ReadElem();

                // Assert
                Assert.AreEqual(5, result, "Метод ReadElem должен возвращать введенное число");
            }
        }

        [TestMethod]
        public void TestInterfaceReadElem_InvalidInput()
        {
            // Act
            using (var reader = new System.IO.StringReader("abc\n5"))
            {
                Console.SetIn(reader);
                int result = Interface.ReadElem();

                // Assert
                Assert.AreEqual(5, result, "Метод ReadElem должен пропускать некорректный ввод и принимать следующее корректное значение");
            }
        }

    }
    [TestClass]
    public class MoneyArrayTests
    {
        private static Random rnd = new Random();

        [TestMethod]
        public void Constructor_Default_SetsArrayToSize10WithDefaultMoney()
        {
            // Act
            MoneyArray moneyArray = new MoneyArray();

            // Assert
            Assert.AreEqual(10, moneyArray.Length, "Массив по умолчанию должен иметь длину 10.");
            foreach (var money in moneyArray)
            {
                Assert.IsNotNull(money, "Элемент массива Money должен быть инициализирован.");
                Assert.AreEqual(0, money.Rubles, "Рубли по умолчанию должны быть равны 0.");
                Assert.AreEqual(0, money.Kopeks, "Копейки по умолчанию должны быть равны 0.");
            }
        }

        [TestMethod]
        public void Constructor_WithSizeAndRandom_SetsArrayToSizeWithRandomMoney()
        {
            // Arrange
            int size = 5;

            // Act
            MoneyArray moneyArray = new MoneyArray(size, rnd);

            // Assert
            Assert.AreEqual(size, moneyArray.Length, "Длина массива должна совпадать с переданным значением.");
            foreach (var money in moneyArray)
            {
                Assert.IsNotNull(money, "Элемент массива Money должен быть инициализирован.");
                Assert.IsTrue(money.Rubles >= 0 && money.Rubles < 100, "Рубли должны быть в пределах 0-99.");
                Assert.IsTrue(money.Kopeks >= 0 && money.Kopeks < 100, "Копейки должны быть в пределах 0-99.");
            }
        }

        [TestMethod]
        public void Constructor_WithSizeManualInput_SetsArrayToSizeWithManualMoney()
        {
            // Arrange
            int size = 2;
            string input = "5\n30\n10\n45\n"; // Входные данные для рублей и копеек
            var inputReader = new System.IO.StringReader(input);
            Console.SetIn(inputReader);

            // Act
            MoneyArray moneyArray = new MoneyArray(size);

            // Assert
            Assert.AreEqual(size, moneyArray.Length, "Длина массива должна совпадать с переданным значением.");
            Assert.AreEqual(5, moneyArray[0].Rubles, "Неверное значение рублей для первого элемента.");
            Assert.AreEqual(30, moneyArray[0].Kopeks, "Неверное значение копеек для первого элемента.");
            Assert.AreEqual(10, moneyArray[1].Rubles, "Неверное значение рублей для второго элемента.");
            Assert.AreEqual(45, moneyArray[1].Kopeks, "Неверное значение копеек для второго элемента.");
        }

        [TestMethod]
        public void Constructor_WithSizeManualInput_InvalidRubles_RepeatsUntilValid()
        {
            // Arrange
            int size = 1;
            string input = "-1\nabc\n20\n50\n"; // Неверный ввод для рублей и копеек
            var inputReader = new System.IO.StringReader(input);
            Console.SetIn(inputReader);

            // Act
            MoneyArray moneyArray = new MoneyArray(size);

            // Assert
            Assert.AreEqual(20, moneyArray[0].Rubles, "Рубли должны совпадать с корректным введенным значением.");
            Assert.AreEqual(50, moneyArray[0].Kopeks, "Копейки должны совпадать с корректным введенным значением.");
        }

        [TestMethod]
        public void Constructor_WithSizeManualInput_InvalidKopeks_RepeatsUntilValid()
        {
            // Arrange
            int size = 1;
            string input = "10\n-1\n200\n75\n"; // Неверный ввод для копеек
            var inputReader = new System.IO.StringReader(input);
            Console.SetIn(inputReader);

            // Act
            MoneyArray moneyArray = new MoneyArray(size);

            // Assert
            Assert.AreEqual(10, moneyArray[0].Rubles, "Рубли должны совпадать с введенным значением.");
            Assert.AreEqual(75, moneyArray[0].Kopeks, "Копейки должны совпадать с корректным введенным значением.");
        }

        [TestMethod]
        public void Constructor_WithSizeAndRandom_IncrementsObjectCount()
        {
            // Arrange
            int initialCount = MoneyArray.GetObjectCount();
            int size = 5;

            // Act
            MoneyArray moneyArray = new MoneyArray(size, rnd);

            // Assert
            Assert.AreEqual(initialCount + size, MoneyArray.GetObjectCount(), "Количество объектов Money должно увеличиваться на размер массива.");
        }

        [TestMethod]
        public void Constructor_WithSizeManualInput_IncrementsObjectCount()
        {
            // Arrange
            int initialCount = MoneyArray.GetObjectCount();
            int size = 3;
            string input = "5\n10\n15\n20\n25\n30\n";
            var inputReader = new System.IO.StringReader(input);
            Console.SetIn(inputReader);

            // Act
            MoneyArray moneyArray = new MoneyArray(size);

            // Assert
            Assert.AreEqual(initialCount + size, MoneyArray.GetObjectCount(), "Количество объектов Money должно увеличиваться на размер массива.");
        }
        [TestMethod]
        public void Display_PrintsAllElementsToConsole()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);
            moneyArray[0] = new Money(5, 50);
            moneyArray[1] = new Money(2, 30);
            moneyArray[2] = new Money(10, 80);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                moneyArray.Display();

                // Assert
                string expectedOutput = "Элемент 1: 5 руб. 50 коп.\r\nЭлемент 2: 2 руб. 30 коп.\r\nЭлемент 3: 10 руб. 80 коп.\r\n";
                Assert.AreEqual(expectedOutput, sw.ToString(), "Display должен корректно выводить элементы массива.");
            }
        }

        [TestMethod]
        public void GetMin_ReturnsElementWithMinimumValue()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);
            moneyArray[0] = new Money(5, 50);
            moneyArray[1] = new Money(2, 30);
            moneyArray[2] = new Money(10, 80);

            // Act
            Money minMoney = moneyArray.GetMin();

            // Assert
            Assert.AreEqual(2, minMoney.Rubles, "Метод GetMin должен возвращать элемент с минимальным значением рублей.");
            Assert.AreEqual(30, minMoney.Kopeks, "Метод GetMin должен возвращать элемент с минимальным значением копеек.");
        }

        [TestMethod]
        public void GetMin_HandlesSingleElementArray()
        {
            // Arrange
            var moneyArray = new MoneyArray(1);
            moneyArray[0] = new Money(5, 50);

            // Act
            Money minMoney = moneyArray.GetMin();

            // Assert
            Assert.AreEqual(5, minMoney.Rubles, "Метод GetMin должен корректно обрабатывать массив с одним элементом.");
            Assert.AreEqual(50, minMoney.Kopeks, "Метод GetMin должен корректно обрабатывать массив с одним элементом.");
        }

        [TestMethod]
        public void Indexer_GetValidIndex_ReturnsCorrectElement()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);
            moneyArray[0] = new Money(5, 50);
            moneyArray[1] = new Money(2, 30);
            moneyArray[2] = new Money(10, 80);

            // Act
            Money result = moneyArray[1];

            // Assert
            Assert.AreEqual(2, result.Rubles, "Индексатор должен возвращать корректное значение для валидного индекса.");
            Assert.AreEqual(30, result.Kopeks, "Индексатор должен возвращать корректное значение для валидного индекса.");
        }

        [TestMethod]
        public void Indexer_SetValidIndex_SetsCorrectElement()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);
            var newMoney = new Money(7, 25);

            // Act
            moneyArray[1] = newMoney;

            // Assert
            Assert.AreEqual(7, moneyArray[1].Rubles, "Индексатор должен корректно устанавливать значение по индексу.");
            Assert.AreEqual(25, moneyArray[1].Kopeks, "Индексатор должен корректно устанавливать значение по индексу.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_GetInvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);

            // Act
            var result = moneyArray[5]; // Обращение к несуществующему индексу

            // Assert — проверка исключения
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_SetInvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);
            var newMoney = new Money(7, 25);

            // Act
            moneyArray[5] = newMoney; // Попытка установить значение по несуществующему индексу

            // Assert — проверка исключения
        }

        [TestMethod]
        public void GetObjectCount_ReturnsCorrectCountAfterArrayCreation()
        {
            // Arrange
            int initialCount = MoneyArray.GetObjectCount();
            var moneyArray1 = new MoneyArray(3);
            var moneyArray2 = new MoneyArray(5);

            // Act
            int finalCount = MoneyArray.GetObjectCount();

            // Assert
            Assert.AreEqual(initialCount + 8, finalCount, "Метод GetObjectCount должен возвращать корректное количество созданных объектов Money.");
        }
    }
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void Rubles_Property_Set_PositiveValue()
        {
            // Arrange
            var money = new Money();

            // Act
            money.Rubles = 50;

            // Assert
            Assert.AreEqual(50, money.Rubles, "Свойство Rubles должно корректно присваивать положительное значение.");
        }

        [TestMethod]
        public void Rubles_Property_Set_NegativeValue_SetsZero()
        {
            // Arrange
            var money = new Money();

            // Act
            money.Rubles = -5;

            // Assert
            Assert.AreEqual(0, money.Rubles, "Свойство Rubles должно присваивать значение 0, если было указано отрицательное значение.");
        }

        [TestMethod]
        public void Kopeks_Property_Set_ValidRange()
        {
            // Arrange
            var money = new Money();

            // Act
            money.Kopeks = 50;

            // Assert
            Assert.AreEqual(50, money.Kopeks, "Свойство Kopeks должно корректно присваивать значение, находящееся в диапазоне от 0 до 99.");
        }

        [TestMethod]
        public void Kopeks_Property_Set_InvalidRange_SetsZero()
        {
            // Arrange
            var money = new Money();

            // Act
            money.Kopeks = 150;

            // Assert
            Assert.AreEqual(0, money.Kopeks, "Свойство Kopeks должно присваивать значение 0, если было указано значение вне диапазона от 0 до 99.");
        }

        [TestMethod]
        public void Constructor_SetsCorrectRublesAndKopeks()
        {
            // Arrange & Act
            var money = new Money(5, 150);

            // Assert
            Assert.AreEqual(6, money.Rubles, "Конструктор должен корректно обрабатывать значения рублей и копеек, складывая их.");
            Assert.AreEqual(50, money.Kopeks, "Конструктор должен корректно обрабатывать значения рублей и копеек, учитывая перенос значений.");
        }

        [TestMethod]
        public void DefaultConstructor_SetsZeroRublesAndKopeks()
        {
            // Arrange & Act
            var money = new Money();

            // Assert
            Assert.AreEqual(0, money.Rubles, "Конструктор по умолчанию должен присваивать 0 рублям.");
            Assert.AreEqual(0, money.Kopeks, "Конструктор по умолчанию должен присваивать 0 копейкам.");
        }

        [TestMethod]
        public void AddKopeks_CorrectlyAddsKopeks()
        {
            // Arrange
            var money = new Money(3, 50);
            int additionalKopeks = 75;

            // Act
            var result = Money.AddKopeks(money, additionalKopeks);

            // Assert
            Assert.AreEqual(4, result.Rubles, "Метод AddKopeks должен корректно увеличивать количество рублей.");
            Assert.AreEqual(25, result.Kopeks, "Метод AddKopeks должен корректно учитывать перенос копеек.");
        }

        [TestMethod]
        public void Display_PrintsCorrectOutput()
        {
            // Arrange
            var money = new Money(5, 50);
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                money.Display();

                // Assert
                string expectedOutput = "Money: 5 rubles, 50 kopeks\r\n";
                Assert.AreEqual(expectedOutput, sw.ToString(), "Метод Display должен корректно выводить количество рублей и копеек.");
            }
        }
        [TestMethod]
        public void AddKopeks_AddsKopeks_Correctly()
        {
            // Arrange
            var money = new Money(3, 50);

            // Act
            var result = money.AddKopeks(75);

            // Assert
            Assert.AreEqual(4, result.Rubles, "Метод AddKopeks должен корректно увеличивать количество рублей.");
            Assert.AreEqual(25, result.Kopeks, "Метод AddKopeks должен корректно учитывать перенос копеек.");
        }

        [TestMethod]
        public void IncrementOperator_IncreasesKopeksByOne()
        {
            // Arrange
            var money = new Money(3, 99);

            // Act
            money++;

            // Assert
            Assert.AreEqual(4, money.Rubles, "Оператор инкремента должен корректно переносить копейки в рубли.");
            Assert.AreEqual(0, money.Kopeks, "Оператор инкремента должен корректно устанавливать копейки в 0 после переноса.");
        }

        [TestMethod]
        public void DecrementOperator_DecreasesKopeksByOne()
        {
            // Arrange
            var money = new Money(3, 0);

            // Act
            money--;

            // Assert
            Assert.AreEqual(2, money.Rubles, "Оператор декремента должен уменьшать рубли на 1, если копейки были равны 0.");
            Assert.AreEqual(99, money.Kopeks, "Оператор декремента должен корректно устанавливать копейки в 99, если было заимствование.");
        }

        [TestMethod]
        public void DecrementOperator_DoesNotGoBelowZero()
        {
            // Arrange
            var money = new Money(0, 0);

            // Act
            money--;

            // Assert
            Assert.AreEqual(0, money.Rubles, "Оператор декремента не должен допускать отрицательных значений рублей.");
            Assert.AreEqual(0, money.Kopeks, "Оператор декремента не должен допускать отрицательных значений копеек.");
        }

        [TestMethod]
        public void ExplicitConversionToInt_ReturnsRublesOnly()
        {
            // Arrange
            var money = new Money(5, 50);

            // Act
            int rubles = (int)money;

            // Assert
            Assert.AreEqual(5, rubles, "Явное приведение к int должно возвращать только рубли.");
        }

        [TestMethod]
        public void ImplicitConversionToDouble_ReturnsKopeksAsFraction()
        {
            // Arrange
            var money = new Money(5, 50);

            // Act
            double kopeksFraction = money;

            // Assert
            Assert.AreEqual(0.5, kopeksFraction, 0.001, "Неявное приведение к double должно возвращать копейки в виде дробного значения.");
        }

        [TestMethod]
        public void AdditionOperator_AddsKopeksCorrectly()
        {
            // Arrange
            var money = new Money(2, 30);
            int additionalKopeks = 80;

            // Act
            var result = money + additionalKopeks;

            // Assert
            Assert.AreEqual(3, result.Rubles, "Оператор + должен корректно добавлять рубли.");
            Assert.AreEqual(10, result.Kopeks, "Оператор + должен корректно добавлять копейки с учетом переноса.");
        }
        [TestMethod]
        public void AdditionOperator_IntPlusMoney_AddsValueCorrectly()
        {
            // Arrange
            int additionalKopeks = 150; // 1 рубль и 50 копеек
            Money money = new Money(3, 25);

            // Act
            Money result = additionalKopeks + money;

            // Assert
            Assert.AreEqual(4, result.Rubles, "Рубли должны корректно увеличиваться на значение от сложения.");
            Assert.AreEqual(75, result.Kopeks, "Копейки должны корректно учитывать перенос при сложении.");
        }

        [TestMethod]
        public void SubtractionOperator_MoneyMinusInt_SubtractsValueCorrectly()
        {
            // Arrange
            Money money = new Money(5, 50);
            int subtractedKopeks = 75;

            // Act
            Money result = money - subtractedKopeks;

            // Assert
            Assert.AreEqual(4, result.Rubles, "Рубли должны корректно уменьшаться при вычитании.");
            Assert.AreEqual(75, result.Kopeks, "Копейки должны корректно учитывать перенос при вычитании.");
        }

        [TestMethod]
        public void SubtractionOperator_MoneyMinusInt_ResultBelowZero_ReturnsZeroMoney()
        {
            // Arrange
            Money money = new Money(1, 20);
            int subtractedKopeks = 150; // Больше, чем текущее количество денег

            // Act
            Money result = money - subtractedKopeks;

            // Assert
            Assert.AreEqual(0, result.Rubles, "Если результат отрицательный, рубли должны быть установлены в 0.");
            Assert.AreEqual(0, result.Kopeks, "Если результат отрицательный, копейки должны быть установлены в 0.");
        }

        [TestMethod]
        public void SubtractionOperator_IntMinusMoney_SubtractsMoneyCorrectly()
        {
            // Arrange
            int rublesToSubtract = 10;
            Money money = new Money(3, 50);

            // Act
            Money result = rublesToSubtract - money;

            // Assert
            Assert.AreEqual(6, result.Rubles, "Рубли должны корректно уменьшаться при вычитании значения объекта Money.");
            Assert.AreEqual(50, result.Kopeks, "Копейки должны корректно рассчитываться при вычитании.");
        }

        [TestMethod]
        public void SubtractionOperator_IntMinusMoney_ResultBelowZero_ReturnsZeroMoney()
        {
            // Arrange
            int rublesToSubtract = 2;
            Money money = new Money(5, 0);

            // Act
            Money result = rublesToSubtract - money;

            // Assert
            Assert.AreEqual(0, result.Rubles, "Если результат отрицательный, рубли должны быть установлены в 0.");
            Assert.AreEqual(0, result.Kopeks, "Если результат отрицательный, копейки должны быть установлены в 0.");
        }

        [TestMethod]
        public void ObjectCount_ReturnsCorrectCount()
        {
            // Arrange
            int initialCount = Money.ObjectCount;
            var money1 = new Money(1, 10);
            var money2 = new Money(2, 20);

            // Act
            int finalCount = Money.ObjectCount;

            // Assert
            Assert.AreEqual(initialCount + 2, finalCount, "Счетчик объектов должен увеличиваться при создании каждого нового объекта Money.");
        }
    }
}
