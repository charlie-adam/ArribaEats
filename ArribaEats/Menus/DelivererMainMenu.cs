using System;
using ArribaEats.Models;

namespace ArribaEats.Menus
{
    public static class DelivererMainMenu
    {
        public static void Show(Deliverer deliverer)
        {
            while (true)
            {
                Console.WriteLine($"\nWelcome, {deliverer.Name} (Deliverer)");
                Console.WriteLine("1: View Assigned Deliveries");
                Console.WriteLine("2: Update Delivery Status");
                Console.WriteLine("3: Logout");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Feature not implemented yet: View Assigned Deliveries.");
                        break;
                    case "2":
                        Console.WriteLine("Feature not implemented yet: Update Delivery Status.");
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