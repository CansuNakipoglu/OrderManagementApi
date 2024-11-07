using Microsoft.AspNetCore.Mvc;
using OrderManagement.Business.Services.Abstracts;
using OrderManagement.Core.DTOs.Order;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public List<OrderDTO> GetAllOrders()
        {
            return _orderService.GetAll();
        }

        [HttpPost]
        public ActionResult CreateOrder(CreateOrderRequestDTO createProductRequest)
        {
            return Ok(_orderService.Create(createProductRequest));
        }

        [HttpGet("Id")]
        public ActionResult GetOrder(int Id)
        {
            var product = _orderService.GetById(Id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet("userId")]
        public ActionResult GetUserOrders(int userId)
        {
            var product = _orderService.GetById(userId);

            if (product == null) return NotFound();

            return Ok(product);
        }
    }
}
