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
using System.Data.Entity.Infrastructure;

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
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,HireDate,Assignment")] Employee employee)
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
            Employee employee = db.Employees
                .Include(i => i.Assignment)
                .Where(i => i.ID == id)
                .Single();
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Assignments, "EmployeeID", "Location", employee.ID);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = db.Employees
               .Include(i => i.Assignment)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(employeeToUpdate, "",
               new string[] { "LastName", "FirstMidName", "HireDate", "Assignment" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(employeeToUpdate.Assignment.Location))
                    {
                        employeeToUpdate.Assignment = null;
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(employeeToUpdate);
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
            Employee employee = db.Employees
                .Include(i => i.Assignment)
                .Where(d => d.ID == id)
                .Single();

            db.Employees.Remove(employee);

            var department = db.Departments
                .Where(d => d.EmployeeID == id)
                .SingleOrDefault();
            if (department != null)
            {
                department.EmployeeID = null;
            }

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
