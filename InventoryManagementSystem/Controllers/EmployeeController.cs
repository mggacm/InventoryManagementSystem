using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;


namespace InventoryManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Employee
        public ActionResult Index(int? id, int? productID)
        {
            var viewModel = new EmployeeIndexData();
            viewModel.Employees = db.Employees
             .Include(i => i.Assignment)
            .Include(i => i.Product.Select(c => c.Departments))
            .OrderBy(i => i.LastName);
            
            if (id != null)
            {
                ViewBag.EmployeeID = id.Value;
                viewModel.Products = viewModel.Employees.Where(
                    i => i.ID == id.Value).Single().Product;
            }

            if (productID != null)
            {
                ViewBag.ProductID = productID.Value;
                //viewModel.Purchases = viewModel.Products.Where(
                //    x => x.ProductID == productID).Single().Purchases;
                var selectedProduct = viewModel.Products.Where(x => x.ProductID == productID).Single();
                db.Entry(selectedProduct).Collection(x => x.Purchases).Load();
                foreach (Purchase purchase in selectedProduct.Purchases)
                {
                    db.Entry(purchase).Reference(x => x.Customer).Load();
                }

                viewModel.Purchases = selectedProduct.Purchases;
            }

            return View(viewModel);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Assignments, "EmployeeID", "Location");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Assignments, "EmployeeID", "Location", employee.ID);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Assignments, "EmployeeID", "Location", employee.ID);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Assignments, "EmployeeID", "Location", employee.ID);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
