using BackShopCore.Dto;
using BackShopCore.Models;
using BackShopCore.Utils;

namespace BackShopCore.Services
{
    public interface ICustomerServices
    {
        public bool VerifyDateOfBirth(DateTime dateOfBirth);
        public IEnumerable<Customer> GetAll(PaginationFilter paginationFilter);
        public Customer GetByEmail(string email);
        public ServiceResult<Customer> GetById(int id);
        public ServiceResult<Customer> Add(CustomerDtoRequest customerDtoRequest);
        public ServiceResult<Customer> Update(int id, CustomerDtoRequest customerDtoRequest);
        public ServiceResult<Customer> Delete(int id);
    }
}