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
            Assert.AreEqual(size, moneyArray.Length, "����� ������� MoneyArray �� ��������� � ��������");
            Assert.IsNotNull(moneyArray.GetMin(), "����������� ������� �� ������ ���� null ��� ������� � ����������");
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
                moneyArray[i] = new Money(i + 1, i * 10); // ��������� �������
            }

            // Assert
            Assert.AreEqual(size, moneyArray.Length, "����� ������� MoneyArray �� ��������� � ��������");
            Assert.IsNotNull(moneyArray.GetMin(), "����������� ������� �� ������ ���� null ��� ������� ������������ �������");
        }

        [TestMethod]
        public void TestIncrementOperation()
        {
            // Arrange
            Money money = new Money(10, 50);

            // Act
            money++;

            // Assert
            Assert.AreEqual(11, money.Rubles, "�������� �������� ������ ����� ����������");
            Assert.AreEqual(50, money.Kopeks, "�������� ������ ������ �������� ��� ��������� ����� ����������");
        }

        [TestMethod]
        public void TestDecrementOperation()
        {
            // Arrange
            Money money = new Money(10, 0);

            // Act
            money--;

            // Assert
            Assert.AreEqual(9, money.Rubles, "�������� �������� ������ ����� ����������");
            Assert.AreEqual(100, money.Kopeks, "������� ������ ���� 100 ����� ���������� ��� ����������� ��������");
        }

        [TestMethod]
        public void TestExplicitConversionToInt()
        {
            // Arrange
            Money money = new Money(5, 75);

            // Act
            int rubles = (int)money;

            // Assert
            Assert.AreEqual(5, rubles, "���������� � int ������ ���������� ������ �����");
        }

        [TestMethod]
        public void TestImplicitConversionToDouble()
        {
            // Arrange
            Money money = new Money(5, 75);

            // Act
            double kopeks = money;

            // Assert
            Assert.AreEqual(575, kopeks, "���������� � double ������ ���������� ������ �������� � ��������");
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
            Assert.AreEqual(6, result.Rubles, "����� ����� ���������� ������ ������ ���� 6");
            Assert.AreEqual(0, result.Kopeks, "������� ������ ���� 0 ����� ���������� 25 ������ � 75 ��������");
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
            Assert.AreEqual(4, result.Rubles, "����� ����� ��������� 100 ������ ������ ���� 4");
            Assert.AreEqual(50, result.Kopeks, "������� ������ ���� 50 ����� ��������� 100 ������");
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
                Assert.AreEqual(5, result, "����� ReadElem ������ ���������� ��������� �����");
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
                Assert.AreEqual(5, result, "����� ReadElem ������ ���������� ������������ ���� � ��������� ��������� ���������� ��������");
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
            Assert.AreEqual(10, moneyArray.Length, "������ �� ��������� ������ ����� ����� 10.");
            foreach (var money in moneyArray)
            {
                Assert.IsNotNull(money, "������� ������� Money ������ ���� ���������������.");
                Assert.AreEqual(0, money.Rubles, "����� �� ��������� ������ ���� ����� 0.");
                Assert.AreEqual(0, money.Kopeks, "������� �� ��������� ������ ���� ����� 0.");
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
            Assert.AreEqual(size, moneyArray.Length, "����� ������� ������ ��������� � ���������� ���������.");
            foreach (var money in moneyArray)
            {
                Assert.IsNotNull(money, "������� ������� Money ������ ���� ���������������.");
                Assert.IsTrue(money.Rubles >= 0 && money.Rubles < 100, "����� ������ ���� � �������� 0-99.");
                Assert.IsTrue(money.Kopeks >= 0 && money.Kopeks < 100, "������� ������ ���� � �������� 0-99.");
            }
        }

        [TestMethod]
        public void Constructor_WithSizeManualInput_SetsArrayToSizeWithManualMoney()
        {
            // Arrange
            int size = 2;
            string input = "5\n30\n10\n45\n"; // ������� ������ ��� ������ � ������
            var inputReader = new System.IO.StringReader(input);
            Console.SetIn(inputReader);

            // Act
            MoneyArray moneyArray = new MoneyArray(size);

            // Assert
            Assert.AreEqual(size, moneyArray.Length, "����� ������� ������ ��������� � ���������� ���������.");
            Assert.AreEqual(5, moneyArray[0].Rubles, "�������� �������� ������ ��� ������� ��������.");
            Assert.AreEqual(30, moneyArray[0].Kopeks, "�������� �������� ������ ��� ������� ��������.");
            Assert.AreEqual(10, moneyArray[1].Rubles, "�������� �������� ������ ��� ������� ��������.");
            Assert.AreEqual(45, moneyArray[1].Kopeks, "�������� �������� ������ ��� ������� ��������.");
        }

        [TestMethod]
        public void Constructor_WithSizeManualInput_InvalidRubles_RepeatsUntilValid()
        {
            // Arrange
            int size = 1;
            string input = "-1\nabc\n20\n50\n"; // �������� ���� ��� ������ � ������
            var inputReader = new System.IO.StringReader(input);
            Console.SetIn(inputReader);

            // Act
            MoneyArray moneyArray = new MoneyArray(size);

            // Assert
            Assert.AreEqual(20, moneyArray[0].Rubles, "����� ������ ��������� � ���������� ��������� ���������.");
            Assert.AreEqual(50, moneyArray[0].Kopeks, "������� ������ ��������� � ���������� ��������� ���������.");
        }

        [TestMethod]
        public void Constructor_WithSizeManualInput_InvalidKopeks_RepeatsUntilValid()
        {
            // Arrange
            int size = 1;
            string input = "10\n-1\n200\n75\n"; // �������� ���� ��� ������
            var inputReader = new System.IO.StringReader(input);
            Console.SetIn(inputReader);

            // Act
            MoneyArray moneyArray = new MoneyArray(size);

            // Assert
            Assert.AreEqual(10, moneyArray[0].Rubles, "����� ������ ��������� � ��������� ���������.");
            Assert.AreEqual(75, moneyArray[0].Kopeks, "������� ������ ��������� � ���������� ��������� ���������.");
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
            Assert.AreEqual(initialCount + size, MoneyArray.GetObjectCount(), "���������� �������� Money ������ ������������� �� ������ �������.");
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
            Assert.AreEqual(initialCount + size, MoneyArray.GetObjectCount(), "���������� �������� Money ������ ������������� �� ������ �������.");
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
                string expectedOutput = "������� 1: 5 ���. 50 ���.\r\n������� 2: 2 ���. 30 ���.\r\n������� 3: 10 ���. 80 ���.\r\n";
                Assert.AreEqual(expectedOutput, sw.ToString(), "Display ������ ��������� �������� �������� �������.");
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
            Assert.AreEqual(2, minMoney.Rubles, "����� GetMin ������ ���������� ������� � ����������� ��������� ������.");
            Assert.AreEqual(30, minMoney.Kopeks, "����� GetMin ������ ���������� ������� � ����������� ��������� ������.");
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
            Assert.AreEqual(5, minMoney.Rubles, "����� GetMin ������ ��������� ������������ ������ � ����� ���������.");
            Assert.AreEqual(50, minMoney.Kopeks, "����� GetMin ������ ��������� ������������ ������ � ����� ���������.");
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
            Assert.AreEqual(2, result.Rubles, "���������� ������ ���������� ���������� �������� ��� ��������� �������.");
            Assert.AreEqual(30, result.Kopeks, "���������� ������ ���������� ���������� �������� ��� ��������� �������.");
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
            Assert.AreEqual(7, moneyArray[1].Rubles, "���������� ������ ��������� ������������� �������� �� �������.");
            Assert.AreEqual(25, moneyArray[1].Kopeks, "���������� ������ ��������� ������������� �������� �� �������.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_GetInvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);

            // Act
            var result = moneyArray[5]; // ��������� � ��������������� �������

            // Assert � �������� ����������
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_SetInvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var moneyArray = new MoneyArray(3);
            var newMoney = new Money(7, 25);

            // Act
            moneyArray[5] = newMoney; // ������� ���������� �������� �� ��������������� �������

            // Assert � �������� ����������
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
            Assert.AreEqual(initialCount + 8, finalCount, "����� GetObjectCount ������ ���������� ���������� ���������� ��������� �������� Money.");
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
            Assert.AreEqual(50, money.Rubles, "�������� Rubles ������ ��������� ����������� ������������� ��������.");
        }

        [TestMethod]
        public void Rubles_Property_Set_NegativeValue_SetsZero()
        {
            // Arrange
            var money = new Money();

            // Act
            money.Rubles = -5;

            // Assert
            Assert.AreEqual(0, money.Rubles, "�������� Rubles ������ ����������� �������� 0, ���� ���� ������� ������������� ��������.");
        }

        [TestMethod]
        public void Kopeks_Property_Set_ValidRange()
        {
            // Arrange
            var money = new Money();

            // Act
            money.Kopeks = 50;

            // Assert
            Assert.AreEqual(50, money.Kopeks, "�������� Kopeks ������ ��������� ����������� ��������, ����������� � ��������� �� 0 �� 99.");
        }

        [TestMethod]
        public void Kopeks_Property_Set_InvalidRange_SetsZero()
        {
            // Arrange
            var money = new Money();

            // Act
            money.Kopeks = 150;

            // Assert
            Assert.AreEqual(0, money.Kopeks, "�������� Kopeks ������ ����������� �������� 0, ���� ���� ������� �������� ��� ��������� �� 0 �� 99.");
        }

        [TestMethod]
        public void Constructor_SetsCorrectRublesAndKopeks()
        {
            // Arrange & Act
            var money = new Money(5, 150);

            // Assert
            Assert.AreEqual(6, money.Rubles, "����������� ������ ��������� ������������ �������� ������ � ������, ��������� ��.");
            Assert.AreEqual(50, money.Kopeks, "����������� ������ ��������� ������������ �������� ������ � ������, �������� ������� ��������.");
        }

        [TestMethod]
        public void DefaultConstructor_SetsZeroRublesAndKopeks()
        {
            // Arrange & Act
            var money = new Money();

            // Assert
            Assert.AreEqual(0, money.Rubles, "����������� �� ��������� ������ ����������� 0 ������.");
            Assert.AreEqual(0, money.Kopeks, "����������� �� ��������� ������ ����������� 0 ��������.");
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
            Assert.AreEqual(4, result.Rubles, "����� AddKopeks ������ ��������� ����������� ���������� ������.");
            Assert.AreEqual(25, result.Kopeks, "����� AddKopeks ������ ��������� ��������� ������� ������.");
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
                Assert.AreEqual(expectedOutput, sw.ToString(), "����� Display ������ ��������� �������� ���������� ������ � ������.");
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
            Assert.AreEqual(4, result.Rubles, "����� AddKopeks ������ ��������� ����������� ���������� ������.");
            Assert.AreEqual(25, result.Kopeks, "����� AddKopeks ������ ��������� ��������� ������� ������.");
        }

        [TestMethod]
        public void IncrementOperator_IncreasesKopeksByOne()
        {
            // Arrange
            var money = new Money(3, 99);

            // Act
            money++;

            // Assert
            Assert.AreEqual(4, money.Rubles, "�������� ���������� ������ ��������� ���������� ������� � �����.");
            Assert.AreEqual(0, money.Kopeks, "�������� ���������� ������ ��������� ������������� ������� � 0 ����� ��������.");
        }

        [TestMethod]
        public void DecrementOperator_DecreasesKopeksByOne()
        {
            // Arrange
            var money = new Money(3, 0);

            // Act
            money--;

            // Assert
            Assert.AreEqual(2, money.Rubles, "�������� ���������� ������ ��������� ����� �� 1, ���� ������� ���� ����� 0.");
            Assert.AreEqual(99, money.Kopeks, "�������� ���������� ������ ��������� ������������� ������� � 99, ���� ���� �������������.");
        }

        [TestMethod]
        public void DecrementOperator_DoesNotGoBelowZero()
        {
            // Arrange
            var money = new Money(0, 0);

            // Act
            money--;

            // Assert
            Assert.AreEqual(0, money.Rubles, "�������� ���������� �� ������ ��������� ������������� �������� ������.");
            Assert.AreEqual(0, money.Kopeks, "�������� ���������� �� ������ ��������� ������������� �������� ������.");
        }

        [TestMethod]
        public void ExplicitConversionToInt_ReturnsRublesOnly()
        {
            // Arrange
            var money = new Money(5, 50);

            // Act
            int rubles = (int)money;

            // Assert
            Assert.AreEqual(5, rubles, "����� ���������� � int ������ ���������� ������ �����.");
        }

        [TestMethod]
        public void ImplicitConversionToDouble_ReturnsKopeksAsFraction()
        {
            // Arrange
            var money = new Money(5, 50);

            // Act
            double kopeksFraction = money;

            // Assert
            Assert.AreEqual(0.5, kopeksFraction, 0.001, "������� ���������� � double ������ ���������� ������� � ���� �������� ��������.");
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
            Assert.AreEqual(3, result.Rubles, "�������� + ������ ��������� ��������� �����.");
            Assert.AreEqual(10, result.Kopeks, "�������� + ������ ��������� ��������� ������� � ������ ��������.");
        }
        [TestMethod]
        public void AdditionOperator_IntPlusMoney_AddsValueCorrectly()
        {
            // Arrange
            int additionalKopeks = 150; // 1 ����� � 50 ������
            Money money = new Money(3, 25);

            // Act
            Money result = additionalKopeks + money;

            // Assert
            Assert.AreEqual(4, result.Rubles, "����� ������ ��������� ������������� �� �������� �� ��������.");
            Assert.AreEqual(75, result.Kopeks, "������� ������ ��������� ��������� ������� ��� ��������.");
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
            Assert.AreEqual(4, result.Rubles, "����� ������ ��������� ����������� ��� ���������.");
            Assert.AreEqual(75, result.Kopeks, "������� ������ ��������� ��������� ������� ��� ���������.");
        }

        [TestMethod]
        public void SubtractionOperator_MoneyMinusInt_ResultBelowZero_ReturnsZeroMoney()
        {
            // Arrange
            Money money = new Money(1, 20);
            int subtractedKopeks = 150; // ������, ��� ������� ���������� �����

            // Act
            Money result = money - subtractedKopeks;

            // Assert
            Assert.AreEqual(0, result.Rubles, "���� ��������� �������������, ����� ������ ���� ����������� � 0.");
            Assert.AreEqual(0, result.Kopeks, "���� ��������� �������������, ������� ������ ���� ����������� � 0.");
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
            Assert.AreEqual(6, result.Rubles, "����� ������ ��������� ����������� ��� ��������� �������� ������� Money.");
            Assert.AreEqual(50, result.Kopeks, "������� ������ ��������� �������������� ��� ���������.");
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
            Assert.AreEqual(0, result.Rubles, "���� ��������� �������������, ����� ������ ���� ����������� � 0.");
            Assert.AreEqual(0, result.Kopeks, "���� ��������� �������������, ������� ������ ���� ����������� � 0.");
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
            Assert.AreEqual(initialCount + 2, finalCount, "������� �������� ������ ������������� ��� �������� ������� ������ ������� Money.");
        }
    }
}
