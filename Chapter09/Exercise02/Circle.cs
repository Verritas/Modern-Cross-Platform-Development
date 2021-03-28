namespace Exercise02
{
    public class Circle : Shape
    {
        public double Radius {get;set;}

        public override double Area {get{return 3.14*Radius*Radius;}}
        public Circle() {}
    }
}