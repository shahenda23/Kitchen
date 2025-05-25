using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    [Authorize(Roles = "1, 2, 3, 4")]
    public class DishController : Controller
    {
        IDishRepository DishRepo;
        
        public DishController(IDishRepository _dishRepo)
        {
            DishRepo = _dishRepo;
            
        }
        public IActionResult All()
        {
            List<Dish> DishList = DishRepo.GetAll();
            return View("All", DishList);
        }
        public IActionResult Details(int id)
        {
            var dish = DishRepo.GetById(id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }
        [Authorize(Roles = "2")]
        public IActionResult Add()
        {
            var model = new DishViewModel();

            return View("Create", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "2")]
        public IActionResult Create(DishViewModel DishReq)
        {
            if (DishReq.Name != null)
            {
                string fileName = null;

                if (DishReq.ImageFile != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(DishReq.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        DishReq.ImageFile.CopyTo(stream);
                    }
                }

                Dish newDish = new Dish
                { 
                    Name = DishReq.Name,
                    Price = DishReq.Price,
                    Image = fileName,
                    Description = DishReq.Description,
                    Category = DishReq.Category
                };

                DishRepo.Add(newDish);
                DishRepo.Save();
                return RedirectToAction("All");
            }
            return View("Create", DishReq);
        }
        [Authorize(Roles = "2")]
        public IActionResult Edit(int id)
        {
            Dish dishList = DishRepo.GetById(id);

            DishViewModel DishVM = new();
            DishVM.Id = dishList.Id;
            DishVM.Name = dishList.Name;
            DishVM.Image = dishList.Image;
            DishVM.Price = dishList.Price;
            DishVM.Description = dishList.Description;
            DishVM.Category = dishList?.Category;

            return View("Edit", DishVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "2")]
        public IActionResult Edit(DishViewModel DishReq)
        {
            if (ModelState.IsValid)
            {
                Dish? DishDB = DishRepo.GetById(DishReq.Id);

                if (DishDB == null)
                {
                    return NotFound();
                }

                DishDB.Name = DishReq.Name;
                DishDB.Price = DishReq.Price;
                DishDB.Description = DishReq.Description;
                DishDB.Category = DishReq.Category;

                if (DishReq.ImageFile != null)
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(DishReq.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        DishReq.ImageFile.CopyTo(stream);
                    }


                    DishDB.Image = fileName;
                }
                DishRepo.Edit(DishDB);
                DishRepo.Save();

                return RedirectToAction("All");
            }

            return View("Edit", DishReq);
        }
        [Authorize(Roles = "2")]
        public IActionResult Delete(int id)
        {
            var dish = DishRepo.GetById(id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "2")]
        public IActionResult DeleteConfirmed(int id)
        {
            var dish = DishRepo.GetById(id);
            if (dish == null)
            {
                return NotFound();
            }

            DishRepo.Delete(id);
            DishRepo.Save();

            return RedirectToAction("All");
        }
        public IActionResult Search(string searchString)
        {
            var dishes = DishRepo.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View("All", dishes);
        }
        public IActionResult Filter(string category)
        {
            var dishes = DishRepo.GetAll();
            if (!string.IsNullOrEmpty(category))
            {
                dishes = dishes.Where(d => d.Category == category).ToList();
            }
            return View("All", dishes);
        }
    }
}
