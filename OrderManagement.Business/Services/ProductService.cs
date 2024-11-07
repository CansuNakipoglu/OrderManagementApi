using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Business.Services.Abstracts;
using OrderManagement.Core.DTOs.Product;
using OrderManagement.Core.Entities;
using OrderManagement.Data.Repositroies.Abstracts;

namespace OrderManagement.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(CreateProductRequestDTO createProdcutRequest)
        {
            var product = new Product
            {
                Name = createProdcutRequest.Name,
                Description = createProdcutRequest.Description,
                Price = createProdcutRequest.Price
            };

            _repository.Add(product);
            _unitOfWork.Commit();
        }

        public async Task<ProductDTO?> GetById(int id)
        {
            var product = await _repository.GetAsync(id);

            if (product == null) { return null; }

            return new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Id = product.Id
            };
        }

        public List<ProductDTO> GetAll()
        {
            var products = _repository.GetAll().ToList();
        
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public string Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return "Ürün bulunamadı";

            _repository.Delete(_mapper.Map<Product>(product));
            _unitOfWork.Commit();

            return "Ürün Başarıyla silindi";
        }

        public async Task<string> Put(UpdateProductDTO product, int productId)
        {
            var productEntity = await _repository.GetAsync(productId);
            if (productEntity == null) return "Ürün bulunamadı";

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Description = product.Description;
            _repository.Update(productEntity);
            _unitOfWork.Commit();

            return "Ürün Başarıyla Güncellendi";
        }

        public async Task<string> Patch(UpdateProductDTO product, int productId)
        {
            var productEntity = await _repository.GetAsync(productId);
            if (productEntity == null) return "Ürün bulunamadı";

            if (!product.Name.IsNullOrEmpty()) productEntity.Name = product.Name;
            if (product.Price > 0) productEntity.Price = product.Price;
            if (!product.Description.IsNullOrEmpty()) productEntity.Description = product.Description;
            _repository.Update(productEntity);
            _unitOfWork.Commit();

            return "Ürün Başarıyla Güncellendi";
        }
    }
}
