using System;

namespace ArribaEats.Models
{
    /// <summary>
    /// Represents a user in the Arriba Eats system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the user.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="name">Full name of the user.</param>
        /// <param name="age">Age of the user.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="mobileNumber">Mobile number of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="role">Role of the user.</param>
        public User(string name, int age, string email, string mobileNumber, string password, UserRole role)
        {
            Name = name;
            Age = age;
            Email = email;
            MobileNumber = mobileNumber;
            Password = password;
            Role = role;
        }
    }
}