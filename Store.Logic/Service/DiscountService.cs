using System;
using System.Collections.Generic;
using System.Linq;
using Store.Logic.Entity;
using Store.Logic.Repository;

namespace Store.Logic.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService()
        {
            _discountRepository = new DiscountRepository();
        }

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public List<Discount> GetAllMatchedDiscounts(Order order)
        {
            if (order == null) throw new ArgumentNullException();
            if (order.OrderDetails == null || !order.OrderDetails.Any()) throw new Exception("No Order Details!");

            var discounts = _discountRepository.GetDiscounts();

            var effectiveDiscounts = new List<Discount>();
            foreach (var discount in discounts)
            {
                if (discount.GetDiscount(order))
                {
                    effectiveDiscounts.Add(discount);
                }
            }

            return effectiveDiscounts;
        }
    }
}
