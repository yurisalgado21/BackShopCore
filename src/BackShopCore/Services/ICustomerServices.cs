using BackShopCore.Dto;
using BackShopCore.Models;
using BackShopCore.Utils;

namespace BackShopCore.Services
{
    public interface ICustomerServices
    {
        public bool VerifyDateOfBirth(DateTime dateOfBirth);
        public IEnumerable<Customer> GetAll(PaginationFilter paginationFilter);
        public ServiceResult<Customer> Add(CustomerDtoRequest customerDtoRequest);
    }
}