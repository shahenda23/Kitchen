using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class OrderRepository : IOrderRepository
    {
        KitchenContext context;
        public OrderRepository(KitchenContext _ctx)
        {
            context = _ctx;
        }
        public void Add(Order obj)
        {
            context.Orders.Add(obj);

        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Edit(Order obj)
        {
            throw new NotImplementedException();
        }
        public List<Order> GetAll(string includes = "")
        {
            List<Order> list;
            if (string.IsNullOrEmpty(includes))
            {
                list = context.Orders.ToList();

            }
            else
            {
                list = context.Orders.Include(includes).ToList();
            }
            return list;
        }
        public IEnumerable<Order> GetByCustomer(int customerId)
        {
            return context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Dish)
                .ToList();
        }
        public Order GetById(int id, string includes = "")
        {
            return context.Orders.Include(o => o.Feedbacks).FirstOrDefault(o => o.Id == id);
        }
        public void Save()
        {
            context.SaveChanges();      
        }
    }
}
