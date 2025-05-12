using Kitchen.Models;

namespace Kitchen.Repository
{
    public class OrderDetailsRepository: IOrderDetailsRepository
    {
        public KitchenContext context;
        public OrderDetailsRepository(KitchenContext context)
        {
            context = context;
        }

        public void Add(OrderDetails obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(OrderDetails obj)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetails> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderDetails GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
    
}
