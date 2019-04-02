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
    public class CompaniesController : Controller
    {
        private CompanyDataContainer db = new CompanyDataContainer();

        // GET: Companies
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView(db.Companies.ToList());
                }
                return View(db.Companies.ToList());
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }

        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
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

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,CompanyName,CompanyAddress,ContactNo,Website")] Company company)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Companies.Add(company);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                        return PartialView("Create", company);
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

        // GET: Companies/Edit/5
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
                            return PartialView("Error", new ErrorViewModel { Description = "You should select a company." });
                        }
                        Company company = db.Companies.Find(id);
                        if (company == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Company not found." });
                        }
                        return PartialView("Edit", company);
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

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,CompanyName,CompanyAddress,ContactNo,Website")] Company company)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(company).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return PartialView("Edit", company);
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

        // GET: Companies/Delete/5
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
                            return PartialView("Error", new ErrorViewModel { Description = "You should select a company." });
                        }
                        Company company = db.Companies.Find(id);
                        if (company == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Company not found." });
                        }
                        return PartialView("Delete", company);
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

        // POST: Companies/Delete/5
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
                        var companyEmployeeShift = from x in db.Shifts
                                                   where x.Employee.CompanyCompanyId == id
                                                   select x;
                        if (!companyEmployeeShift.Any())
                        {
                            //Delete previous employee data of the company
                            var deletePreviousEmployeeList = from x in db.PreviousEmployees
                                                             where x.CompanyCompanyId == id
                                                             select x;

                            foreach (PreviousEmployee temp in deletePreviousEmployeeList)
                            {
                                db.PreviousEmployees.Remove(temp);
                            }

                            //Delete PaidShifts data of the company's employees
                            var deletePaidShiftsOfCompanyEmployees = from x in db.PaidShifts
                                                                     where x.Employee.CompanyCompanyId == id
                                                                     select x;

                            foreach (PaidShift temp in deletePaidShiftsOfCompanyEmployees)
                            {
                                db.PaidShifts.Remove(temp);
                            }


                            //Delete current Employee data of the company
                            var deleteEmployeeList = from x in db.Employees
                                                     where x.CompanyCompanyId == id
                                                     select x;

                            foreach (Employee temp in deleteEmployeeList)
                            {
                                db.Employees.Remove(temp);
                            }


                            Company company = db.Companies.Find(id);
                            db.Companies.Remove(company);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = "You must pay all your employee's unpaid hours before close the comapany.";
                            return RedirectToAction("Index");
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
