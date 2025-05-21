using System.Collections.Generic;
using System.Linq;
using ArribaEats.Models;

namespace ArribaEats.Repositories
{
    /// <summary>
    /// Repository for managing user data in memory.
    /// </summary>
    public class UserRepository
    {
        private readonly List<User> _users = new List<User>();

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        /// <summary>
        /// Retrieves a user by email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user with the specified email, or null if not found.</returns>
        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Checks if an email is already registered.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the email is already registered; otherwise, false.</returns>
        public bool IsEmailRegistered(string email)
        {
            return _users.Any(u => u.Email == email);
        }

        //Authenticate 
        
        /// <summary>
        /// Authenticates a user by email and password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The authenticated user, or null if authentication fails.</returns>
        public User Authenticate(string email, string password)
        {
            return _users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}