using System;
using ArribaEats.Models;

namespace ArribaEats.Menus
{
    public static class ClientMainMenu
    {
        public static void Show(Client client)
        {
            while (true)
            {
                Console.WriteLine($"\nWelcome, {client.Name} (Client)");
                Console.WriteLine("1: Upload New Menu Items");
                Console.WriteLine("2: View Orders");
                Console.WriteLine("3: Logout");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Feature not implemented yet: Upload New Menu Items.");
                        break;
                    case "2":
                        Console.WriteLine("Feature not implemented yet: View Orders.");
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