using Kitchen.Models;
using Kitchen.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Kitchen.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderrepo;
        public OrderController(IOrderRepository _orderrepo)
        {
            orderrepo = _orderrepo;
        }
       
        public IActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid) {
                orderrepo.Add( order);
                return RedirectToAction("OrderSuccess");
            }
            return View(order);
        }
        public IActionResult ViewOrders(int customerId)
        {
            var orders = orderrepo.GetByCustomer(customerId);
            return View(orders);
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }


    }
}
