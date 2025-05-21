using System;
using ArribaEats.Models;
using ArribaEats.Repositories;
using ArribaEats.Services;

namespace ArribaEats
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new UserRepository();
            var registrationService = new RegistrationService(userRepository);

            Console.WriteLine("Welcome to Arriba Eats Registration!");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your mobile number: ");
            string mobileNumber = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Select your role (Customer, Deliverer, Client): ");
            string roleInput = Console.ReadLine();
            UserRole role = Enum.Parse<UserRole>(roleInput, true);

            bool isRegistered = registrationService.RegisterUser(name, age, email, mobileNumber, password, role);

            if (isRegistered)
            {
                Console.WriteLine("Registration successful!");
            }
            else
            {
                Console.WriteLine("Registration failed. Email already registered.");
            }
        }
    }
}