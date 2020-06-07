using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /*
        *  TEST ZONE
        */
        public delegate IActionResult RequestCallback();

        //Using this structure allows us to templatize the try catch block
        public IActionResult RequestHandler(RequestCallback callback)
        {
            try { return callback(); }
            catch { return StatusCode(500); }
        }

        [HttpGet]
        public IActionResult GetHandler()
        {
            // Pass a function into the Request callback delegate.  Lambda function
            // inherits parameter type of RequestHandler parameter definition
            return RequestHandler( () => {
                var customers = _customerContext.Customer.ToList();
                return Ok(customers);
            });
        }


        /*
         *  TEST ZONE
        */


        // GET: api/<CustomerController>
        //[HttpGet]
        //public IActionResult GetCustomers()
        //{
        //    var customers = _customerContext.Customer.ToList();
        //    return Ok(customers);
        //}

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            //Using the callback loop we are able to access the values in the get request method directly 
            //and write to the response.
            return RequestHandler( () => {
                //Explictly typed
                List<Customers> customers = _customerContext.Customer.ToList();

                foreach (Customers cust in customers)
                    if (cust.Id == id) return Ok(cust);

                return NotFound("Employee not found");
            });

        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customers customer)
        {
            return RequestHandler(() =>
           {
               _customerContext.Customer.Add(customer);
               _customerContext.SaveChanges();

               //Save DB
               return Ok("Created new customer.");
           });
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Customers customer)
        {
           return RequestHandler(() =>
           {
               var customers = _customerContext.Customer.ToList();

               //Find matching record (via LINQ)
               var match = (from c in customers
                            where id == c.Id
                            select c).SingleOrDefault();

               //Update all fields
               match.FirstName = customer.FirstName;
               match.LastName = customer.LastName;
               match.Email = customer.Email;

               //Save DB
               _customerContext.SaveChanges();

               return Ok("Customer Updated");
           });

        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           return RequestHandler(() =>
           {
               var customers = _customerContext.Customer.ToList();

               var match = (from c in customers
                            where id == c.Id
                            select c).SingleOrDefault();

               _customerContext.Remove(match);

               _customerContext.SaveChanges();

               return Ok("Customer deleted");
           });

        }
    }
}
