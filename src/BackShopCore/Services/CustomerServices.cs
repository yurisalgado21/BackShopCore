using BackShopCore.Data;
using BackShopCore.Dto;
using BackShopCore.Models;
using BackShopCore.Repository;
using BackShopCore.Utils;
using Microsoft.EntityFrameworkCore;

namespace BackShopCore.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IApplicationDbContext _dbContext;

        public CustomerServices(ICustomerRepository customerRepository, IApplicationDbContext dbContext)
        {
            _customerRepository = customerRepository;
            _dbContext = dbContext;
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
                return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.CustomerIsNotValid, 422);
            }

            _customerRepository.Add(entity: newCustomer);
            return ServiceResult<Customer>.SuccessResult(data: newCustomer, statusCode: 201);
        }

        public ServiceResult<Customer> Delete(int id)
        {
            var findCustomer = _customerRepository.GetById(id: id);

            if (findCustomer == null)
            {
                return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.CustomerNotFoundMessage, statusCode: 404);
            }

            _customerRepository.Delete(id: id);

            return ServiceResult<Customer>.SuccessResult(data: findCustomer, statusCode: 204); 
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

        public ServiceResult<Customer> Update(int id, CustomerDtoRequest customerDtoRequest)
        {
            var dateIsNotValid = VerifyDateOfBirth(dateOfBirth: customerDtoRequest.DateOfBirth);
            if (dateIsNotValid) return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.DateOfBirthError, statusCode: 422);

            var findCustomer = _dbContext.Customers.AsNoTracking().FirstOrDefault(c => c.CustomerId == id);

            if (findCustomer == null)
            {
                return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.CustomerNotFoundMessage, statusCode: 404);
            }

            var findCustomerByEmail = GetByEmail(email: customerDtoRequest.Email);

            if (findCustomerByEmail != null && findCustomerByEmail.CustomerId != id) return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.EmailExistsError, 409);

            var customer = Customer.SetExistingInfo
            (
                customerId: findCustomer.CustomerId,
                firstName: customerDtoRequest.FirstName,
                lastName: customerDtoRequest.LastName,
                email: customerDtoRequest.Email,
                dateOfBirth: DateOnly.FromDateTime(customerDtoRequest.DateOfBirth)
            );

            if (!customer.IsValid)
            {
                return ServiceResult<Customer>.ErrorResult(message: ResponseMessages.CustomerIsNotValid, statusCode: 422);
            }

            var updatedCustomer = _customerRepository.Update(id: id, entity: customer);

            return ServiceResult<Customer>.SuccessResult(data: updatedCustomer);
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