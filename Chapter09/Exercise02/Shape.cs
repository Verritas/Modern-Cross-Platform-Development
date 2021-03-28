using System.Xml.Serialization;

namespace Exercise02
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public abstract class Shape
    {
        public string Colour {get;set;}
        public double Height{get;set;}
        public double Width{get;set;}

        public abstract double Area {get;}
        public Shape() {}
    }
}