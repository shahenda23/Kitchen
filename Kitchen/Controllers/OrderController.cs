using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kitchen.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderrepo;
        IFeedbackRepository feedbackrepo;
        IDishRepository dishrepo;
        public OrderController(IOrderRepository _orderrepo,IFeedbackRepository _feedbackrepo,IDishRepository _dishrepo)
        {
            orderrepo = _orderrepo;
            feedbackrepo = _feedbackrepo;
            dishrepo = _dishrepo;
        }
        public IActionResult All()
        {
            List<Order> order = orderrepo.GetAll();
            return View(order);
        }
        public IActionResult OrdersByCustomer(int customerId)
        {
            var orders = orderrepo.GetByCustomer(customerId); 
            return View(orders);
        }

        
        public IActionResult CreateOrder()
        {
            var orderdishVM = new OrderDishesViewModel
            {
                OrderDetails = dishrepo.GetAll().Select(d => new OrderDetails
                {
                    DishId = d.Id,
                    Price = d.Price
                }).ToList()
            };

            return View(orderdishVM);
        }



        public IActionResult OrderDetails(OrderDishesViewModel Orderfromreq)
        {
            if (ModelState.IsValid) {

                var custVM = new Customer
                {
                    Name = Orderfromreq.customername,
                    Address = Orderfromreq.customeraddress,
                    PhoneNumber = Orderfromreq.customerphone
                };

                var orderVM = new Order
                {
                    Customer = custVM,
                    //Rating = Orderfromreq.orderrate,
                    OrderDetails = Orderfromreq.OrderDetails.Select(d => new OrderDetails
                    {
                        DishId = d.DishId,
                        Price = d.Price,
                        Quantity = 1
                    }).ToList()
                };


                orderrepo.Add(orderVM);
                orderrepo.Save();
                return RedirectToAction("OrderSuccess");
            }
            //Orderfromreq.dishname = dishrepo.GetAll();
            return View(Orderfromreq);
            
        }
        //public IActionResult CreateOrder(Order order)
        //{
        //    var orderdishVM = new OrderDishesViewModel
        //    {
        //        DishNames = dishrepo.GetAll().Select(d => d.Name).ToList() 
        //    };


        //    return View(orderdishVM);
        //}

        //public IActionResult ViewOrders(int customerId)
        //{
        //    var orders = orderrepo.GetByCustomer(customerId);
        //    return View(orders);
        //}
        //public IActionResult OrderSuccess()
        //{
        //    return View();
        //}


    }
}
