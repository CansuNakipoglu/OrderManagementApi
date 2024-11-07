using AutoMapper;
using OrderManagement.Business.Services.Abstracts;
using OrderManagement.Core.DTOs.Order;
using OrderManagement.Core.Entities;
using OrderManagement.Data.Repositroies.Abstracts;

namespace OrderManagement.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Product> productRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public string Create(CreateOrderRequestDTO createOrderRequestDTO)
        {
            var products = _productRepository.GetAll().Where(x => createOrderRequestDTO.ProductIds.Contains(x.Id)).ToList();

            var order = new Order
            {
                UserId = createOrderRequestDTO.UserId,
                Quantity = createOrderRequestDTO.Quantity,
                Products = products
            };

            _repository.Add(order);
            _unitOfWork.Commit();

            return "Sipariş Başarıyla Oluşturuldu";
        }

        public OrderDTO? GetById(int id)
        {
            var order = _repository.GetAllIncluding(x => x.User, x => x.Products).Where(x => x.Id == id).FirstOrDefault();

            if (order == null) { return null; }

            return _mapper.Map<OrderDTO>(order);
        }

        public List<OrderDTO> GetAll()
        {
            var order = _repository.GetAllIncluding(x => x.User, x => x.Products).ToList();

            return _mapper.Map<List<OrderDTO>>(order);
        }

        public List<OrderDTO> GetUserOrders(int userId)
        {
            var order = _repository.GetAllIncluding(x => x.User, x => x.Products).Where(x => x.UserId == userId).ToList();

            return _mapper.Map<List<OrderDTO>>(order);
        }
    }
}
