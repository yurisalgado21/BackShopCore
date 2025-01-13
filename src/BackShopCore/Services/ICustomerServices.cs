using BackShopCore.Dto;
using BackShopCore.Models;
using BackShopCore.Utils;

namespace BackShopCore.Services
{
    public interface ICustomerServices
    {
        public bool VerifyDateOfBirth(DateTime dateOfBirth);
        public List<string> CheckForDuplicateEmails(IEnumerable<CustomerDtoRequest> customersDtoRequests);
        public IEnumerable<Customer> GetAll(PaginationFilter paginationFilter);
        public Customer GetByEmail(string email);
        public ServiceResult<Customer> GetById(int id);
        public ServiceResult<Customer> Add(CustomerDtoRequest customerDtoRequest);
        public ServiceResult<IEnumerable<Customer>> AddBulk(IEnumerable<CustomerDtoRequest> customersDtoRequest);
        public ServiceResult<Bulk2ImportCustomersResponse> AddBulk2(IEnumerable<CustomerDtoRequest> customersDtoRequest);
        public ServiceResult<Customer> Update(int id, CustomerDtoRequest customerDtoRequest);
        public ServiceResult<Customer> Delete(int id);
    }
}