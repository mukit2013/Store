using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Repository
{
    public class DiscountSchemeRepository : IDiscountSchemeRepository
    {
        private static DiscountScheme _currentDiscountScheme = new SingleHighestDiscountScheme();

        public DiscountScheme GetCurrentDiscountScheme()
        {
            return _currentDiscountScheme;
        }

        public bool SetCurrentDiscountScheme(DiscountScheme discountScheme)
        {
            _currentDiscountScheme = discountScheme;
            return true;
        }

        public List<DiscountScheme> GetDiscountSchemes()
        {
            return new List<DiscountScheme>
            {
                new SingleHighestDiscountScheme{Name = "Single Highest Discount Scheme"},
                new MultipleDiscountsScheme{Name = "Multiple Discounts Scheme"}
            };
        }
    }
}
