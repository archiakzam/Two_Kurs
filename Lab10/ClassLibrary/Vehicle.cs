using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Vehicle : IInit, ICloneable
    {
        private Random rnd = new Random();
        private int speed;
        private string name = string.Empty;
        public int Speed
        {
            get { return speed; }
            set
            {
                if (value >= 0)
                {
                    speed = value;
                }
                else
                {
                    throw new Exception("Скорость не может быть меньше 0");
                }
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Vehicle()
        {
            Speed = 0;
        }
        public Vehicle(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
        public Vehicle(Vehicle other)
        {
            Name = other.Name;
            Speed = other.Speed;
        }
        public void Init()
        {
            Console.WriteLine("Введите название транспортного средства: ");
            Name = Console.ReadLine();
            Console.WriteLine("Введите максимальную скорость: ");
            try
            {
                Speed = LibConvert.TryToConvertInt();
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void RandomInit()
        {
            Speed = rnd.Next(60, 220);
            string[] randomName = new string[] {
                "Автобус",
                "Легковая машина",
                "Трактор",
                "Экскаватор",
                "Трамвай"
            };
            Name = randomName[rnd.Next(randomName.Length)];
        }
        public override bool Equals(object obj)
        {
            if (obj is Vehicle other)
            {
                return Name == other.Name && Speed == other.Speed;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Speed);
        }
        public void Show()
        {
            Console.WriteLine("Название: " + Name);
            Console.WriteLine("Скорость: " + Speed);
        }
        public void ShowNonVirtual()
        {
            Console.WriteLine("Название: " + Name);
            Console.WriteLine("Скорость: " + Speed);
        }
        public Vehicle ShallowCopy()
        {
            return (Vehicle)this.MemberwiseClone();
        }
        public object Clone()
        {
            return new Vehicle(Name, Speed);

        }

    }
}
