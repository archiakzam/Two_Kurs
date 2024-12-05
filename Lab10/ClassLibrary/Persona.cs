using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary
{
    public interface IInit
    {
        void Init();
        void RandomInit();
        void Show();
    }
    public class Persona : IInit, IComparable<Persona>, ICloneable
    {   
        
        private string name = string.Empty;
        private int age = 0;
        private string gender = string.Empty;
        protected Random random = new Random();
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }
                name = value;
            }
        }
        public int Age
        {
            get => age;
            set
            {
                if (value < 18)
                {
                    throw new ArgumentException("Age must be 18 or older");
                }
                age = value;
            }
        }
        public string Gender
        {
            get => gender;
            set
            {
               
                if (value != "Male" && value != "Female") throw new ArgumentException("Gender must be 'Male' or 'Female'.");
                gender = value;
            }
        }
        public Address Address { get; set; }
        public Persona()
        {
            Address = new Address("Unknown City", "Unknown Street");
        }
        public Persona(string name, int age, string gender, Address address)
        {
            Gender = gender;
            Name = name;
            Age = age;
            Address = address ?? throw new ArgumentNullException(nameof(address), "Address cannot be null");
        }
        public Persona(Persona other)
        {
            
            Name = other.name;
            Age = other.age;
            Gender = other.gender;
            Address=other.Address;
        }
        public virtual void Show()
        {
            Console.WriteLine($"Perosna name: {Name} age: {Age} gender :{Gender} address:{Address}");
        }
        public virtual void Init()
        {
            Console.WriteLine("Enter name:");
            Name = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Enter age (18 or older):");
                try
                {
                    Age = LibConvert.TryToConvertInt();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            while (true)
            {
                Console.WriteLine("Enter gender (Male/Female):");
                try
                {
                    Gender = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            Console.WriteLine("Enter Address city");
            Address.City =Console.ReadLine() ;
            Console.WriteLine("Enter Address street");
            Address.Street = Console.ReadLine();
        }
        public virtual void RandomInit()
        {
            string[] randomNames = new string[]
            {
                "Alice", "Bob", "Charlie", "Diana", "Edward",
                "Fiona", "George", "Hannah", "Ian", "Jane",
                "Kevin", "Laura", "Mike", "Nina", "Oscar",
                "Paul", "Quincy", "Rachel", "Steve", "Tina"
            };

            name = randomNames[random.Next(randomNames.Length)];
            Age = random.Next(18, 65);
            Gender = random.Next(2) == 0 ? "Male" : "Female";
            Address.City = $"City_{random.Next(1, 10)}";
            Address.Street = $"Street_{random.Next(1, 10)}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Persona other)
            {
                return Name == other.Name && Age == other.Age && Gender == other.Gender && Address==other.Address;
            }
            return false;
        }
        public int CompareTo(Persona other)
        {
            if (other == null) return 1;
            return Age.CompareTo(other.Age);
        }
        public override int GetHashCode()
        {
            return (Name, Age, Gender,Address).GetHashCode();
        }
        public virtual Persona ShallowCopy()
        {
            return (Persona)this.MemberwiseClone();
        }
        public virtual object Clone()
        {
            return new Persona(this.Name, this.Age, this.Gender, new Address(this.Address.City, this.Address.Street));
        }
    }
}
