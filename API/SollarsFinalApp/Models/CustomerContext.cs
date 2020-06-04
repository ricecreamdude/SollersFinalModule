using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SollarsFinalApp.Models;

namespace SollarsFinalApp.Models
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customers> Customers { get; set; }

        //Constructor function for CustomerContext - loads database context.
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
            LoadDefaultCustomers();
        }

        public List<Customers> getCustomers() => Customers.Local.ToList<Customers>();
        private void LoadDefaultCustomers()
        {
            Customers.Add(new Customers {Id=100, FirstName = "Joshua", LastName = "Ho", Email="ho.joshua4@gmail.com" });
            Customers.Add(new Customers { Id = 200, FirstName = "Anna", LastName = "Liu", Email = "anna.liu@hotmail.com" });
        }
    }
}
