using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Service
{
    public interface IDiscountService
    {
        List<Discount> GetAllMatchedDiscounts(Order order);
    }
}
