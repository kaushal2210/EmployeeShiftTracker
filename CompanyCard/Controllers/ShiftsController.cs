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
    public class ShiftsController : Controller
    {
        private CompanyDataContainer db = new CompanyDataContainer();

        // GET: Shifts
        public ActionResult Index(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (id == null)
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "You must have valid employeeid to see the data." });
                    }
                    Employee employee = db.Employees.Find(id);
                    if (employee == null)
                    {
                        return PartialView("Error", new ErrorViewModel { Description = "Company not found." });
                    }
                    ViewBag.EmpoyeeName = employee.EmployeeName;
                    var shiftTemp = from a in db.Shifts.ToList()
                                    where a.EmployeeId == employee.EmployeeId
                                    select a;
                    return PartialView(shiftTemp);
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

        // GET: Shifts/Details/5
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
                            return PartialView("Error", new ErrorViewModel { Description = "You should select a shift." });
                        }
                        Shift shift = db.Shifts.Find(id);
                        if (shift == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Shift not found." });
                        }
                        return View(shift);
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

        // GET: Shifts/Create
        public ActionResult Create()
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                        return View();
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

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShiftId,StartTime,EndTime,workedHours,EmployeeId")] Shift shift)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Shifts.Add(shift);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", shift.EmployeeId);
                        return View(shift);
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

        // GET: Shifts/Edit/5
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
                            return PartialView("Error", new ErrorViewModel { Description = "You should select a shift." });
                        }
                        Shift shift = db.Shifts.Find(id);
                        if (shift == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Shift not found." });
                        }
                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", shift.EmployeeId);
                        return View(shift);
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

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShiftId,StartTime,EndTime,workedHours,EmployeeId")] Shift shift)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(shift).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", shift.EmployeeId);
                        return View(shift);
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

        // GET: Shifts/Delete/5
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
                            return PartialView("Error", new ErrorViewModel { Description = "You should select a shift." });
                        }
                        Shift shift = db.Shifts.Find(id);
                        if (shift == null)
                        {
                            return PartialView("Error", new ErrorViewModel { Description = "Shift not found." });
                        }
                        return PartialView(shift);
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

        // POST: Shifts/Delete/5
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
                        Shift shift = db.Shifts.Find(id);
                        db.Shifts.Remove(shift);
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

        public ActionResult Start()
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    Session["startTime"] = DateTime.Now;
                    return RedirectToAction("Companies", "Index");
                }
                else
                {
                    return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult End(int id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    var endTime = DateTime.Now;
                    double difference = Math.Round(endTime.Subtract(DateTime.Parse(Session["startTime"].ToString())).TotalHours, 2);
                    //var tempStart = startTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //var tempEnd = endTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var emp = db.Employees.Find(id);
                    Shift shift = new Shift { StartTime = DateTime.Parse(Session["startTime"].ToString()), EndTime = endTime, workedHours = difference, EmployeeId = id };
                    if (ModelState.IsValid)
                    {
                        db.Shifts.Add(shift);
                        db.SaveChanges();
                        return RedirectToAction("Companies", "Index");
                    }
                    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", shift.EmployeeId);
                    return RedirectToAction("Companies", "Index");
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

        public ActionResult Pay(int? id)
        {
            if (Session["username"] != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["Admin"].Equals("Yes"))
                    {
                        int? tempId = id;
                        var deleteList = from x in db.Shifts
                                         where x.EmployeeId == id
                                         select x;

                        foreach (Shift temp in deleteList)
                        {
                            db.Shifts.Remove(temp);
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index", "Shifts", new { id = tempId });
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
