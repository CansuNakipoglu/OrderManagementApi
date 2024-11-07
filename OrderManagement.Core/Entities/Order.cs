namespace OrderManagement.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Quantity { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
