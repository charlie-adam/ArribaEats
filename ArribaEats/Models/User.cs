namespace ArribaEats.Models
{
    public abstract class User
    {
        public string Name { get; }
        public int Age { get; }
        public string Email { get; }
        public string Mobile { get; }
        public string Password { get; }

        protected User(string name, int age, string email, string mobile, string password)
        {
            Name = name;
            Age = age;
            Email = email;
            Mobile = mobile;
            Password = password;
        }

        public abstract string Role { get; }
    }
}