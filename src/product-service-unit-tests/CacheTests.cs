using Moq;
using product_service.Cache;
using product_service.Models;

namespace product_service_unit_tests
{
    public class CacheTests
    {
        [Fact(DisplayName = "Test 1 - Can Set Product Cache")]
        public void Test1CanSetProductCache()
        {
            var mockProductCache = new Mock<IProductCache>();
            var categoryName = "Electronics";
            var products = new List<Product>
            {
                new Product { Id = 1, ProductName = "Phone", ProductCategory = "Electronics" },
                new Product { Id = 2, ProductName = "Laptop", ProductCategory = "Electronics" }
            };

            mockProductCache.Object.SetCache(categoryName, products);

            mockProductCache.Verify(cache => cache.SetCache(categoryName, products), Times.Once);
        }

        [Fact(DisplayName = "Test 2 - Can Get Products From Cache")]
        public void Test2CanGetProductsFromCache()
        {
            var mockProductCache = new Mock<IProductCache>();
            var categoryName = "Electronics";
            var products = new List<Product>
            {
                new Product { Id = 1, ProductName = "Phone", ProductCategory = "Electronics" },
                new Product { Id = 2, ProductName = "Laptop", ProductCategory = "Electronics" }
            };
            mockProductCache.Setup(cache => cache.GetProducts(categoryName)).Returns(products);

            var result = mockProductCache.Object.GetProducts(categoryName);

            Assert.Equal(products, result);
            mockProductCache.Verify(cache => cache.GetProducts(categoryName), Times.Once);
        }
    }
}