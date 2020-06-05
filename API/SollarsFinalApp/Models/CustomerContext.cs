using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SollarsFinalApp.Models;

namespace SollarsFinalApp.Models
{
    public partial class CustomerContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CustomerContext() { }
        //Constructor function for CustomerContext - loads database context.
        public CustomerContext(DbContextOptions<CustomerContext> options, IConfiguration configuration) : base(options)
        {

            _configuration = configuration;

        }


        //What does this do?
        //This is literally the endpoint which the controller is access by
        public DbSet<Customers> Customer { get; set; }

        //Validate our model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Email).HasMaxLength(20);

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(50);

            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
