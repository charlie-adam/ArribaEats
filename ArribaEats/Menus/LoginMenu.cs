using System;
using ArribaEats.Services;
using ArribaEats.Models;

namespace ArribaEats.Menus
{
    public static class LoginMenu
    {
        public static void Show()
        {
            while (true)
            {
                Console.WriteLine("\nPlease make a choice from the menu below:");
                Console.WriteLine("1: Login as a registered user");
                Console.WriteLine("2: Register as a new user");
                Console.WriteLine("3: Exit");
                Console.Write("Please enter a choice between 1 and 3: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (Login())
                        {
                            // After login, user is taken to their main menu
                            // This could be a loop inside Login() or separate
                        }
                        break;

                    case "2":
                        RegisterMenu.Run();
                        break;

                    case "3":
                        Console.WriteLine("Thank you for using Arriba Eats!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private static bool Login()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine()?.Trim();

            Console.Write("Password: ");
            string password = Console.ReadLine(); // Could be improved to hide input

            var user = UserService.Authenticate(email, password);

            if (user != null)
            {
                Console.WriteLine($"Welcome back, {user.Name}!");
                switch (user.Role)
                {
                    case "Customer":
                        // CustomerMainMenu.Show((Customer)user);
                        break;
                    case "Deliverer":
                        // DelivererMainMenu.Show((Deliverer)user);
                        break;
                    case "Client":
                        // ClientMainMenu.Show((Client)user);
                        break;
                    default:
                        Console.WriteLine("Unknown user role.");
                        break;
                }
                return true;
            }
            else
            {
                Console.WriteLine("Invalid email or password.");
                return false;
            }
        }
    }
}