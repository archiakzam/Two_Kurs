using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }

        public Address(string city, string street)
        {
            City = city;
            Street = street;
        }

        public void Show()
        {
            Console.WriteLine($"City: {City}, Street: {Street}");
        }
        public override string ToString()
    {
        return $"City: {City}, Street: {Street}";
    }
        public override bool Equals(object obj)
        {
            if (obj is Address other)
            {
                return City == other.City && Street == other.Street;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(City, Street);
        }

    }
}
