using OrderManagement.Core.DTOs.Order;

namespace OrderManagement.Business.Services.Abstracts
{
    public interface IOrderService
    {
        OrderDTO GetById(int id);
        List<OrderDTO> GetAll();
        List<OrderDTO> GetUserOrders(int userId);
        string Create(CreateOrderRequestDTO createOrderRequestDTO);
    }
}
