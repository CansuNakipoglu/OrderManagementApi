using OrderManagement.Core.DTOs.Product;

namespace OrderManagement.Core.DTOs.Order
{
    public class CreateOrderRequestDTO
    {
        public int UserId { get; set; }
        public int Quantity { get; set; }

        public ICollection<int> ProductIds { get; set; }
    }
}
