using BackShopCore.Services;
using BackShopCore.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackShopCore.Controllers
{
    [ApiController]
    [Route("api/Customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomersController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
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
    }
}