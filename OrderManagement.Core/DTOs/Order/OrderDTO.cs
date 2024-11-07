using OrderManagement.Core.DTOs.Product;
using OrderManagement.Core.DTOs.User;

namespace OrderManagement.Core.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public int Quantity { get; set; }

        public ICollection<ProductDTO> Products { get; set; }
    }
}
