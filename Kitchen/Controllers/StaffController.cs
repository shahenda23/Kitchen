using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Drawing.Printing;

namespace Kitchen.Controllers
{
    [Authorize(Roles = "1, 2")]
    public class StaffController : Controller
    {
        IStaffRepository staffRepository;
        private const int DefaultPageSize = 3;
        public StaffController(IStaffRepository staffRepo)
        {
            staffRepository = staffRepo;
        }
        public IActionResult All1()
        {
            List<Staff> AllStaff = staffRepository.GetAll();
            List<AllStaffViewModel> viewModel = new List<AllStaffViewModel>();

            foreach (var item in AllStaff)
            {
                 viewModel.Add (new AllStaffViewModel
                 { 
                    Id = item.Id,
                    Name = item.Name,
                    Position = item.Position,
                    Salary = item.Salary,
                    Image=item.Image
                });
            }
            return View("All1" , viewModel);
        }
        public IActionResult Details(int id)
        {
            Staff staff = staffRepository.GetById(id);
            if (staff == null) 
            { 
                return NotFound();  
            }
            return View("Details", staff);
        }
    }
}
