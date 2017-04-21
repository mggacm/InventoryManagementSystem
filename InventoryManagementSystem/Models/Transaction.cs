using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime TimeOfDay { get; set; }
        public string AnyIssues { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Customer customer { get; set; }

    }
}