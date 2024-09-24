using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly IProductQuery _productQuery;

        //public ProductController(IProductQuery productQuery)
        //{
        //    _productQuery = productQuery;
        //}

        //[HttpGet]
        //public List<ProductQueryModel> GetProducts()
        //{
        //    return _productQuery.GetProducts();
        //}
    }
}
