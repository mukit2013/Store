using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Service
{
    public interface IDiscountSchemeService
    {
        IEnumerable<DiscountScheme> GetDiscountSchemes();
        bool SetCurrentDiscountScheme(string discountScheme);
    }
}
