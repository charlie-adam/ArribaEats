using System;
using System.Collections.Generic;
using System.Linq;
using ArribaEats.Models;
using ArribaEats.Helpers;
using ArribaEats.Repositories;

namespace ArribaEats.Menus
{
    public static class CustomerMainMenu
    {
        private static readonly RestaurantRepository _restaurantRepo = new RestaurantRepository();
        private static readonly OrderRepository _orderRepo = new OrderRepository();

        public static void Show(Customer customer)
        {
            while (true)
            {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: Select a list of restaurants to order from");
                Console.WriteLine("3: See the status of your orders");
                Console.WriteLine("4: Rate a restaurant you've ordered from");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UserInformationDisplay.DisplayCustomerInfo(customer);
                        break;

                    case "2":
                        ShowRestaurantList(customer);
                        break;

                    case "3":
                        ShowOrderStatus(customer);
                        break;

                    case "4":
                        RateRestaurant(customer);
                        break;

                    case "5":
                        Console.WriteLine("You are now logged out.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void ShowRestaurantList(Customer customer)
        {
            Console.WriteLine("How would you like the list of restaurants ordered?");
            Console.WriteLine("1: Sorted alphabetically by name");
            Console.WriteLine("2: Sorted by distance");
            Console.WriteLine("3: Sorted by style");
            Console.WriteLine("4: Sorted by average rating");
            Console.WriteLine("5: Return to the previous menu");
            Console.WriteLine("Please enter a choice between 1 and 5:");
            var choice = Console.ReadLine();

            List<Restaurant> restaurants = _restaurantRepo.GetAll();

            IEnumerable<Restaurant> sortedRestaurants = choice switch
            {
                "1" => restaurants.OrderBy(r => r.Name),
                "2" => restaurants.OrderBy(r => GetDistance(customer.Location.X, customer.Location.Y, r.LocationX, r.LocationY)),
                "3" => restaurants.OrderBy(r => r.Style),
                "4" => restaurants.OrderByDescending(r => r.AverageRating), // Assuming AverageRating property
                "5" => null,
                _ => null
            };

            if (sortedRestaurants == null)
                return;

            Console.WriteLine("Restaurants:");
            int i = 1;
            foreach (var r in sortedRestaurants)
            {
                Console.WriteLine($"{i++}: {r.Name} ({r.Style}) - Location: {r.LocationX},{r.LocationY}");
            }
            // Additional ordering logic, such as choosing a restaurant to order from, can be added here.
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        private static void ShowOrderStatus(Customer customer)
        {
            var orders = _orderRepo.GetOrdersByCustomerEmail(customer.Email);
            if (orders.Count == 0)
            {
                Console.WriteLine("You have not placed any orders.");
                return;
            }

            Console.WriteLine("Your orders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order #{order.OrderId}: From {order.Restaurant.Name} - Status: {order.Status}");
            }
        }

        private static void RateRestaurant(Customer customer)
        {
            var orders = _orderRepo.GetOrdersByCustomerEmail(customer.Email)
                .Where(o => !o.IsRated)
                .ToList();

            Console.WriteLine("Select a previous order to rate the restaurant it came from:");
            Console.WriteLine("1: Return to the previous menu");

            if (orders.Count == 0)
            {
                Console.WriteLine("Please enter a choice between 1 and 1:");
                var input = Console.ReadLine();
                if (input == "1")
                    return;
                else
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                }
            }

            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"{i + 2}: Order #{orders[i].OrderId} from {orders[i].Restaurant.Name}");
            }

            Console.WriteLine($"Please enter a choice between 1 and {orders.Count + 1}:");
            var choiceInput = Console.ReadLine();

            if (!int.TryParse(choiceInput, out int selection) || selection < 1 || selection > orders.Count + 1)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            if (selection == 1)
            {
                // Return to previous menu
                return;
            }

            // Adjust selection index to zero-based orders list index
            var orderToRate = orders[selection - 2];

            Console.WriteLine("Please enter your rating (1 to 5):");
            var ratingInput = Console.ReadLine();

            if (int.TryParse(ratingInput, out int rating) && rating >= 1 && rating <= 5)
            {
                _restaurantRepo.AddRating(orderToRate.Restaurant.Name, rating);
                _orderRepo.MarkOrderAsRated(orderToRate.OrderId);
                Console.WriteLine($"Thank you for rating {orderToRate.Restaurant.Name} with a {rating}!");
            }
            else
            {
                Console.WriteLine("Invalid rating input.");
            }
        }
    }
}