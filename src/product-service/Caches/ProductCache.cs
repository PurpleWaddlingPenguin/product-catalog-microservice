using product_service.Models;


namespace product_service.Cache
{
    public interface IProductCache
    {
        List<Product> GetProducts(string categoryName);
        void SetCache(string categoryName, List<Product> value);
    }

    public class ProductCache : IProductCache
    {
        readonly Dictionary<string, List<Product>> products = [];

        public List<Product> GetProducts(string categoryName)
        {
            return products[categoryName];
        }

        public void SetCache(string categoryName, List<Product> value)
        {
            products.Remove(categoryName);
            products.Add(categoryName, value);
        }
    }
}
