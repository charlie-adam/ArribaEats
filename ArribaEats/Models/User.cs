namespace ArribaEats.Models
{
    public abstract class User
    {
        public string Name { get; }
        public int Age { get; }
        public string Email { get; }
        public string Mobile { get; }
        public string Password { get; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }

        protected User(string name, int age, string email, string mobile, string password, int orderCount = 0, decimal totalSpent = 0)
        {
            Name = name;
            Age = age;
            Email = email;
            Mobile = mobile;
            Password = password;
            OrderCount = orderCount;
            TotalSpent = totalSpent;
        }

        public abstract string Role { get; }
    }
}