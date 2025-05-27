using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Kitchen.Controllers
{
    [Authorize(Roles = "1, 2, 3, 4, 5")]
    public class OrderController : Controller
    {
        IOrderRepository orderrepo; 
        ICustomerRepository custrepo;
        IOrderDetailsRepository orderdetailsrepo;

        public OrderController(IOrderDetailsRepository _orderdetailsrepo,
            IOrderRepository _orderrepo, ICustomerRepository _custrepo)
        {
            orderrepo = _orderrepo;
            custrepo = _custrepo;
            orderdetailsrepo = _orderdetailsrepo;
        }
        [Authorize(Roles = "1,2,3,4 ")]
        public IActionResult All()
        {
            List<Order> order = orderrepo.GetAll("Customer");
            return View(order);
        }
        public IActionResult CreateOrder(string orderDetailsJson)
        {
            if (string.IsNullOrEmpty(orderDetailsJson))
            {
                return RedirectToAction("All", "Dish");
            }

            List<OrderDetails> orderDetails = JsonConvert.DeserializeObject<List<OrderDetails>>(orderDetailsJson);

            // Filter out any items with zero quantity
            orderDetails = orderDetails.Where(d => d.Quantity > 0).ToList();

            if (orderDetails.Count == 0)
            {
                return RedirectToAction("All", "Dish");
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
        public IActionResult OrderDetails(int id)
        {
            var order = orderrepo.GetById(id);
            if (order == null)
                return NotFound();

            var model = new OrderDishesViewModel
            {
                customername = order.Customer?.Name,
                customeraddress = order.Customer?.Address,
                customerphone = order.Customer?.PhoneNumber,
                orderprice = order.TotalPrice,
                orderStatus = order.Status,
                OrderDetails = order.OrderDetails
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetails(OrderDishesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateOrder", model);
            }
            int customerID = int.Parse(User.FindFirst("CustomerID")?.Value);
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

            HttpContext.Session.SetInt32("OrderID", order.Id);

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
            return RedirectToAction("Profile", "Customer");
        }
        public IActionResult Search(string searchString)
        {
            var orders = orderrepo.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(d => d.Customer != null && (
                            d.Customer.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            d.Customer.PhoneNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                            )).ToList();
            }
            return View("All", orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderStatus(int orderId)
        {
            var order = orderrepo.GetById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            // Update status to "Delivered"
            order.Status = "Delivered";
            orderrepo.Edit(order);
            orderrepo.Save();

            // Redirect to Create Feedback action
            return RedirectToAction("CreateFeedback", "Feedback", new { orderId = orderId });
        }
    }
}