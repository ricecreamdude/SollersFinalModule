using System.Linq;
using Microsoft.AspNetCore.Mvc;

using SollarsFinalApp.Models;


namespace SollarsFinalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerContext _customerContext;
        public CustomerController(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerContext.Customer.ToList();
            return Ok(customers);
        }

        //// GET api/<CustomerController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<CustomerController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CustomerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CustomerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
