using BackShopCore.Data;
using BackShopCore.Dto;
using BackShopCore.Services;
using BackShopCore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackShopCore.Controllers
{
    [ApiController]
    [Route("api/Customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;
        private readonly ApplicationDbContext _dbContext;

        public CustomersController(ICustomerServices customerServices, ApplicationDbContext dbContext)
        {
            _customerServices = customerServices;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 0 || pageSize < 0) return BadRequest(ResponseMessages.CustomerPaginationError);

            var paginationFilter = new PaginationFilter(pageNumber: pageNumber, pageSize: pageSize);

            var customers = _customerServices.GetAll(paginationFilter: paginationFilter);

            if (customers.Count() == 0) return NoContent();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _customerServices.GetById(id: id);

            if (!result.Success)
            {
                return StatusCode(statusCode: result.StatusCode, value: result.Data);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CustomerDtoRequest customerDtoRequest)
        {
            var result = _customerServices.Add(customerDtoRequest: customerDtoRequest);

            if (!result.Success) return StatusCode(result.StatusCode, result.Message);

            _dbContext.SaveChanges();

            return CreatedAtAction(actionName: nameof(GetById), routeValues: new { id = result.Data.CustomerId }, value: result.Data);
        }

        [HttpPost("Bulk")]
        public async Task<IActionResult> AddBulk([FromBody] IEnumerable<CustomerDtoRequest> customersDtoRequest)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (customersDtoRequest.Count() == 0) return NoContent();

                var result = _customerServices.AddBulk(customersDtoRequest: customersDtoRequest);

                if (!result.Success)
                {
                    return StatusCode(statusCode: result.StatusCode, value: result.Message);
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return Created("", result.Data);
            }
            catch (Exception err)
            {
                await transaction.RollbackAsync();
                return StatusCode(statusCode: 500, value: new { message = err.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerDtoRequest customerDtoRequest)
        {
            var result = _customerServices.Update(id: id, customerDtoRequest: customerDtoRequest);

            if (!result.Success)
            {
                return StatusCode(statusCode: result.StatusCode, value: result.Message);
            }

            _dbContext.SaveChanges();

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _customerServices.Delete(id: id);

            if (!result.Success)
            {
                return StatusCode(statusCode: result.StatusCode, value: result.Message);
            }

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}