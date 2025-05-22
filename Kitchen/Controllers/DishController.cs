using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    public class DishController : Controller
    {
        IDishRepository DishRep;
        
        public DishController(IDishRepository dishrep)
        {
            DishRep = dishrep;
            
        }
        //show all dish
        public IActionResult All()
        {
            List<Dish> DishList = DishRep.GetAll();
            return View("All", DishList);
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
                    Category = DishReq.Category
                };

                DishRep.Add(newDish);
                DishRep.Save();
                return RedirectToAction("All");
            }
            return View("Create", DishReq);
        }

        public IActionResult Edit(int id)
        {
            Dish dishList = DishRep.GetById(id);

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


                DishRep.Edit(DishDB);
                DishRep.Save();

                return RedirectToAction("All");
            }

            return View("Edit", DishReq);
        }
        public IActionResult Delete(int id)
        {
            var dish = DishRep.GetById(id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dish = DishRep.GetById(id);
            if (dish == null)
            {
                return NotFound();
            }

            DishRep.Delete(id);
            DishRep.Save();

            return RedirectToAction("Index");
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
        public IActionResult Filter(string category)
        {
            var dishes = DishRep.GetAll();
            if (!string.IsNullOrEmpty(category))
            {
                dishes = dishes.Where(d => d.Category == category).ToList();
            }
            return View("All", dishes);
        }
    }
}
