using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class StoreInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            var customers = new List<Customer>
            {
            new Customer{FirstMidName="Carson",LastName="Alexander",PurchaseDate=DateTime.Parse("2005-09-01")},
            new Customer{FirstMidName="Meredith",LastName="Alonso",PurchaseDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstMidName="Arturo",LastName="Anand",PurchaseDate=DateTime.Parse("2003-09-01")},
            new Customer{FirstMidName="Gytis",LastName="Barzdukas",PurchaseDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstMidName="Yan",LastName="Li",PurchaseDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstMidName="Peggy",LastName="Justice",PurchaseDate=DateTime.Parse("2001-09-01")},
            new Customer{FirstMidName="Laura",LastName="Norman",PurchaseDate=DateTime.Parse("2003-09-01")},
            new Customer{FirstMidName="Nino",LastName="Olivetto",PurchaseDate=DateTime.Parse("2005-09-01")}
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
            var products = new List<Product>
            {
            new Product{ProductID=1050,Title="Chemistry",InventoryNumber=3,},
            new Product{ProductID=4022,Title="Microeconomics",InventoryNumber=3,},
            new Product{ProductID=4041,Title="Macroeconomics",InventoryNumber=3,},
            new Product{ProductID=1045,Title="Calculus",InventoryNumber=4,},
            new Product{ProductID=3141,Title="Trigonometry",InventoryNumber=4,},
            new Product{ProductID=2021,Title="Composition",InventoryNumber=3,},
            new Product{ProductID=2042,Title="Literature",InventoryNumber=4,}
            };
            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
            var purchases = new List<Purchase>
            {
            new Purchase{CustomerID=1,ProductID=1050},
            new Purchase{CustomerID=1,ProductID=4022},
            new Purchase{CustomerID=1,ProductID=4041},
            new Purchase{CustomerID=1,ProductID=1045},
            new Purchase{CustomerID=1,ProductID=3141},
            new Purchase{CustomerID=1,ProductID=2021}
           
            };
            purchases.ForEach(s => context.Purchases.Add(s));
            context.SaveChanges();
        }

    }
}