namespace Exercise02
{
    public class Square : Shape {
        public Square (double _len) {
            Width = _len;
            Height = _len;
        }
        public override double Area {get {return Height*Width;} set {Area = value;}}
    }
}