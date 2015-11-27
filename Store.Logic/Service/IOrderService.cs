using Store.Logic.Entity;

namespace Store.Logic.Service
{
    public interface IOrderService
    {
        Order ProcessOrder(Order order);
    }
}
