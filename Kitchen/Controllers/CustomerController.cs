using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    //[Authorize(Roles = "4")]
    public class CustomerController : Controller
    {
        ICustomerRepository customerRepo;
        IOrderRepository orderRepo;
        IOrderDetailsRepository orderdetailsRepo;
        public CustomerController(ICustomerRepository _customerRepo, IOrderRepository _orderRepo, IOrderDetailsRepository _orderdetailsRepo)
        {
            customerRepo = _customerRepo;
            orderRepo = _orderRepo;
            orderdetailsRepo = _orderdetailsRepo;
        }
        public IActionResult Profile()
        {
            int customerID = int.Parse(User.FindFirst("CustomerID")?.Value);
            Customer customerDB = customerRepo.GetById(customerID);
            IEnumerable<Order> orderDB = orderRepo.GetByCustomer(customerID);
            if (customerDB != null)
            {
                ProfileViewModel ProfileVM = new ProfileViewModel()
                {
                    customerName = customerDB.Name,
                    customerAddress = customerDB.Address,
                    customerPhone = customerDB.PhoneNumber,
                    Orders = orderDB,
                };
                return View("Profile", ProfileVM);
            }
            return NotFound();
        }
    }
}