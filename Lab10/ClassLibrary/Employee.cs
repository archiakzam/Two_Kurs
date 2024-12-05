using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{   

    public class Employee : Persona
    {
        private string position = string.Empty;
        private double salary = 0;
        public string Position
        {
            get => position;
            set { position = value; }
        }
        public double Salary
        {
            get => salary;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Salary must be positive.");
                }
                salary = value;
            }
        }
        public Employee() : base() { }
        public Employee(string Name, int Age, string Gender,Address Address, string position, double salary) : base(Name, Age, Gender, Address)
        {
            Position = position;
            Salary = salary;
        }
        public Employee(Employee other) : base(other)
        {
            Position = other.Position;
            Salary = other.Salary;
        }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Position: {Position}, Salary: {Salary}");
        }
        
        public override void Init()
        {
            base.Init();
            Console.Write("Enter Position: ");
            Position = Console.ReadLine();
            Console.Write("Enter Salary: ");
            Salary = LibConvert.TryToConvertDouble();
        }
        public override void RandomInit()
        {
            base.RandomInit();
            var random = new Random();
            Position = $"Position_{random.Next(1, 5)}";
            Salary = random.Next(30000, 100000);
        }
        public override bool Equals(object obj)
        {
            if (obj is Engineer other)
            {
                return base.Equals(other) &&
                       Position == other.Position &&
                       Salary == other.Salary&&
                       Address.Equals(other.Address);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override Persona ShallowCopy()
        {
            return (Employee)this.MemberwiseClone();
        }
        public override object Clone()
        {
            Employee copy = new Employee(this.Name, this.Age, this.Gender, new Address(this.Address.City, this.Address.Street), this.Position, this.Salary);
            
            return copy;
        }
    }
}
