using Kitchen.Models;

namespace Kitchen.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByPhoneNumber(string phoneNumber, string Includes="");
    }
}
