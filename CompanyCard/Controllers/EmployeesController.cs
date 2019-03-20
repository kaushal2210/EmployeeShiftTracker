using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyCard.Models;

namespace CompanyCard.Controllers
{
    public class EmployeesController : Controller
    {
        private CompanyDataContainer db = new CompanyDataContainer();

        // GET: Employees
        public ActionResult Index(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (id == null)
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "You should select a company." });
                    }
                    Company company = db.Companies.Find(id);
                    if (company == null)
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Company not found." });
                    }
                    ViewBag.CompanyName = company.CompanyName;
                    var empTemp = from a in db.Employees.ToList()
                                  where a.CompanyCompanyId == company.CompanyId
                                  select a;
                    return PartialView(empTemp);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (id == null)
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "You should select an employee." });
                    }
                    Employee employee = db.Employees.Find(id);
                    if (employee == null)
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Employee not found." });
                    }
                    return PartialView("Details", employee);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        ViewBag.CompanyCompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
                        return PartialView();
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Only Admins can do this!!." });
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,EmployeeName,EmployeePhoneNo,EmployeeAddress,Email,,SalaryPerHour,CompanyCompanyId")] Employee employee)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Employees.Add(employee);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                        ViewBag.CompanyCompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", employee.CompanyCompanyId);
                        return PartialView("Create", employee);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Only Admins can do this!!." });
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (id == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "You should select an employee." });
                        }
                        Employee employee = db.Employees.Find(id);
                        if (employee == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Employee not found." });
                        }
                        ViewBag.CompanyCompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", employee.CompanyCompanyId);
                        return PartialView("Edit", employee);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Only Admins can do this!!." });
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeName,EmployeePhoneNo,EmployeeAddress,Email,SalaryPerHour,CompanyCompanyId")] Employee employee)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(employee).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        ViewBag.CompanyCompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", employee.CompanyCompanyId);
                        return PartialView("Edit", employee);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Only Admins can do this!!." });
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (id == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "You should select an employee." });
                        }
                        Employee employee = db.Employees.Find(id);
                        if (employee == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Employee not found." });
                        }
                        return PartialView("Delete", employee);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Only Admins can do this!!." });
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        Employee employee = db.Employees.Find(id);
                        db.Employees.Remove(employee);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Only Admins can do this!!." });
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
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
