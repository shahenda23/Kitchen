using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    public class DishController : Controller
    {
        IDishRepository DishRep;
        ICategoryRepository CategoryRep;
        
        public DishController(IDishRepository dishrep , ICategoryRepository categoryRep)
        {
            DishRep = dishrep;
            CategoryRep = categoryRep;
            
        }
        //show all dish

        public IActionResult All()
        {
            var viewModel = new DishListViewModel
            {
                Dishes = DishRep.GetAll(),
                Categories = CategoryRep.GetAll()
            };
            return View("All", viewModel);
            
        }


        public IActionResult Details(int id)
        {
            var dish = DishRep.GetById(id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }
        
        public IActionResult Add()
        {
            var model = new DishViewModel();
            model.Categories = CategoryRep.GetAll();

            return View("Create", model);
        }

        [HttpPost]


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
                    Category_Id = DishReq.CategoryId
                };

                DishRep.Add(newDish);
                DishRep.Save();
                return RedirectToAction("All");
            }
            return View("Create", DishReq);
        }

        public IActionResult Edit(int id)
        {
            Dish dish = DishRep.GetById(id);

            DishViewModel DishVM = new();
            DishVM.Id = dish.Id;
            DishVM.Name = dish.Name;
            DishVM.Image = dish.Image;
            DishVM.Price = dish.Price;
            DishVM.Description = dish.Description;
            DishVM.CategoryId = dish.Category_Id;
            DishVM.Categories= CategoryRep.GetAll();
            return View("Edit", DishVM);
        }
        [HttpPost]
        public IActionResult Edit(DishViewModel DishReq)
        {
            if (ModelState.IsValid)
            {
                Dish? DishDB = DishRep.GetById(DishReq.Id);

                if (DishDB == null)
                {
                    return NotFound();
                }

                DishDB.Name = DishReq.Name;
                DishDB.Price = DishReq.Price;
                DishDB.Description = DishReq.Description;
                DishDB.Category_Id = DishReq.CategoryId;


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
                DishRep.Edit(DishDB);
                DishRep.Save();

                return RedirectToAction("Details", new { id = DishReq.Id });
            }
            DishReq.Categories = CategoryRep.GetAll();

            return View("Edit", DishReq);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dish = DishRep.GetById(id);
            if (dish == null)
            {
                return NotFound();
            }

            DishRep.Delete(dish.Id);
            DishRep.Save();

            return RedirectToAction("All");
        }
        public IActionResult Search(string searchString)
        {
            var dishes = DishRep.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View("All", dishes);
        }
    }
}
