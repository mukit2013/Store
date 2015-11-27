using Store.Logic.Entity;

namespace Store.Web.Models
{
    public class OrderWithDiscountScheme
    {
        public Order Order { get; set; }
        public string DiscountSchemeName { get; set; }
    }
}