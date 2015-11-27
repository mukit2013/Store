using System.Collections.Generic;
using Store.Logic.Service;

namespace Store.Logic.Entity
{
    public abstract class DiscountScheme
    {
        public string Name { get; set; }

        public abstract List<Discount> GetDiscountByScheme(Order order);
    }

    public class SingleHighestDiscountScheme : DiscountScheme
    {
        public override List<Discount> GetDiscountByScheme(Order order)
        {
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Discount discountTmp = null;
            foreach (var discount in discounts)
            {
                discountTmp = discount;
                if (discount.EffectiveDiscount > discountTmp.EffectiveDiscount)
                {
                    discountTmp = discount;
                }
            }
            if (discountTmp != null)
            {
                return new List<Discount> { discountTmp };
            }
            else
            {
                return new List<Discount>();
            }
        }
    }

    public class MultipleDiscountsScheme : DiscountScheme
    {
        public override List<Discount> GetDiscountByScheme(Order order)
        {
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            return discounts;
        }
    }
}
