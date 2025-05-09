using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    public class DishController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
