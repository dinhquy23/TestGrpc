using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly GrpcChannel _channel;
        readonly ProductsService.ProductsServiceClient _productsServiceClient;
        public ProductsController()
        {
            _channel = GrpcChannel.ForAddress("http://localhost:5000");
            _productsServiceClient = new ProductsService.ProductsServiceClient(_channel);
        }
        //private ProductsService.ProductsServiceClient Schannel1()
        //{
        //    using var channel = GrpcChannel.ForAddress("http://localhost:5000");
        //    return new ProductsService.ProductsServiceClient(channel);
        //}
        [HttpGet("get-product")]
        public IActionResult Get()
        {
            return Ok(_productsServiceClient.GetAll(new Empty()).Items);
        }

        //[HttpPost("post-product")]

        //public IActionResult Post(ProductDto productDto)
        //{
        //    Product product = new Product();
        //    product.Manufacturer = productDto.Manufacturer;
        //    product.ProductId = productDto.Productid;
        //    product.ProductName = productDto.Productname;
        //    product.CategoryName = productDto.Categoryname;
        //    product.Price = productDto.Price;

        //    return Ok(_productsServiceClient.Post(product));
        //}
    }
}
