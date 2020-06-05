using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customers = _customerContext.Customer.ToList();

            foreach (Customers cust in customers)
            {
                if (cust.Id == id) return Ok(cust);
            }

            return NotFound($"No customer with id {id} found.");

        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customers customer)
        {

            _customerContext.Customer.Add(customer);

            return Ok(_customerContext.SaveChanges());
        }

        //// PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Customers customer)
        {

            var customers = _customerContext.Customer.ToList();

            //Find matching record (via 
            var match = ( from c in customers
                       where id == c.Id
                       select c).SingleOrDefault();

            //Update all fields
            match.FirstName = customer.FirstName;
            match.LastName = customer.LastName;
            match.Email = customer.Email;

            //Update database value
            _customerContext.Update(match);

            //Save
            return Ok(_customerContext.SaveChanges());
 

        }

        //// DELETE api/<CustomerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
