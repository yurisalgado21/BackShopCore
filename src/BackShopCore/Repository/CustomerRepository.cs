using BackShopCore.Data;
using BackShopCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BackShopCore.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer GetByEmail(string email)
        {
            var findCustomerByEmail = _dbContext.Customers.AsNoTracking()
                .FirstOrDefault(e => EF.Property<string>(e, "_email") == email);


            if (findCustomerByEmail == null)
            {
                return null!;
            }

            return findCustomerByEmail;
        }
    }
}