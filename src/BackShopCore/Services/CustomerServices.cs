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

            var findCustomer = GetByEmail(email: customerDtoRequest.Email);

            if (findCustomer != null) return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.EmailExistsError, 409);

            var newCustomer = Customer.RegisterNew
            (
                firstName: customerDtoRequest.FirstName,
                lastName: customerDtoRequest.LastName,
                email: customerDtoRequest.Email,
                dateOfBirth: customerDtoRequest.DateOfBirth
            );

            if (!newCustomer.IsValid)
            {
                return ServiceResult<Customer>.ErrorResult(message: "Customer is not valid", 400);
            }

            _customerRepository.Add(entity: newCustomer);
            return ServiceResult<Customer>.SuccessResult(data: newCustomer, statusCode: 201);
        }

        public IEnumerable<Customer> GetAll(PaginationFilter paginationFilter)
        {
            var customers = _customerRepository.GetAll(paginationFilter: paginationFilter);

            return customers;
        }

        public Customer GetByEmail(string email)
        {
            var findCustomer = _customerRepository.GetByEmail(email: email);

            return findCustomer;
        }

        public ServiceResult<Customer> GetById(int id)
        {
            var findCustomer = _customerRepository.GetById(id: id);

            if (findCustomer == null)
            {
                return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.CustomerNotFoundMessage, 404);
            }

            var customer = Customer.SetExistingInfo
            (
                customerId: findCustomer.CustomerId,
                firstName: findCustomer.FirstName,
                lastName: findCustomer.LastName,
                email: findCustomer.Email,
                dateOfBirth: findCustomer.DateOfBirth
            );

            return ServiceResult<Customer>.SuccessResult
            (
                data: customer
            );
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