using System.Linq;
using Store.Logic.Entity;
using Store.Logic.Repository;

namespace Store.Logic.Service
{
    public class OrderService : IOrderService
    {
        private readonly IDiscountSchemeRepository _discountSchemeRepository;

        public OrderService()
        {
            _discountSchemeRepository = new DiscountSchemeRepository();
        }

        public OrderService(IDiscountSchemeRepository discountSchemeRepository)
        {
            _discountSchemeRepository = discountSchemeRepository;
        }

        public Order ProcessOrder(Order order)
        {
            order.TotalOrder = GetTotalOrder(order);
            order.TotalDiscount = GetTotalDiscount(order);
            order.TotalDiscountAmount = order.TotalOrder * order.TotalDiscount / 100;
            order.Total = order.TotalOrder - order.TotalDiscountAmount;
            return order;
        }

        private decimal GetTotalDiscount(Order order)
        {
            order.Discounts = _discountSchemeRepository.GetCurrentDiscountScheme().GetDiscountByScheme(order);
            return order.Discounts.Sum(discount => discount.EffectiveDiscount);
        }

        private decimal GetTotalOrder(Order order)
        {
            return order.OrderDetails.Sum(orderDetail => orderDetail.Total);
        }
    }
}
