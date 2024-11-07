using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OrderManagement.Business.Services.Abstracts;
using OrderManagement.Core.DTOs.Product;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public List<ProductDTO> GetAllProducts() 
        {
            return _productService.GetAll();
        }

        [HttpPost]
        public ActionResult CreatePorduct(CreateProductRequestDTO createProductRequest)
        {
            _productService.Create(createProductRequest);
            return Ok("Ürün Başarıyla eklendi");
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetProducts(int Id)
        {
            var product = await _productService.GetById(Id);

            if (product == null) return NotFound();
            
            return Ok(product);
        }

        [HttpDelete]
        public ActionResult DeleteProduct(int Id)
        {
            return Ok(_productService.Delete(Id));
        }

        [HttpPut("Id")]
        public async Task<IActionResult> PutProduct([FromBody] UpdateProductDTO productDto,[FromQuery] int productId)
        {
            return Ok(await _productService.Put(productDto, productId));
        }

        [HttpPatch("Id")]
        public async Task<IActionResult> PatchProduct([FromBody] UpdateProductDTO productDto, [FromQuery] int productId)
        {
            return Ok(await _productService.Patch(productDto, productId));
        }

    }

}
