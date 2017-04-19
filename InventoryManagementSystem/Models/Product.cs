using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }
        public string Title { get; set; }
        public int InventoryNumber { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }

    }
}