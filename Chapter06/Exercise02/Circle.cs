namespace Exercise02
{
    public class Circle : Shape {
        public Circle (double _diameter) {
            Width = _diameter;
            Height = _diameter;
        }
        public override double Area {get {return 3.14*Width*Height;} set {Area = value;}}
    }
}