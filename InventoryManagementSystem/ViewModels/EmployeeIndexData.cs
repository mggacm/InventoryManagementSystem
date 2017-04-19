using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.ViewModels
{
    public class EmployeeIndexData
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }
    }
}