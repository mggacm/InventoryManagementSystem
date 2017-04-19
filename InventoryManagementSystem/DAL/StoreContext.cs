using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("StoreContext")
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }     
        public DbSet<Assignment> Assignments { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Product>()
            //    .HasMany(c => c.Employees).WithMany(i => i.Products)
            // .Map(t => t.MapLeftKey("ProductID")
            //     .MapRightKey("EmployeeID")
            //     .ToTable("ProductEmployee"));
        }
    }
}


    