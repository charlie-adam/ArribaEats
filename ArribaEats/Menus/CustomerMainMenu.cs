using System;
using ArribaEats.Models;

namespace ArribaEats.Menus
{
    public static class CustomerMainMenu
    {
        public static void Show(Customer customer)
        {
            while (true)
            {
                Console.WriteLine($"\nWelcome, {customer.Name} (Customer)");
                Console.WriteLine("1: View Available Meals");
                Console.WriteLine("2: View Past Orders");
                Console.WriteLine("3: Logout");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Feature not implemented yet: View Available Meals.");
                        break;
                    case "2":
                        Console.WriteLine("Feature not implemented yet: View Past Orders.");
                        break;
                    case "3":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}