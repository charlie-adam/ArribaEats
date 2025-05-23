namespace ArribaEats.Models
{
    public class OrderItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderItem(string itemName, decimal price, int quantity)
        {
            ItemName = itemName;
            Price = price;
            Quantity = quantity;
        }
    }
}