using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class DishRepository : IDishRepository
    {
        public KitchenContext context;
        public DishRepository(KitchenContext Context)
        {
            context = Context;
        }
        public void Add(Dish obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            context.Remove(GetById(id));
        }

        public void Edit(Dish obj)
        {
            context.Update(obj);
        }

        public List<Dish> GetAll(string includes = "")
        {
            List<Dish> list;
            if (string.IsNullOrEmpty(includes))
            {
                list = context.Dishes.ToList();

            }
            else
            {
                list = context.Dishes.Include(includes).ToList();
            }
            return list;
        }

        public List<Dish> GetAll()
        {
            throw new NotImplementedException();
        }

        public Dish GetById(int id,string includes = "")
        {
            Dish? obj;
            if (string.IsNullOrEmpty(includes))
            {
                obj = context.Dishes.FirstOrDefault(d => d.Id == id);

            }
            else
            {
                obj = context.Dishes.Include(includes).FirstOrDefault(d => d.Id == id);
            }
            return obj;
        }

        public Dish GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
