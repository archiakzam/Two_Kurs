using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Engineer : Worker
    {
        private string qualification = string.Empty;
        private string specialization = string.Empty;
        public string Specialization
        {
            get => specialization;
            set => specialization = value;
        }

        public string Qualification
        {
            get => qualification;
            set => qualification = value;
        }

        public Engineer() { }

        public Engineer(string name, int age, string gender, Address address, string position, double salary, int experience, string workshop, string specialization, string qualification)
        : base(name, age, gender, address, position, salary, experience, workshop)
        {
            Specialization = specialization;
            Qualification = qualification;
        }

        public Engineer(Engineer other) : base(other)
        {
            Specialization = other.Specialization;
            Qualification = other.Qualification;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Specialization: {Specialization}, Qualification: {Qualification}");
        }
        public void ShowNonVirtual()
        {
            Console.WriteLine($"Perosna name: {Name}, age: {Age}, gender: {Gender}, address: {Address}");
            Console.WriteLine($"Position: {Position}, Salary: {Salary}");
            Console.WriteLine($"Experience: {Experience} years, Workshop: {Workshop}");
            Console.WriteLine($"Specialization: {Specialization}, Qualification: {Qualification}");
        }
        public override void Init()
        {
            base.Init();
            Console.Write("Enter Specialization: ");
            Specialization = Console.ReadLine();
            Console.Write("Enter Qualification: ");
            Qualification = Console.ReadLine();
        }

        public override void RandomInit()
        {
            base.RandomInit();
            string[] specializations = { "Electrical", "Mechanical", "Software", "Civil" };
            string[] qualifications = { "Expert", "Intermediate", "Beginner" };

            var random = new Random();
            Specialization = specializations[random.Next(specializations.Length)];
            Qualification = qualifications[random.Next(qualifications.Length)];
        }
        public override bool Equals(object obj)
        {
            if (obj is Engineer other)
            {
                return base.Equals(other) &&
                       Specialization == other.Specialization &&
                       Qualification == other.Qualification;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override Employee ShallowCopy()
        {
            return (Engineer)this.MemberwiseClone();
        }
        public Worker BaseWorker
        {
            get
            {
                return new Worker(this.Name, this.Age, this.Gender, this.Address, this.Position, this.Salary, this.Experience, this.Workshop);
            }
        }
        public override object Clone()
        {
            return new Engineer(
                this.Name,
                this.Age,
                this.Gender,
                new Address(this.Address.City, this.Address.Street),
                this.Position,
                this.Salary,
                this.Experience,
                this.Workshop,
                this.Specialization,
                this.Qualification);
        }
    }
}
