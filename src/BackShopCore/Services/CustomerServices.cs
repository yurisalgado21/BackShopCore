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

        public ServiceResult<Customer> Add(CustomerDtoRequest customerDtoRequest)
        {
            var dateIsNotValid = VerifyDateOfBirth(dateOfBirth: customerDtoRequest.DateOfBirth);
            if (dateIsNotValid) return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.DateOfBirthError, statusCode: 400);

            var newCustomer = Customer.RegisterNew
            (
                firstName: customerDtoRequest.FirstName,
                lastName: customerDtoRequest.LastName,
                email: customerDtoRequest.Email,
                dateOfBirth: customerDtoRequest.DateOfBirth
            );

            _customerRepository.Add(entity: newCustomer);
            return ServiceResult<Customer>.SuccessResult(data: newCustomer, statusCode: 201);
        }

        public IEnumerable<Customer> GetAll(PaginationFilter paginationFilter)
        {
            var customers = _customerRepository.GetAll(paginationFilter: paginationFilter);

            return customers;
        }

        public bool VerifyDateOfBirth(DateTime dateOfBirth)
        {
            var dateNow = DateTime.UtcNow;

            if (dateOfBirth.ToUniversalTime().Date > dateNow.Date)
            {
                return true;
            }

            return false;
        }
    }
}