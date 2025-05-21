namespace ArribaEats.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public string Style { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }

        private readonly List<int> _ratings = new();

        public Restaurant(string name, string style, double locationX, double locationY)
        {
            Name = name;
            Style = style;
            LocationX = locationX;
            LocationY = locationY;
        }

        public double AverageRating
        {
            get
            {
                if (_ratings.Count == 0)
                    return 0;
                return _ratings.Average();
            }
        }

        public void AddRating(int rating)
        {
            if (rating >= 1 && rating <= 5)
            {
                _ratings.Add(rating);
            }
        }
    }
}