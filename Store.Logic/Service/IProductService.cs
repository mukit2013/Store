using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}
