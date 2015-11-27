using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Repository
{
    public interface IDiscountSchemeRepository
    {
        DiscountScheme GetCurrentDiscountScheme();
        bool SetCurrentDiscountScheme(DiscountScheme discountScheme);
        List<DiscountScheme> GetDiscountSchemes();
    }
}
