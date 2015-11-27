using System;
using System.Collections.Generic;

namespace Store.Logic.Entity
{
    public class Order
    {
        //input
        public DateTime OrderDate { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        //output
        public decimal TotalDiscount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public List<Discount> Discounts { get; set; }
        public decimal TotalOrder { get; set; }
        public decimal Total { get; set; }
    }
}
