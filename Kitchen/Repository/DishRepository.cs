using Kitchen.Models;

namespace Kitchen.Repository
{
    public class DishRepository : IDishRepository
    {
        public KitchenContext context;
        public DishRepository(KitchenContext context)
        {
            context = context;
        }
        public void Add(Dish obj)
        {
            context.Dishes.Add(obj);
        }

        public void Delete(int id)
        {
            context.Dishes.Remove(GetById(id));
        }

        public void Edit(Dish obj)
        {
            context.Dishes.Update(obj);
        }

        public List<Dish> GetAll()
        {
            throw new NotImplementedException();
        }

        public Dish GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
