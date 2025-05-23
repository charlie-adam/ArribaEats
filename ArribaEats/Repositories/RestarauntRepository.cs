using ArribaEats.Models;

public class RestaurantRepository
{
    private static RestaurantRepository _instance;

    public static RestaurantRepository Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RestaurantRepository();
            }
            return _instance;
        }
    }

    private RestaurantRepository() { }

    private readonly List<Restaurant> _restaurants = new();

    public void AddRestaurant(Restaurant restaurant)
    {
        _restaurants.Add(restaurant);
    }

    public Restaurant GetByName(string name)
    {
        return _restaurants.FirstOrDefault(r => r.Name == name);
    }

    public Restaurant GetByOwnerEmail(string email)
    {
        return _restaurants.FirstOrDefault(r => r.OwnerEmail == email);
    }

    public List<Restaurant> GetAll()
    {
        return _restaurants;
    }
    
    public void AddRating(string restaurantName, int rating, string comment, string customerName)
    {
        var restaurant = GetByName(restaurantName);
        if (restaurant != null)
        {
            restaurant.AddRating(rating, comment, customerName);
        }
    }
}