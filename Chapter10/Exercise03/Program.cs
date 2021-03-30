using System;
using Packt.Shared;
using System.IO;
using static System.IO.Path;
using static System.Console;
using static System.Environment;
using System.Xml;

namespace Exercise03
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Password: ");
            string password = ReadLine();

            string file = Combine(CurrentDirectory,"protected-customers.xml");
            if (!File.Exists(file)) {
                WriteLine("File not found");
                return;
            }

            var xmlReader = XmlReader.Create(file, new XmlReaderSettings { IgnoreWhitespace = true });

            var decrypt = new Customer();

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "customer")
                {
                xmlReader.Read();
                string Dname = xmlReader.ReadElementContentAsString();
                string DcreditcardEncrypted = xmlReader.ReadElementContentAsString();
                string Dcreditcard = null;
                try
                {
                    Dcreditcard = Protector.Decrypt(DcreditcardEncrypted, password);
                }
                catch (System.Exception)
                {
                    WriteLine($"Failed to decrypt {Dname}'s credit card.");
                    return;
                }
                string passwordHashed = xmlReader.ReadElementContentAsString();

                decrypt = new Customer
                {
                    name = Dname,
                    creditCard = Dcreditcard,
                    password = passwordHashed
                };
                }

                WriteLine($"Decrypt result: {decrypt.name} {decrypt.password} {decrypt.creditCard}");
            }
        }
    }
}
