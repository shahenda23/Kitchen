using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Kitchen.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderrepo;
        IFeedbackRepository feedbackrepo;
        IDishRepository dishrepo;
        ICustomerRepository custrepo;
        IOrderDetailsRepository orderdetailsrepo;

        public OrderController(IOrderDetailsRepository _orderdetailsrepo,IOrderRepository _orderrepo,
            ICustomerRepository _custrepo, IFeedbackRepository _feedbackrepo, IDishRepository _dishrepo)
        {
            orderrepo = _orderrepo;
            feedbackrepo = _feedbackrepo;
            dishrepo = _dishrepo;
            custrepo = _custrepo;
            orderdetailsrepo = _orderdetailsrepo;
        }


        public IActionResult All()
        {
            List<Order> order = orderrepo.GetAll();
            return View(order);
        }

        public IActionResult SelectDishes()
        {
            var dishes = dishrepo.GetAll();
            return View(dishes);
        }
        public IActionResult OrdersByCustomer(int customerId)
        {
            var orders = orderrepo.GetByCustomer(customerId);
            return View(orders);
        }
        public IActionResult CreateOrder(string orderDetailsJson)
        {
            if (string.IsNullOrEmpty(orderDetailsJson))
            {
                return RedirectToAction("SelectDishes");
            }

            List<OrderDetails> orderDetails = JsonConvert.DeserializeObject<List<OrderDetails>>(orderDetailsJson);

            // Filter out any items with zero quantity
            orderDetails = orderDetails.Where(d => d.Quantity > 0).ToList();

            if (orderDetails.Count == 0)
            {
                return RedirectToAction("SelectDishes");
            }

            // Calculate the total price
            float totalPrice = orderDetails.Sum(d => d.Price * d.Quantity);

            var orderdishVM = new OrderDishesViewModel
            {
                OrderDetails = orderDetails,
                orderprice = totalPrice
            };

            return View(orderdishVM);
        }

        [HttpPost]
        public async Task<IActionResult> OrderDetails(OrderDishesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateOrder", model);
            }
            int customerID = int.Parse(User.FindFirst("ID")?.Value);
            Customer customerDB = custrepo.GetById(customerID);
            customerDB.Name = model.customername;
            customerDB.PhoneNumber = model.customerphone;
            customerDB.Address = model.customeraddress;
            custrepo.Edit(customerDB);
            custrepo.Save();

            // 2. فك JSON تفاصيل الطلب
            var orderDetailsItems = JsonConvert.DeserializeObject<List<OrderDetailsItemViewModel>>(model.OrderDetailsJson);

            // 3. حساب إجمالي السعر
            var totalPrice = orderDetailsItems.Sum(item => item.Price * item.Quantity);

            // 4. تسجيل الطلب
            var order = new Order
            {
                CustomerId = customerDB.Id,
                Date = DateTime.Now,
                TotalPrice = totalPrice,
                Status = "Pending"
            };
            orderrepo.Add(order);
            orderrepo.Save();

            // 5. تسجيل تفاصيل الأطباق
            foreach (var item in orderDetailsItems)
            {
                var orderDetails = new OrderDetails
                {
                    DishId = item.DishId,
                    OrderId = order.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                orderdetailsrepo.Add(orderDetails);
            }
            orderdetailsrepo.Save();

            return RedirectToAction("OrderSuccess");
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}