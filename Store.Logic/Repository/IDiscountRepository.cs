using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Repository
{
    public interface IDiscountRepository
    {
        List<Discount> GetDiscounts();
    }
}
