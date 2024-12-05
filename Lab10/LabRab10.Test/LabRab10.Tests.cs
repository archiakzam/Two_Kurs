using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibrary;

namespace LabRab10.Tests
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            var address = new Address("New York", "5th Avenue");
            Assert.AreEqual("New York", address.City);
            Assert.AreEqual("5th Avenue", address.Street);
        }

        [TestMethod]
        public void ToString_ShouldReturnCorrectFormat()
        {
            var address = new Address("New York", "5th Avenue");
            Assert.AreEqual("City: New York, Street: 5th Avenue", address.ToString());
        }
    }

    [TestClass]
    public class PersonaTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeDefaultValues()
        {
            var persona = new Persona();
            Assert.AreEqual("Unknown City", persona.Address.City);
            Assert.AreEqual("Unknown Street", persona.Address.Street);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Age_SetInvalidValue_ShouldThrowException()
        {
            var persona = new Persona();
            persona.Age = 17;
        }

        [TestMethod]
        public void Gender_SetValidValue_ShouldSucceed()
        {
            var persona = new Persona();
            persona.Gender = "Male";
            Assert.AreEqual("Male", persona.Gender);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Gender_SetInvalidValue_ShouldThrowException()
        {
            var persona = new Persona();
            persona.Gender = "Invalid";
        }

        [TestMethod]
        public void Clone_ShouldCreateDeepCopy()
        {
            var address = new Address("New York", "5th Avenue");
            var persona = new Persona("John", 30, "Male", address);

            var clone = (Persona)persona.Clone();

            Assert.AreEqual(persona.Name, clone.Name);
            Assert.AreEqual(persona.Age, clone.Age);
            Assert.AreEqual(persona.Gender, clone.Gender);
            Assert.AreEqual(persona.Address.City, clone.Address.City);
            Assert.AreEqual(persona.Address.Street, clone.Address.Street);
            Assert.AreNotSame(persona.Address, clone.Address); // Deep copy check
        }
    }

    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            var address = new Address("New York", "5th Avenue");
            var employee = new Employee("John", 30, "Male", address, "Manager", 50000);

            Assert.AreEqual("John", employee.Name);
            Assert.AreEqual(30, employee.Age);
            Assert.AreEqual("Male", employee.Gender);
            Assert.AreEqual("Manager", employee.Position);
            Assert.AreEqual(50000, employee.Salary);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Salary_SetNegativeValue_ShouldThrowException()
        {
            var employee = new Employee();
            employee.Salary = -1;
        }

        [TestMethod]
        public void Clone_ShouldCreateDeepCopy()
        {
            var address = new Address("New York", "5th Avenue");
            var employee = new Employee("John", 30, "Male", address, "Manager", 50000);

            var clone = (Employee)employee.Clone();

            Assert.AreEqual(employee.Name, clone.Name);
            Assert.AreEqual(employee.Age, clone.Age);
            Assert.AreEqual(employee.Gender, clone.Gender);
            Assert.AreEqual(employee.Position, clone.Position);
            Assert.AreEqual(employee.Salary, clone.Salary);
            Assert.AreNotSame(employee.Address, clone.Address); // Deep copy check
        }
    }

    [TestClass]
    public class EngineerTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            var address = new Address("New York", "5th Avenue");
            var engineer = new Engineer("John", 30, "Male", address, "Engineer", 70000, 5, "Workshop1", "Electrical", "Expert");

            Assert.AreEqual("John", engineer.Name);
            Assert.AreEqual(30, engineer.Age);
            Assert.AreEqual("Male", engineer.Gender);
            Assert.AreEqual("Engineer", engineer.Position);
            Assert.AreEqual(70000, engineer.Salary);
            Assert.AreEqual("Electrical", engineer.Specialization);
            Assert.AreEqual("Expert", engineer.Qualification);
        }

        [TestMethod]
        public void RandomInit_ShouldSetRandomValues()
        {
            var engineer = new Engineer();
            engineer.RandomInit();

            Assert.IsNotNull(engineer.Name);
            Assert.IsTrue(engineer.Age >= 18 && engineer.Age <= 65);
            Assert.IsTrue(engineer.Gender == "Male" || engineer.Gender == "Female");
            Assert.IsNotNull(engineer.Address.City);
            Assert.IsNotNull(engineer.Address.Street);
            Assert.IsNotNull(engineer.Specialization);
            Assert.IsNotNull(engineer.Qualification);
        }

        [TestMethod]
        public void Clone_ShouldCreateDeepCopy()
        {
            var address = new Address("New York", "5th Avenue");
            var engineer = new Engineer("John", 30, "Male", address, "Engineer", 70000, 5, "Workshop1", "Electrical", "Expert");

            var clone = (Engineer)engineer.Clone();

            Assert.AreEqual(engineer.Name, clone.Name);
            Assert.AreEqual(engineer.Age, clone.Age);
            Assert.AreEqual(engineer.Gender, clone.Gender);
            Assert.AreEqual(engineer.Position, clone.Position);
            Assert.AreEqual(engineer.Salary, clone.Salary);
            Assert.AreEqual(engineer.Specialization, clone.Specialization);
            Assert.AreEqual(engineer.Qualification, clone.Qualification);
            Assert.AreNotSame(engineer.Address, clone.Address); // Deep copy check
        }
    }
    [TestClass]
    public class WorkerTests
    {
        [TestMethod]
        public void Worker_DefaultConstructor_PropertiesInitialized()
        {
            // Arrange & Act
            var worker = new Worker();

            // Assert
            Assert.AreEqual(0, worker.Experience);
            Assert.AreEqual(string.Empty, worker.Workshop);
        }

       

        [TestMethod]
        public void Worker_SetExperience_NegativeValue_ThrowsException()
        {
            // Arrange
            var worker = new Worker();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => worker.Experience = -1);
        }

        



        [TestMethod]
        public void Worker_Equals_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var worker1 = new Worker("John", 30, "Male", new Address("New York", "Wall Street"), "Engineer", 50000, 5, "Workshop A");
            var worker2 = new Worker("Jane", 28, "Female", new Address("San Francisco", "Market Street"), "Manager", 70000, 3, "Workshop B");

            // Act
            var areEqual = worker1.Equals(worker2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Worker_Show_DisplaysProperties()
        {
            // Arrange
            var worker = new Worker("John", 30, "Male", new Address("New York", "Wall Street"), "Engineer", 50000, 5, "Workshop A");

            // Act & Assert
            worker.Show();
        }
        [TestMethod]
        public void Vehicle_DefaultConstructor_InitializesProperties()
        {
            // Arrange & Act
            var vehicle = new Vehicle();

            // Assert
            Assert.AreEqual(0, vehicle.Speed);
            Assert.AreEqual(string.Empty, vehicle.Name);
        }

        [TestMethod]
        public void Vehicle_ParameterizedConstructor_InitializesProperties()
        {
            // Arrange & Act
            var vehicle = new Vehicle("Car", 120);

            // Assert
            Assert.AreEqual("Car", vehicle.Name);
            Assert.AreEqual(120, vehicle.Speed);
        }

        [TestMethod]
        public void Vehicle_SetSpeed_NegativeValue_ThrowsException()
        {
            // Arrange
            var vehicle = new Vehicle();

            // Act & Assert
            Assert.ThrowsException<Exception>(() => vehicle.Speed = -1);
        }

        [TestMethod]
        public void Vehicle_RandomInit_PropertiesSet()
        {
            // Arrange
            var vehicle = new Vehicle();

            // Act
            vehicle.RandomInit();

            // Assert
            Assert.IsTrue(vehicle.Speed >= 60 && vehicle.Speed <= 220);
            Assert.IsFalse(string.IsNullOrWhiteSpace(vehicle.Name));
        }

        [TestMethod]
        public void Vehicle_Clone_CreatesDeepCopy()
        {
            // Arrange
            var vehicle = new Vehicle("Car", 120);

            // Act
            var clone = (Vehicle)vehicle.Clone();

            // Assert
            Assert.AreEqual(vehicle, clone);
            Assert.AreNotSame(vehicle, clone);
        }
        [TestClass]
        public class LibConvertTests
        {
            [TestMethod]
            public void TryToConvertInt_ValidInput_ReturnsInteger()
            {
                // Arrange
                using var input = new StringReader("42\n");
                Console.SetIn(input);

                // Act
                var result = LibConvert.TryToConvertInt();

                // Assert
                Assert.AreEqual(42, result);
            }

            [TestMethod]
            public void TryToConvertInt_InvalidInput_RepromptsUntilValid()
            {
                // Arrange
                using var input = new StringReader("abc\n123\n");
                Console.SetIn(input);

                // Act
                var result = LibConvert.TryToConvertInt();

                // Assert
                Assert.AreEqual(123, result);
            }

           

            [TestMethod]
            public void TryToConvertIntMoreZero_ValidPositiveInput_ReturnsInteger()
            {
                // Arrange
                using var input = new StringReader("5\n");
                Console.SetIn(input);

                // Act
                var result = LibConvert.TryToConvertIntMoreZero();

                // Assert
                Assert.AreEqual(5, result);
            }

            
        }
    }


}
