using Kitchen.Models;
using Kitchen.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Kitchen.Controllers
{
    public class StaffController : Controller
    {
        IStaffRepository staffRepository;
        public StaffController(IStaffRepository staffRepo)
        {
            staffRepository = staffRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult All()
        {
            List<Staff> staffs = staffRepository.GetAll();

            return View("All",staffs);
        }


    }
}
