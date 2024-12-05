using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Worker : Employee
    {
        private int experience;
        private string workshop= string.Empty;
        public int Experience
        {
            get => experience;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Experience must be non-negative.");
                }
                experience = value;
            }
        }
        public string Workshop { get=>workshop; set { workshop = value; } }

        public Worker() { }

        public Worker(string name, int age, string gender, Address address, string position, double salary, int experience, string workshop)
    : base(name, age, gender, address, position, salary)
        {
            Experience = experience;
            Workshop = workshop;
        }


        public Worker(Worker other) : base(other)
        {
            Experience = other.Experience;
            Workshop = other.Workshop;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Experience: {Experience} years, Workshop: {Workshop}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Enter Experience: ");
            Experience = LibConvert.TryToConvertInt();
            Console.Write("Enter Workshop: ");
            Workshop = Console.ReadLine();
        }

        public override void RandomInit()
        {
            base.RandomInit();
            var random = new Random();
            Experience = random.Next(0, 40);
            Workshop = $"Workshop_{random.Next(1, 10)}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Worker other)
            {
                return base.Equals(other) &&
                       Experience == other.Experience &&
                       Workshop == other.Workshop;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Experience, Workshop);
        }
        public override Employee ShallowCopy()
        {
            return (Worker)this.MemberwiseClone();
        }
        public override object Clone()
        {
            return new Worker(this.Name, this.Age, this.Gender,new Address(this.Address.City, this.Address.Street), this.Position, this.Salary,this.Experience, this.Workshop); ;
        }
    }
}
