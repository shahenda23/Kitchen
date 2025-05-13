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
        public OrderController(IOrderRepository _orderrepo,IFeedbackRepository _feedbackrepo)
        {
            orderrepo = _orderrepo;
            feedbackrepo = _feedbackrepo;
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
                    OrderDetails = new List<OrderDetails> 
                    {
                        new OrderDetails
                        {
                            DishId = Orderfromreq.dishid,
                            Price = Orderfromreq.dishprice
                        }
                    }
                };


                orderrepo.Add(orderVM);
                orderrepo.Save();
                return RedirectToAction("OrderSuccess");
            }
            Orderfromreq.dishname = dishrepo.GetAll();
            return View();
            
        }

        //public IActionResult CreateOrder(Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        orderrepo.Add( order);
        //        return RedirectToAction("OrderSuccess");
        //    }
        //    return View(order);
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
