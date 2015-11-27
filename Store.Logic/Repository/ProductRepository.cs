using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product{UniqueCode = "IPhone6",Name = "IPhone 6",Price = 300},
                new Product{UniqueCode = "IPhone5",Name = "IPhone 5",Price = 200},
                new Product{UniqueCode = "IPad",Name = "IPad",Price = 500},
                new Product{UniqueCode = "Mackbookair ",Name = "Mackbook air ",Price = 1000},
                new Product{UniqueCode = "Macbookpro ",Name = "Macbook pro ",Price = 2000},
            };
        }
    }
}
