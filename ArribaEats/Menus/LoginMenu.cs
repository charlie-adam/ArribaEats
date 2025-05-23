using System;
using ArribaEats.Services;
using ArribaEats.Models;

namespace ArribaEats.Menus
{
    /// <summary>
    /// Provides the login and registration menu for users.
    /// </summary>
    public static class LoginMenu
    {
        /// <summary>
        /// Displays the login menu and handles user input for login, registration, or exit.
        /// </summary>
        public static void Show()
        {
            // Main menu loop for login/registration
            while (true)
            {
                Console.WriteLine("Please make a choice from the menu below:");
                Console.WriteLine("1: Login as a registered user");
                Console.WriteLine("2: Register as a new user");
                Console.WriteLine("3: Exit");
                Console.WriteLine("Please enter a choice between 1 and 3:");
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

        /// <summary>
        /// Handles the login process for a user.
        /// </summary>
        /// <returns>True if login is successful, otherwise false.</returns>
        private static bool Login()
        {
            Console.WriteLine("Email: ");
            string email = Console.ReadLine()?.Trim();

            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            try
            {
                var user = UserService.Authenticate(email, password);
                Console.WriteLine($"Welcome back, {user.Name}!");

                switch (user.Role)
                {
                    case "Customer":
                        CustomerMainMenu.Show((Customer)user);
                        break;
                    case "Deliverer":
                        DelivererMainMenu.Show((Deliverer)user);
                        break;
                    case "Client":
                        ClientMainMenu.Show((Client)user);
                        break;
                    default:
                        Console.WriteLine("Unknown user role.");
                        break;
                }

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Invalid email or password.");
                return false;
            }
        }
    }
}