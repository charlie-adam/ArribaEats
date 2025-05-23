using System.Collections.Generic;

namespace ArribaEats.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public string OwnerEmail { get; set; }
        public string Style { get; set; }
        public List<MenuItem> Menu { get; set; } = new List<MenuItem>();

        private readonly List<(int Rating, string Comment, string CustomerName)> _reviews = new();

        public void AddRating(int rating, string comment, string customerName)
        {
            if (rating >= 1 && rating <= 5)
            {
                _reviews.Add((rating, comment, customerName));
            }
        }

        public IEnumerable<(int Rating, string Comment, string CustomerName)> GetReviews()
        {
            return _reviews;
        }

        public double AverageRating => _reviews.Count == 0 ? 0 : _reviews.Average(r => r.Rating);

        public Restaurant(string name, string style, int locationX, int locationY, string ownerEmail)
        {
            Name = name;
            Style = style;
            LocationX = locationX;
            LocationY = locationY;
            OwnerEmail = ownerEmail;
        }

        public void AddMenuItem(string name, decimal price)
        {
            Menu.Add(new MenuItem(name, price));
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