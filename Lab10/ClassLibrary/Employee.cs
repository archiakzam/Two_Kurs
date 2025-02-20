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
        public void ShowNonVirtual()
        {
            Console.WriteLine($"Perosna name: {Name}, age: {Age}, gender: {Gender}, address: {Address}");
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
            if (obj is Employee other)
            {
                return BasePersona.Equals(other.BasePersona) && Position == other.Position && Salary == other.Salary;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BasePersona, Position, Salary);
        }
        public override Persona ShallowCopy()
        {
            return (Employee)this.MemberwiseClone();
        }
        public Persona BasePersona
        {
            get
            {
                return new Persona(this.Name, this.Age, this.Gender, this.Address);
            }
        }
        public override object Clone()
        {
            Employee copy = new Employee(this.Name, this.Age, this.Gender, new Address(this.Address.City, this.Address.Street), this.Position, this.Salary);
            
            return copy;
        }
    }
}
