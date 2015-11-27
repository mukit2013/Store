using System.Collections.Generic;
using Store.Logic.Entity;
using Store.Logic.Repository;

namespace Store.Logic.Service
{
    public class DiscountSchemeService : IDiscountSchemeService
    {
        private readonly IDiscountSchemeRepository _iDiscountSchemeRepository;

        public DiscountSchemeService()
        {
            _iDiscountSchemeRepository = new DiscountSchemeRepository();
        }

        public DiscountSchemeService(IDiscountSchemeRepository iDiscountSchemeRepository)
        {
            _iDiscountSchemeRepository = iDiscountSchemeRepository;
        }

        public IEnumerable<DiscountScheme> GetDiscountSchemes()
        {
            var discountSchemes = _iDiscountSchemeRepository.GetDiscountSchemes();
            return discountSchemes;
        }

        public bool SetCurrentDiscountScheme(string discountSchemeName)
        {
            DiscountScheme discountScheme = null;

            if (discountSchemeName == "Single Highest Discount Scheme")
            {
                discountScheme = new SingleHighestDiscountScheme();
            }
            else if (discountSchemeName == "Multiple Discounts Scheme")
            {
                discountScheme = new MultipleDiscountsScheme();
            }

            var result = _iDiscountSchemeRepository.SetCurrentDiscountScheme(discountScheme);
            return result;
        }
    }
}
