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
                if (id == null)
                {
                    return View("Error", new ErrorViewModel { Description = "You must have valid employeeid to see the data." });
                }
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return View("Error", new ErrorViewModel { Description = "Company not found." });
                }
                ViewBag.EmpoyeeName = employee.EmployeeName;
                var shiftTemp = from a in db.Shifts.ToList()
                                where a.EmployeeId == employee.EmployeeId
                                select a;
                return PartialView(shiftTemp);
            }
            else
            {
                return View("ErrorLogin", new ErrorViewModel { Description = "Your must login first!!." });
            }

        }

        // GET: Shifts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // GET: Shifts/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShiftId,StartTime,EndTime,workedHours,EmployeeId")] Shift shift)
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

        // GET: Shifts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", shift.EmployeeId);
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShiftId,StartTime,EndTime,workedHours,EmployeeId")] Shift shift)
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

        // GET: Shifts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shift shift = db.Shifts.Find(id);
            db.Shifts.Remove(shift);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Start()
        {
            Session["startTime"] = DateTime.Now;
            return RedirectToAction("Companies", "Index");
        }

        public ActionResult End(int id)
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
