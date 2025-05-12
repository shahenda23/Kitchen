using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        KitchenContext context;
        public FeedbackRepository(KitchenContext _ctx)
        {
            context = _ctx;
        }
        public void Add(Feedback obj)
        {
            context.Feedbacks.Add(obj);      
        }
       

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Feedback obj)
        {
            throw new NotImplementedException();
        }

        public List<Feedback> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Feedback> GetByCustomer(int customerId)
        {
            return context.Feedbacks
                   .Where(f => f.Customer_ID == customerId)
                   .Include(f => f.Customer) 
                   .ToList();
        }

        public Feedback GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Feedback> GetByOrder(int orderId)
        {
            return context.Feedbacks.Where(f => f.OrderId == orderId).ToList();
        }
       
        public void Save()
        {
            context.SaveChanges();        
        }
    }
}
