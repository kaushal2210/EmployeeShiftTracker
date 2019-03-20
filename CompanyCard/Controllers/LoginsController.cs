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
    public class LoginsController : Controller
    {
        private CompanyDataContainer db = new CompanyDataContainer();

        public ActionResult loginPage()
        {
            return View();
        }
        // GET: Logins
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                if (Session["Admin"].Equals("Yes"))
                {
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView(db.Logins);
                    }
                    return View(db.Logins);
                }
                else
                {
                    return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
                }
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        public ActionResult logout()
        {
            if (Session["username"] != null)
            {
                Session.Abandon();
                return RedirectToAction("loginPage", "Logins");
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        [HttpPost]
        public ActionResult Authorize(Logins log)
        {

            var user = db.Logins.Where(l => l.UserName == log.UserName && l.Password == log.Password).FirstOrDefault();

            if (user == null)
            {
                return PartialView("ErrorLogin", new ErrorViewModel { Description = "Your user name or password is wrong, try again!!." });
            }
            else
            {
                Session["loginid"] = user.LoginsId;
                Session["username"] = user.UserName;
                Session["Admin"] = user.Admin;
                Session["EmployeeId"] = user.EmployeeId;
                return RedirectToAction("Index", "Companies");
            }

        }
        // GET: Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {

                        if (id == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "you should provide loginid." });
                        }
                        Logins logins = db.Logins.Find(id);
                        if (logins == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Loginid not found." });
                        }
                        return PartialView("Details", db.Logins);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
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

        // GET: Logins/Create
        public ActionResult Create()
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        return PartialView();
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
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

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginsId,UserName,Password,Admin")] Logins logins)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Logins.Add(logins);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return PartialView("Create", logins);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
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

        // GET: Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (id == null)
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "you should provide loginid." });
                    }
                    Logins logins = db.Logins.Find(id);
                    if (Session["Admin"].Equals("Yes") || logins.UserName.Equals(Session["username"]))
                    {

                        if (logins == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Loginid not found." });
                        }
                        return PartialView("Edit", logins);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
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

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginsId,UserName,Password,Admin")] Logins logins)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes") || logins.UserName.Equals(Session["username"]))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(logins).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return PartialView("Edit", logins);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
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

        // GET: Logins/Delete/5
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
                            return PartialView("Error", new ErrorViewModel { Description = "You should provide loginid." });
                        }
                        Logins logins = db.Logins.Find(id);
                        if (logins == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Loginid not found." });
                        }
                        return PartialView("Delete", logins);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
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

        // POST: Logins/Delete/5
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

                        Logins logins = db.Logins.Find(id);
                        db.Logins.Remove(logins);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "This function is only for Admins." });
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
