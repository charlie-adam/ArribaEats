using System;
using ArribaEats.Models;
using ArribaEats.Helpers;

namespace ArribaEats.Menus
{
    public static class CustomerMainMenu
    {
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