using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel1;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerRepository customerRepository;
        IAccountRepository accountRepository;
        public CustomerController(ICustomerRepository customerRepo)
        {
            customerRepository = customerRepo;
        }

        public ActionResult All()
        {
            List<Customer> customer = customerRepository.GetAll();
            
            return View("All",customer);
        }
        public ActionResult GetByPhone(string Number)
        {
            Customer customer = customerRepository.GetByPhoneNumber(Number);

            Account account = accountRepository.GetById(customer.AccountId);

            CustomerWithAccountViewModel viewModel = new CustomerWithAccountViewModel();
            viewModel.Address = customer.Address;
            viewModel.Name = customer.Name;
            viewModel.PhoneNumber = customer.PhoneNumber;
            viewModel.AccountId = customer.AccountId;
            viewModel.Email= account.Email;
            return View("Search",viewModel);
        }
    }
}
