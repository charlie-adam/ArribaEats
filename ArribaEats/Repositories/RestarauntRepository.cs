using ArribaEats.Models;

/// <summary>
/// Repository for managing restaurant data in memory.
/// </summary>
public class RestaurantRepository
{
    private static RestaurantRepository _instance;

    /// <summary>
    /// Gets the singleton instance of the RestaurantRepository.
    /// </summary>
    public static RestaurantRepository Instance
    {
        get
        {
            // Lazy initialization of singleton instance
            if (_instance == null)
            {
                _instance = new RestaurantRepository();
            }
            return _instance;
        }
    }

    // Private constructor to prevent external instantiation
    private RestaurantRepository() { }

    private readonly List<Restaurant> _restaurants = new();

    /// <summary>
    /// Adds a new restaurant to the repository.
    /// </summary>
    /// <param name="restaurant">The restaurant to add.</param>
    public void AddRestaurant(Restaurant restaurant)
    {
        // Add restaurant to the list
        _restaurants.Add(restaurant);
    }

    /// <summary>
    /// Retrieves a restaurant by name.
    /// </summary>
    /// <param name="name">The name of the restaurant.</param>
    /// <returns>The restaurant with the specified name, or null if not found.</returns>
    public Restaurant GetByName(string name)
    {
        // Find restaurant by name
        return _restaurants.FirstOrDefault(r => r.Name == name);
    }

    /// <summary>
    /// Retrieves a restaurant by the owner's email.
    /// </summary>
    /// <param name="email">The owner's email.</param>
    /// <returns>The restaurant owned by the specified email, or null if not found.</returns>
    public Restaurant GetByOwnerEmail(string email)
    {
        // Find restaurant by owner email
        return _restaurants.FirstOrDefault(r => r.OwnerEmail == email);
    }

    /// <summary>
    /// Gets all restaurants.
    /// </summary>
    /// <returns>A list of all restaurants.</returns>
    public List<Restaurant> GetAll()
    {
        // Return all restaurants
        return _restaurants;
    }
    
    /// <summary>
    /// Adds a rating and comment to a restaurant.
    /// </summary>
    /// <param name="restaurantName">The name of the restaurant.</param>
    /// <param name="rating">The rating value.</param>
    /// <param name="comment">The comment for the rating.</param>
    /// <param name="customerName">The name of the customer leaving the rating.</param>
    public void AddRating(string restaurantName, int rating, string comment, string customerName)
    {
        // Add rating to the specified restaurant
        var restaurant = GetByName(restaurantName);
        if (restaurant != null)
        {
            restaurant.AddRating(rating, comment, customerName);
        }
    }
}