using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using product_service.Cache;
using product_service.Models;


namespace product_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductCache _cache;

        string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

        public ProductController(ILogger<ProductController> logger, IProductCache productCache)
        {
            _logger = logger;
            _cache = productCache;
        }

        [HttpGet("{category}")]
        public List<Product> GetProductsInCategory(string category)
        {
            var products = _cache.GetProducts(category);
            if (products == null)
            {
                products = new List<Product>();

                var connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "SELECT Id, ProductName, ProductCategory FROM Products";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Id = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        ProductCategory = reader.GetString(2)
                    };
                    products.Add(product);
                    Console.WriteLine($"Id: {product.Id}, Name: {product.ProductName}, Category: {product.ProductCategory}");
                    _cache.SetCache(category, products);
                }
            }

            return products;
        }
    }
}
