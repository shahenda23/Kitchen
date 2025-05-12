using Kitchen.Models;

namespace Kitchen.Repository
{
    public class AccountRepository : IAccountRepository
    {
        KitchenContext context;
        public AccountRepository(KitchenContext _Ctx)
        {
            context = _Ctx;
        }
        public List<Account> GetAll()
        {
            return context.Accounts.ToList();
        }
        public Account GetById(int id)
        {
            return ;
        }
        public Account GetOne(string username, string password)
        {
            throw new NotImplementedException();
        }
        public void Add(Account obj)
        {
            throw new NotImplementedException();
        }
        public void Edit(Account obj)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
