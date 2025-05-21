using System.Collections.Generic;
using System.Linq;
using ArribaEats.Models;

namespace ArribaEats.Repositories
{
    public class RestaurantRepository
    {
        private readonly List<Restaurant> _restaurants = new();

        public void AddRestaurant(Restaurant restaurant)
        {
            _restaurants.Add(restaurant);
        }

        public List<Restaurant> GetAll()
        {
            return _restaurants;
        }

        public Restaurant GetByName(string name)
        {
            return _restaurants.FirstOrDefault(r => r.Name == name);
        }

        public void AddRating(string restaurantName, int rating)
        {
            var restaurant = GetByName(restaurantName);
            if (restaurant != null)
            {
                restaurant.AddRating(rating);
            }
        }
    }
}