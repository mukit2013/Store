using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Logic.Entity;
using Store.Logic.Repository;
using Store.Logic.Service;

namespace Store.Test
{
    [TestClass]
    public class DiscountTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DiscountTestForArgumentNull()
        {
            new DiscountService().GetAllMatchedDiscounts(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No Order Details!")]
        public void DiscountTestForNoOrderDetails()
        {
            var order = new Order();
            new DiscountService().GetAllMatchedDiscounts(order);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No Order Details!")]
        public void DiscountTestForNoProduct()
        {
            var order = new Order
            {
                OrderDetails = new List<OrderDetail>()
            };
            new DiscountService().GetAllMatchedDiscounts(order);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid Order Details!")]
        public void DiscountTestForInvalidOrderDetailsQuentityZero()
        {
            var order = new Order
            {
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product(),Quantity = 0}
                }
            };
            new DiscountService().GetAllMatchedDiscounts(order);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid Order Details!")]
        public void DiscountTestForInvalidOrderDetailsProductNull()
        {
            var order = new Order
            {
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Quantity = 10}
                }
            };
            new DiscountService().GetAllMatchedDiscounts(order);
        }

        [TestMethod]
        public void Iphone6DiscountTestForValidProduct()
        {
            var order = new Order
            {
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6"}, Quantity = 2}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNotNull(discounts.OfType<IPhone6Discount>().FirstOrDefault());
        }

        [TestMethod]
        public void Iphone6DiscountTestForInvalidProduct()
        {
            var order = new Order
            {
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6"}, Quantity = 1}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNull(discounts.OfType<IPhone6Discount>().FirstOrDefault());
        }

        [TestMethod]
        public void BirthdayDiscountTestForValidProduct()
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerBirthday = DateTime.Now,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6"}, Quantity = 5}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNotNull(discounts.OfType<BirthdayDiscount>().FirstOrDefault());
        }

        [TestMethod]
        public void BirthdayDiscountTestForInvalidProduct()
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerBirthday = DateTime.Now.AddDays(1),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6"}, Quantity = 5}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNull(discounts.OfType<BirthdayDiscount>().FirstOrDefault());
        }

        [TestMethod]
        public void SeniorCitizenDiscountTestForValidProduct()
        {
            var order = new Order
            {
                CustomerBirthday = new DateTime(1965, 11, 21),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6"}, Quantity = 5}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNotNull(discounts.OfType<SeniorCitizenDiscount>().FirstOrDefault());
        }

        [TestMethod]
        public void SeniorCitizenDiscountTestForInvalidProduct()
        {
            var order = new Order
            {
                CustomerBirthday = new DateTime(1965, 12, 22),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6"}, Quantity = 5}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNull(discounts.OfType<SeniorCitizenDiscount>().FirstOrDefault());
        }

        [TestMethod]
        public void HighValueOrderDiscountTestForValidProduct()
        {
            var order = new Order
            {
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6", Price = 1000}, Quantity = 5}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNotNull(discounts.OfType<HighValueOrderDiscount>().FirstOrDefault());
        }

        [TestMethod]
        public void HighValueOrderDiscountTestForInvalidProduct()
        {
            var order = new Order
            {
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6", Price = 100}, Quantity = 5}
                }
            };
            var discounts = new DiscountService().GetAllMatchedDiscounts(order);
            Assert.IsNull(discounts.OfType<HighValueOrderDiscount>().FirstOrDefault());
        }

        [TestMethod]
        public void DiscountTestForSingleHighestDiscount()
        {
            var order = new Order
            {
                OrderDate = DateTime.Today,
                CustomerBirthday = new DateTime(1960, 11, 22),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6", Price = 3000}, Quantity = 2}
                }
            };
            //Factory.CurrentDiscountScheme = new SingleHighestDiscountScheme();
            new DiscountSchemeRepository().SetCurrentDiscountScheme(new SingleHighestDiscountScheme());
            new OrderService().ProcessOrder(order);
            Assert.IsNotNull(order.Discounts.OfType<HighValueOrderDiscount>().FirstOrDefault());
            Assert.AreEqual(1, order.Discounts.Count);
        }

        [TestMethod]
        public void DiscountTestForMultipleDiscount()
        {
            //Factory.CurrentDiscountScheme = new MultipleDiscountsScheme();
            new DiscountSchemeRepository().SetCurrentDiscountScheme(new MultipleDiscountsScheme());

            var order = new Order
            {
                OrderDate = DateTime.Today,
                CustomerBirthday = new DateTime(1960, DateTime.Today.Month, DateTime.Today.Day),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail{ Product = new Product{UniqueCode = "IPhone6", Price = 3000}, Quantity = 2}
                }
            };
            new OrderService().ProcessOrder(order);
            Assert.AreEqual(4, order.Discounts.Count);
        }
    }
}
