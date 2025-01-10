using BackShopCore.Dto;
using BackShopCore.Models;
using BackShopCore.Repository;
using BackShopCore.Utils;

namespace BackShopCore.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ServiceResult<Customer> Add(CustomerDtoRequest customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll(PaginationFilter paginationFilter)
        {
            var customers = _customerRepository.GetAll(paginationFilter: paginationFilter);

            return customers;
        }
    }
}