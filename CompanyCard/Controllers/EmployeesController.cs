using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
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
            Session["CompanyId"] = id;
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                    ViewBag.CompanyId = company.CompanyId;
                    var empTemp = from a in db.Employees.ToList()
                                  where a.CompanyCompanyId == company.CompanyId
                                  select a;
                    return PartialView(empTemp);

                }
                else
                {
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,EmployeeName,EmployeePhoneNo,EmployeeAddress,Email,,SalaryPerHour,CompanyCompanyId")] Employee employee)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Employees.Add(employee);
                            db.SaveChanges();
                            return RedirectToAction("Index" , new { id = employee.CompanyCompanyId });
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeName,EmployeePhoneNo,EmployeeAddress,Email,SalaryPerHour,CompanyCompanyId")] Employee employee)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(employee).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index",new { id = employee.CompanyCompanyId });
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
                        var shiftTemp = from a in db.Shifts.ToList()
                                        where a.EmployeeId == id
                                        select a;
                        if (!shiftTemp.Any())
                        {
                            var deleteList = from x in db.PaidShifts
                                             where x.EmployeeId == id
                                             select x;
                            foreach (PaidShift temp in deleteList)
                            { 
                                db.PaidShifts.Remove(temp);
                            }
                            var login = from x in db.Logins
                                        where x.EmployeeId == id
                                        select x;
                            foreach (Logins temp in login)
                            {
                                db.Logins.Remove(temp);
                            }
                            Employee employee = db.Employees.Find(id);
                            db.PreviousEmployees.Add(new PreviousEmployee{ EmployeeName = employee.EmployeeName, EmployeePhoneNo = employee.EmployeePhoneNo, EmployeeAddress = employee.EmployeeAddress, Email = employee.Email, CompanyCompanyId = employee.CompanyCompanyId });
                            db.Employees.Remove(employee);
                            db.SaveChanges();
                            return RedirectToAction("Index", new { id = employee.CompanyCompanyId });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Shifts", new { id = id });
                        }
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Only Admins can do this!!." });
                    }

                }
                else
                {
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });                 
                }
            }
            else
            {
                return View("Error404");
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
