using System;
using System.Linq;
using static System.Console;
using Exercise02;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Northwind()) {
                var cities = db.Customers.Select(c => c.City).Distinct();
                foreach (var item in cities) {
                        Write($" {item}");
                }

                Write("Enter the name of a city: ");
                var city = ReadLine();

                var customersCity = db.Customers.Where(c => c.City == city);

                WriteLine($"There are {customersCity.Count()} customers in {city}:");
                foreach (var item in customersCity)
                {
                    WriteLine($"{item.CompanyName}");
                }
            }
        }
    }
}
