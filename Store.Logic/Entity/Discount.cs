using System;

namespace Store.Logic.Entity
{
    public class Discount
    {
        public string Name { get; set; }
        public decimal EffectiveDiscount { get; set; }

        public virtual bool GetDiscount(Order order)
        {
            return false;
        }
    }

    public class IPhone6Discount : Discount
    {
        public override bool GetDiscount(Order order)
        {
            foreach (var orderDetails in order.OrderDetails)
            {
                if (orderDetails.Quantity == 0 || orderDetails.Product == null) throw new Exception("Invalid Order Details!");
                if (orderDetails.Quantity >= 2 && orderDetails.Product.UniqueCode == "IPhone6")
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class BirthdayDiscount : Discount
    {
        public override bool GetDiscount(Order order)
        {
            if (order.CustomerBirthday != DateTime.MinValue
                    && order.OrderDate != DateTime.MinValue
                    && order.CustomerBirthday.Month == order.OrderDate.Month
                    && order.CustomerBirthday.Day == order.OrderDate.Day)
            {
                return true;
            }
            return false;
        }
    }

    public class SeniorCitizenDiscount : Discount
    {
        public override bool GetDiscount(Order order)
        {
            if (order.CustomerBirthday != DateTime.MinValue
                    && GetAge(order.CustomerBirthday) >= 50)
            {
                return true;
            }
            return false;
        }

        public int GetAge(DateTime birthday)
        {
            var now = DateTime.Today;
            var age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age)) age--;
            return age;
        }
    }

    public class HighValueOrderDiscount : Discount
    {
        public override bool GetDiscount(Order order)
        {
            var price = 0m;
            foreach (var orderDetails in order.OrderDetails)
            {
                if (orderDetails.Quantity == 0 || orderDetails.Product == null) throw new Exception("Invalid Order Details!");
                price += orderDetails.Total;

            }
            if (price > 3000)
            {
                return true;
            }
            return false;
        }
    }
}
