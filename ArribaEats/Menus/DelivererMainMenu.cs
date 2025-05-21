using System;
using ArribaEats.Models;
using ArribaEats.Helpers;

namespace ArribaEats.Menus
{
    public static class DelivererMainMenu
    {
        public static void Show(Deliverer deliverer)
        {
            while (true)
            {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Display your user information");
                Console.WriteLine("2: List orders available to deliver");
                Console.WriteLine("3: Arrived at restaurant to pick up order");
                Console.WriteLine("4: Mark this delivery as complete");
                Console.WriteLine("5: Log out");
                Console.WriteLine("Please enter a choice between 1 and 5: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UserInformationDisplay.DisplayDelivererInfo(deliverer);
                        break;

                    case "5":
                        Console.WriteLine("You are now logged out.");
                        return;

                    default:
                        Console.WriteLine("Feature not implemented yet.");
                        break;
                }
            }
        }
    }
}