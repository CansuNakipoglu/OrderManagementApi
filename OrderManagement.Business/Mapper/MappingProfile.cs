using AutoMapper;
using OrderManagement.Core.DTOs.Order;
using OrderManagement.Core.DTOs.Product;
using OrderManagement.Core.DTOs.User;
using OrderManagement.Core.Entities;

namespace OrderManagement.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
