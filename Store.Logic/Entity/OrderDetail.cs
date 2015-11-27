
namespace Store.Logic.Entity
{
    public class OrderDetail
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public decimal Total
        {
            get { return Product.Price * Quantity; }
        }
    }
}
