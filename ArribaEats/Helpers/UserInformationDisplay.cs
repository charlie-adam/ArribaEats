using System;
using ArribaEats.Models;

namespace ArribaEats.Helpers
{
    /// <summary>
    /// Provides methods to display user information for different user types.
    /// </summary>
    public static class UserInformationDisplay
    {
        /// <summary>
        /// Displays the base user information (name, age, email, mobile).
        /// </summary>
        /// <param name="user">The user whose information to display.</param>
        private static void DisplayBaseUserInfo(User user)
        {
            // Print basic user details
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Age: {user.Age}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Mobile: {user.Mobile}");
        }

        /// <summary>
        /// Displays information for a customer user.
        /// </summary>
        /// <param name="customer">The customer whose information to display.</param>
        public static void DisplayCustomerInfo(Customer customer)
        {
            // Show base info and customer-specific info
            DisplayBaseUserInfo(customer);
            Console.WriteLine($"Location: {customer.Location.X},{customer.Location.Y}");
            Console.WriteLine($"You've made {customer.OrderCount} order(s) and spent a total of ${customer.TotalSpent:F2} here.");
        }

        /// <summary>
        /// Displays information for a deliverer user.
        /// </summary>
        /// <param name="deliverer">The deliverer whose information to display.</param>
        public static void DisplayDelivererInfo(Deliverer deliverer)
        {
            // Show base info and deliverer-specific info
            DisplayBaseUserInfo(deliverer);
            Console.WriteLine($"Licence plate: {deliverer.LicencePlate}");

            // Show current delivery if any
            if (deliverer.CurrentDelivery != null)
            {
                var order = deliverer.CurrentDelivery;
                Console.WriteLine("Current delivery:");
                Console.WriteLine($"Order #{order.OrderId} from {order.Restaurant.Name} at {order.Restaurant.LocationX},{order.Restaurant.LocationY}.");
                Console.WriteLine($"To be delivered to {order.Customer.Name} at {order.Customer.Location.X},{order.Customer.Location.Y}.");
            }
        }

        /// <summary>
        /// Displays information for a client user.
        /// </summary>
        /// <param name="client">The client whose information to display.</param>
        public static void DisplayClientInfo(Client client)
        {
            // Show base info and client-specific info
            DisplayBaseUserInfo(client);
            Console.WriteLine($"Restaurant name: {client.Restaurant.Name}");
            Console.WriteLine($"Restaurant style: {client.Restaurant.Style}");
            Console.WriteLine($"Restaurant location: {client.Restaurant.LocationX},{client.Restaurant.LocationY}");
        }
    }
}