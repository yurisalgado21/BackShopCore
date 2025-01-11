using BackShopCore.Models;

namespace BackShopCore.Repository
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        public Customer GetByEmail(string email);
    }
}