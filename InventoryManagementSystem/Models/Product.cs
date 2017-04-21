using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "UPC")]
        public int ProductID { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }

        public int InventoryNumber { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual Department Departments { get; set; }
        public virtual Employee Employees { get; set; }

    }
}