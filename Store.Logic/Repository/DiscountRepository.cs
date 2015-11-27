using System.Collections.Generic;
using Store.Logic.Entity;

namespace Store.Logic.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        public List<Discount> GetDiscounts()
        {
            return new List<Discount>
            {
                new IPhone6Discount
                {
                    Name = "IPhone 6 Discount",
                    EffectiveDiscount = 5
                },
                new BirthdayDiscount
                {
                    Name = "Birthday Discount",
                    EffectiveDiscount = 10
                },
                new SeniorCitizenDiscount
                {
                    Name = "Senior Citizen Discount",
                    EffectiveDiscount = 15
                },
                new HighValueOrderDiscount
                {
                    Name = "High Value Order Discount",
                    EffectiveDiscount = 20
                }
            };
        }
    }
}
