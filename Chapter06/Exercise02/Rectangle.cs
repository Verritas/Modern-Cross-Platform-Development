namespace Exercise02
{
    public class Rectangle : Shape {
        public Rectangle (double _width, double _height) {
            Width = _width;
            Height = _height;
        }
        public override double Area {get {return Height*Width;} set {Area = value;}}
    }
}