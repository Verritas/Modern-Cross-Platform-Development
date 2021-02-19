namespace Packt.Shared
{
    public partial class Person {
        public string Origin {
            get {
                return $"{Name} was born on {HomePlanet}";
            }
        }
        public string Greeting => $"{Name} says Hello";
        public int Age =>System.DateTime.Today.Year - DateOfBirth.Year;

        public Person this[int index] {
            get {
                return Children[index];
            }
            set {
                Children[index] = value;
            }
        }
    }
}