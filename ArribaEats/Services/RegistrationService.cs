using System;
using ArribaEats.Models;
using ArribaEats.Repositories;

namespace ArribaEats.Services
{
    /// <summary>
    /// Service for handling user registration.
    /// </summary>
    public class RegistrationService
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public RegistrationService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="name">Full name of the user.</param>
        /// <param name="age">Age of the user.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="mobileNumber">Mobile number of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="role">Role of the user.</param>
        /// <returns>True if registration is successful; otherwise, false.</returns>
        public bool RegisterUser(string name, int age, string email, string mobileNumber, string password, UserRole role)
        {
            if (_userRepository.IsEmailRegistered(email))
            {
                return false; // Email already registered
            }

            var user = new User(name, age, email, mobileNumber, password, role);
            _userRepository.AddUser(user);
            return true;
        }
    }
}