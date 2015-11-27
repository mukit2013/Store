using System.Collections.Generic;
using Store.Logic.Entity;
using Store.Logic.Repository;

namespace Store.Logic.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = _productRepository.GetProducts();
            return products;
        }
    }
}
