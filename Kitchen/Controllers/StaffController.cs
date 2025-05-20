using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Drawing.Printing;

namespace Kitchen.Controllers
{
    public class StaffController : Controller
    {
        IStaffRepository staffRepository;
        private const int DefaultPageSize = 3;
        public StaffController(IStaffRepository staffRepo)
        {
            staffRepository = staffRepo;
        }
        //public IActionResult All(int page=1 , int PageSize = DefaultPageSize)
        //{
        //    var AllStaff = staffRepository.GetAll();
        //    var StaffCount = AllStaff.Count();
        //    var TotalPages = (int)Math.Ceiling((decimal) StaffCount / PageSize);

        //    var staff = AllStaff
        //        .Skip((page-1)*PageSize)
        //        .Take(PageSize)
        //        .Select(s => new AllStaffViewModel
        //        {
        //            Id = s.Id,
        //            Name = s.Name,
        //            Position = s.Position,
        //            Salary = s.Salary
        //        })
        //        .ToList();

        //    AllStaffWithPagesViewModel viewModel = new()
        //    {
        //        allStaff = staff,
        //        PageSize = PageSize,    
        //        TotalPages = TotalPages,
        //        CurrentPage = page
        //    };

        //    return View("All", viewModel);
        //}
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
                    Salary = item.Salary
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
