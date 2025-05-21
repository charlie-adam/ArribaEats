using System.Collections.Generic;

namespace ArribaEats.Models
{
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

    public class Restaurant
    {
        public string Name { get; set; }
        public string Style { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public string OwnerEmail { get; set; }  // link to Client

        private readonly List<int> _ratings = new();
        public List<MenuItem> MenuItems { get; set; } = new();

        public Restaurant(string name, string style, double locationX, double locationY, string ownerEmail)
        {
            Name = name;
            Style = style;
            LocationX = locationX;
            LocationY = locationY;
            OwnerEmail = ownerEmail;
        }

        public double AverageRating => _ratings.Count == 0 ? 0 : _ratings.Average();

        public void AddRating(int rating)
        {
            if (rating >= 1 && rating <= 5)
            {
                _ratings.Add(rating);
            }
        }
    }
}