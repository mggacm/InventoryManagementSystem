using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Assignment
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Employee Employee { get; set; }
        
    }
}