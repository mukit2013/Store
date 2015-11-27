using System;
using System.Collections.Generic;
using System.Web.Http;
using Store.Logic.Entity;
using Store.Logic.Service;
using Store.Web.Models;

namespace Store.Web.Api_Service
{
    public class StoreController : ApiController
    {
        private readonly IProductService _iProductService;
        private readonly IOrderService _iOrderService;
        private readonly IDiscountSchemeService _iDiscountSchemeService;

        public StoreController()
        {
            _iDiscountSchemeService = new DiscountSchemeService();
            _iProductService = new ProductService();
            _iOrderService = new OrderService();
        }

        public StoreController(IProductService iProductService, IOrderService iOrderService, IDiscountSchemeService iDiscountSchemeService)
        {
            _iProductService = iProductService;
            _iOrderService = iOrderService;
            _iDiscountSchemeService = iDiscountSchemeService;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = _iProductService.GetProducts();
            return products;
        }

        public IEnumerable<DiscountScheme> GetDiscountSchemes()
        {
            var discountSchemes = _iDiscountSchemeService.GetDiscountSchemes();
            return discountSchemes;
        }

        [HttpPost]
        public Order CalculateOrder(OrderWithDiscountScheme orderWithDiscountScheme)
        {
            _iDiscountSchemeService.SetCurrentDiscountScheme(orderWithDiscountScheme.DiscountSchemeName);
            orderWithDiscountScheme.Order.OrderDate = DateTime.Today;
            _iOrderService.ProcessOrder(orderWithDiscountScheme.Order);
            return orderWithDiscountScheme.Order;
        }
    }
}
