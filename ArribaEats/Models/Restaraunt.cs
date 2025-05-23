using System.Collections.Generic;

namespace ArribaEats.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public string OwnerEmail { get; set; }
        private readonly List<int> _ratings = new();
        public string Style { get; set; }
        public List<MenuItem> Menu { get; set; } = new List<MenuItem>();
        public List<int> Ratings { get; set; } = new List<int>();

        public double AverageRating => Ratings.Count == 0 ? 0 : Ratings.Average();

        public void AddMenuItem(string name, decimal price)
        {
            Menu.Add(new MenuItem(name, price));
        }
        
        public Restaurant(string name, string style, int locationX, int locationY, string ownerEmail)
        {
            Name = name;
            Style = style;
            LocationX = locationX;
            LocationY = locationY;
            OwnerEmail = ownerEmail;
        }
        
        public void AddRating(int rating)
        {
            if (rating >= 1 && rating <= 5)
            {
                _ratings.Add(rating);
            }
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public MenuItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}