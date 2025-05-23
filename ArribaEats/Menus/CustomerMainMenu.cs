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
        private static RestaurantRepository _restaurantRepo = RestaurantRepository.Instance;
        private static readonly OrderRepository _orderRepo = OrderRepository.Instance;

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
            Console.WriteLine("Please enter a choice between 1 and 5: ");
            var choice = Console.ReadLine();

            List<Restaurant> restaurants = _restaurantRepo.GetAll();

            IEnumerable<Restaurant> sortedRestaurants = choice switch
            {
                "1" => restaurants.OrderBy(r => r.Name),
                "2" => restaurants.OrderBy(r => GetDistance(customer.Location.X, customer.Location.Y, r.LocationX, r.LocationY)),
                "3" => restaurants.OrderBy(r => r.Style),
                "4" => restaurants.OrderByDescending(r => r.AverageRating),
                "5" => null,
                _ => null
            };

            if (sortedRestaurants == null)
                return;

            Console.WriteLine("You can order from the following restaurants:");
            Console.WriteLine("   Restaurant Name       Loc    Dist  Style       Rating");
            int i = 1;
            foreach (var r in sortedRestaurants)
            {
                double distance = GetDistance(customer.Location.X, customer.Location.Y, r.LocationX, r.LocationY);
                string ratingDisplay = r.AverageRating > 0 ? r.AverageRating.ToString("0.0") : "-";
                Console.WriteLine($"{i}: {r.Name,-20} {r.LocationX},{r.LocationY}  {distance,4:0}   {r.Style,-10} {ratingDisplay}");
                i++;
            }
            Console.WriteLine($"{i}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {i}: ");
            var selectionInput = Console.ReadLine();

            if (!int.TryParse(selectionInput, out int selection) || selection < 1 || selection > i)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            if (selection == i)
            {
                // Return to previous menu
                return;
            }

            var selectedRestaurant = sortedRestaurants.ElementAt(selection - 1);
            HandleRestaurantSelection(customer, selectedRestaurant);
        }

        private static void HandleRestaurantSelection(Customer customer, Restaurant restaurant)
        {
            var first = false;
            while (true)
            {
                if (!first)
                {
                    Console.WriteLine($"Placing order from {restaurant.Name}.");
                    first = true;
                }
                Console.WriteLine("1: See this restaurant's menu and place an order");
                Console.WriteLine("2: See reviews for this restaurant");
                Console.WriteLine("3: Return to main menu");
                Console.WriteLine("Please enter a choice between 1 and 3: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlaceOrder(customer, restaurant);
                        break;
                    case "2":
                        // Assuming there's a method to display reviews
                        Console.WriteLine("Feature not implemented yet.");
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void PlaceOrder(Customer customer, Restaurant restaurant)
        {
            var orderItems = new Dictionary<MenuItem, int>();
            while (true)
            {
                Console.WriteLine("Current order total: $" + orderItems.Sum(item => item.Key.Price * item.Value).ToString("0.00"));
                int index = 1;
                foreach (var item in restaurant.Menu)
                {
                    Console.WriteLine($"{index}:   ${item.Price:0.00}  {item.Name}");
                    index++;
                }
                Console.WriteLine($"{index}: Complete order");
                Console.WriteLine($"{index + 1}: Cancel order");
                Console.WriteLine($"Please enter a choice between 1 and {index + 1}: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int selection) || selection < 1 || selection > index + 1)
                {
                    Console.WriteLine("Invalid selection.");
                    continue;
                }

                if (selection == index)
                {
                    if (orderItems.Count == 0)
                    {
                        Console.WriteLine("You must add at least one item to your order.");
                        continue;
                    }
                    // Complete order
                    var newOrder = new Order(_orderRepo.GetNextOrderId(), restaurant, customer)
                    {
                        OrderItems = orderItems
                    };
                    _orderRepo.AddOrder(newOrder);
                    Console.WriteLine($"Your order has been placed. Your order number is #{newOrder.OrderId}.");
                    return;
                }
                else if (selection == index + 1)
                {
                    return;
                }
                else
                {
                    var selectedItem = restaurant.Menu[selection - 1];
                    Console.WriteLine($"Adding {selectedItem.Name} to order.");
                    Console.WriteLine("Please enter quantity (0 to cancel): ");
                    var quantityInput = Console.ReadLine();
                    if (!int.TryParse(quantityInput, out int quantity) || quantity < 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        continue;
                    }
                    if (quantity == 0)
                    {
                        continue;
                    }
                    if (orderItems.ContainsKey(selectedItem))
                    {
                        orderItems[selectedItem] += quantity;
                    }
                    else
                    {
                        orderItems[selectedItem] = quantity;
                    }
                    Console.WriteLine($"Added {quantity} x {selectedItem.Name} to order.");
                }
            }
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
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
            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"{i + 1}: Order #{orders[i].OrderId} from {orders[i].Restaurant.Name}");
            }
            Console.WriteLine($"{orders.Count + 1}: Return to the previous menu");
            Console.WriteLine($"Please enter a choice between 1 and {orders.Count + 1}: ");
            var choiceInput = Console.ReadLine();

            if (!int.TryParse(choiceInput, out int selection) || selection < 1 || selection > orders.Count + 1)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            if (selection == orders.Count + 1)
            {
                // Return to previous menu
                return;
            }

            var orderToRate = orders[selection - 1];

            Console.WriteLine("Please enter your rating (1 to 5): ");
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