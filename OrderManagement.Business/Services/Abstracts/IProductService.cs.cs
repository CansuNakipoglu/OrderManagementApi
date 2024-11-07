using OrderManagement.Core.DTOs.Product;

namespace OrderManagement.Business.Services.Abstracts
{
    public interface IProductService
    {
        void Create(CreateProductRequestDTO createProdcutRequest);
        Task<ProductDTO?> GetById(int id);
        List<ProductDTO> GetAll();
        string Delete(int id);
        Task<string> Put(UpdateProductDTO product, int productId);
        Task<string> Patch(UpdateProductDTO product, int productId);
    }
}
