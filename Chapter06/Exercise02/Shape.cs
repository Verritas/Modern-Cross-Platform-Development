namespace Exercise02
{
    public abstract class Shape
    {
        private double height;
        public virtual double Height {
            get {return height;}
            set {height = value;}
        }

        private double width;
        public virtual double Width {
            get {return width;}
            set {width = value;}
        }

        public abstract double Area {get;set;}
    }
}