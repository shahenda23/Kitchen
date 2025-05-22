using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerRepository customerRepository;
        IAccountRepository accountRepository;
        public CustomerController(ICustomerRepository customerRepo, IAccountRepository accountRepo)
        {
            customerRepository = customerRepo;
            accountRepository = accountRepo;
        }
        public ActionResult All()
        {
            List<Customer> customer = customerRepository.GetAll("Account");
            List<CustomerWithAccountViewModel> CustomerAccounts = new List<CustomerWithAccountViewModel>();

            foreach (Customer customerItem in customer)
            {
                var account = accountRepository.GetById(customerItem.AccountId);

                CustomerAccounts.Add(new CustomerWithAccountViewModel
                {
                    Id = customerItem.Id,
                    Address = customerItem.Address,
                    Name = customerItem.Name,
                    PhoneNumber = customerItem.PhoneNumber,
                    AccountId = customerItem.AccountId,
                    Email = account?.Email ?? string.Empty // Use null-conditional operator and default to empty string
                });
            }

            return View("All", CustomerAccounts);
        }
        public ActionResult GetByPhone(string Number)
        {
            Customer customer = customerRepository.GetByPhoneNumber(Number);

            Account account = accountRepository.GetById(customer.AccountId);

            CustomerWithAccountViewModel viewModel = new CustomerWithAccountViewModel();
            viewModel.Id = customer.Id;
            viewModel.Address = customer.Address;
            viewModel.Name = customer.Name;
            viewModel.PhoneNumber = customer.PhoneNumber;
            viewModel.AccountId = customer.AccountId;
            viewModel.Email = account.Email;

            return View("Search", viewModel);
        }

    }
}