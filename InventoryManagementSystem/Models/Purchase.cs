using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
        [Display(Name = "Quantity")]
        public int PurchaseID { get; set; }
        [Display(Name = "Product Sold")]
        public int ProductID { get; set; }
        [Display(Name = "Customer Last Name")]
        public int CustomerID { get; set; }

        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}