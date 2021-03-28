using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using static System.Environment;
namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            var listOfShapes = new List<Shape> {
                new Circle { Colour = "Red", Radius = 2.5 },
                new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
                new Circle { Colour = "Green", Radius = 8.0 },
                new Circle { Colour = "Purple", Radius = 12.3 },
                new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
            };

            var serializer = new XmlSerializer(typeof(List<Shape>));
            string path = Path.Combine(CurrentDirectory, "shapes.xml");
            FileStream stream = File.Create(path);
            serializer.Serialize(stream, listOfShapes);
            stream.Dispose();
            stream = File.Open(path, FileMode.Open);

            List<Shape> loadedShapesXml = serializer.Deserialize(stream) as List<Shape>;
            stream.Dispose();

            foreach (Shape item in loadedShapesXml)
            {
                Console.WriteLine($"{item.Colour}  {item.GetType().Name}. Area: {item.Area:N2}");
            }
        }
    }
}
