using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class AccountRepository : IAccountRepository
    {
        KitchenContext context;
        public AccountRepository(KitchenContext _Ctx)
        {
            context = _Ctx;
        }
        public List<Account> GetAll(string inclues = "")
        {
            if(inclues == "")
            {
                return context.Accounts.ToList();
            }
            else
            {
                return context.Accounts.Include(inclues).ToList();
            }
        }
        public Account GetById(int id)
        {
            return context.Accounts.FirstOrDefault(a=>a.Id==id);
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

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
