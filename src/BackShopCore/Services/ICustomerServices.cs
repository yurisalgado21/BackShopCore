using BackShopCore.Dto;
using BackShopCore.Models;
using BackShopCore.Utils;

namespace BackShopCore.Services
{
    public interface ICustomerServices
    {
        public IEnumerable<Customer> GetAll(PaginationFilter paginationFilter);
        public ServiceResult<Customer> Add(CustomerDtoRequest customer);
    }
}