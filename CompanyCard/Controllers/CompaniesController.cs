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
            if (Session["username"] != null)
            {
                return PartialView();
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,CompanyName,CompanyAddress,ContactNo,Website")] Company company)
        {
            if (Session["username"] != null)
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
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] != null)
            {
                if (id == null)
                {
                    return View("Error", new ErrorViewModel { Description = "You should select a company." });
                }
                Company company = db.Companies.Find(id);
                if (company == null)
                {
                    return View("Error", new ErrorViewModel { Description = "Company not found." });
                }
                //if (Request.IsAjaxRequest()) {
                //    return PartialView("Edit",company);
                //}
                return PartialView("Edit", company);
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,CompanyName,CompanyAddress,ContactNo,Website")] Company company)
        {
            if (Session["username"] != null)
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
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["username"] != null)
            {
                if (id == null)
                {
                    return View("Error", new ErrorViewModel { Description = "You should select a company." });
                }
                Company company = db.Companies.Find(id);
                if (company == null)
                {
                    return View("Error", new ErrorViewModel { Description = "Company not found." });
                }
                return PartialView("Delete", company);
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["username"] != null)
            {
                Company company = db.Companies.Find(id);
                db.Companies.Remove(company);
                db.SaveChanges();
                return RedirectToAction("Index");
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
