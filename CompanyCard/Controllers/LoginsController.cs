using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CompanyCard.Models;

namespace CompanyCard.Controllers
{
    public class LoginsController : Controller
    {
        private CompanyDataContainer db = new CompanyDataContainer();

        public ActionResult LoginPage()
        {
            if (Session["username"] != null) {
                return RedirectToAction("Index", "Companies");
            }
            return View();
        }
        // GET: Logins
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {
                    if (Session["Admin"].Equals("Yes"))
                    {

                        return PartialView(db.Logins);
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
            else
            {
                return View("Error404");
            }
        }

        public ActionResult Logout()
        {
            if (Session["username"] != null)
            {
                Session.Abandon();
                return RedirectToAction("LoginPage", "Logins");

            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }
        }

        [HttpPost]
        public ActionResult Authorize(Logins log)
        {
            string passwordMd5Hash = CalculateMD5Hash(log.Password);
            log.Password = passwordMd5Hash;
            var user = db.Logins.Where(l => l.UserName == log.UserName && l.Password == log.Password).FirstOrDefault();

            if (user == null)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Your username or password is wrong, Please Try again!!.')</script>");
                return View("loginPage");
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
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                        return PartialView("Details");
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
            else
            {
                return View("Error404");
            }
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                        return PartialView();
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
            else
            {
                return View("Error404");
            }
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginsId,UserName,Password,Admin,EmployeeId")] Logins logins)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            string passwordMd5Hash = CalculateMD5Hash(logins.Password);
                            logins.Password = passwordMd5Hash;
                            db.Logins.Add(logins);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", logins.EmployeeId);
                        return PartialView("Create", logins);
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
            else
            {
                return View("Error404");
            }
        }

        // GET: Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", logins.EmployeeId);
                        return PartialView("Edit", logins);
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
            else
            {
                return View("Error404");
            }
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginsId,UserName,Password,Admin,EmployeeId")] Logins logins)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
                {

                    if (Session["Admin"].Equals("Yes") || logins.UserName.Equals(Session["username"]))
                    {
                        if (ModelState.IsValid)
                        {
                            string passwordMd5Hash = CalculateMD5Hash(logins.Password);
                            logins.Password = passwordMd5Hash;
                            db.Entry(logins).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", logins.EmployeeId);
                        return PartialView("Edit", logins);
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
            else
            {
                return View("Error404");
            }
        }

        // GET: Logins/Delete/5
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // POST: Logins/Delete/5
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
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return View("Error404");
            }
        }
        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        [HttpPost]
        public ActionResult ResetPassword(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["username"] != null)
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
                        string passwordMd5Hash = CalculateMD5Hash("ABCxyz123");
                        logins.Password = passwordMd5Hash;
                        db.Entry(logins).State = EntityState.Modified;
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
