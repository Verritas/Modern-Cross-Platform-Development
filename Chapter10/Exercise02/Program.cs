using System;
using System.IO;
using static System.IO.Path;
using static System.Environment;
using System.Xml;
using Packt.Shared;
using static System.Console;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create new customer
            var customer = new Customer {
                name = "Bob Smith",
                creditCard = "1234-5678-9012-3456",
                password = "Pa$$w0rd"
            };

            string file = Combine(CurrentDirectory, "protected-customers.xml");

            var xmlWriter = XmlWriter.Create(file, new XmlWriterSettings { Indent = true });

            var userCrypto = Protector.Register(customer.name, customer.password);
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("customer");
            xmlWriter.WriteElementString("name", customer.name);
            xmlWriter.WriteElementString("creditcard", Protector.Encrypt(customer.creditCard, customer.password));
            xmlWriter.WriteElementString("password", userCrypto.SaltedHashedPassword);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            WriteLine(File.ReadAllText(file));
        }
    }
}
