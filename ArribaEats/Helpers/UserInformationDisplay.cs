using System;
using ArribaEats.Models;

namespace ArribaEats.Helpers
{
    public static class UserInformationDisplay
    {
        private static void DisplayBaseUserInfo(User user)
        {
            Console.WriteLine("Your user details are as follows:");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Age: {user.Age}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Mobile: {user.Mobile}");
        }

        public static void DisplayCustomerInfo(Customer customer)
        {
            DisplayBaseUserInfo(customer);
            Console.WriteLine($"Location: {customer.Location.X},{customer.Location.Y}");
            Console.WriteLine($"You've made {customer.OrderCount} order(s) and spent a total of ${customer.TotalSpent:F2} here.");
        }

        public static void DisplayDelivererInfo(Deliverer deliverer)
        {
            DisplayBaseUserInfo(deliverer);
            Console.WriteLine($"Licence plate: {deliverer.LicencePlate}");

            if (deliverer.CurrentDelivery != null)
            {
                var order = deliverer.CurrentDelivery;
                Console.WriteLine("Current delivery:");
                Console.WriteLine($"Order #{order.OrderId} from {order.Restaurant.Name} at {order.Restaurant.LocationX},{order.Restaurant.LocationY}.");
                Console.WriteLine($"To be delivered to {order.Customer.Name} at {order.Customer.Location.X},{order.Customer.Location.Y}.");
            }
        }

        public static void DisplayClientInfo(Client client)
        {
            DisplayBaseUserInfo(client);
            Console.WriteLine($"Restaurant name: {client.Restaurant.Name}");
            Console.WriteLine($"Restaurant style: {client.Restaurant.Style}");
            Console.WriteLine($"Restaurant location: {client.Restaurant.LocationX},{client.Restaurant.LocationY}");
        }
    }
}