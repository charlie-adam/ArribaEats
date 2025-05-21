namespace ArribaEats.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public string Style { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }

        public Restaurant(string name, string style, double locationX, double locationY)
        {
            Name = name;
            Style = style;
            LocationX = locationX;
            LocationY = locationY;
        }
    }
}