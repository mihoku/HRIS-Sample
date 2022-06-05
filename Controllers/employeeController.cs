using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRIS_Sample.Models;

namespace HRIS_Sample.Controllers
{
    public class employeeController : Controller
    {
        private HRISEntities db = new HRISEntities();

        // GET: employee
        public ActionResult Index(int id, string src)
        {
            ViewBag.src = src;
            if (src == "office")
            {
                var employees = db.employees.Where(y => y.department.location_id == id).Include(e => e.department).Include(e => e.job).Include(e => e.employee1);
                ViewBag.description = "Office Branch " + db.locations.Find(id).city + " " + db.locations.Find(id).state_province;
                return View(employees.ToList());
            }
            else if (src == "dept")
            {
                var employees = db.employees.Where(y => y.department_id == id).Include(e => e.department).Include(e => e.job).Include(e => e.employee1);
                ViewBag.description = "Department " + db.departments.Find(id).department_name;
                return View(employees.ToList());
            }
            else
            {
                var employees = db.employees.Where(y => y.job_id == id).Include(e => e.department).Include(e => e.job).Include(e => e.employee1);
                ViewBag.description = "Job Title " + db.jobs.Find(id).job_title;
                return View(employees.ToList());
            }
        }

        // GET: employee/Details/5
        public ActionResult Details(int? id, string src)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.src = src;
            return View(employee);
        }

        // GET: employee/Create
        public ActionResult Create()
        {
            ViewBag.department_id = new SelectList(db.departments, "department_id", "department_name");
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_title");
            ViewBag.manager_id = new SelectList(db.employees, "employee_id", "first_name");
            return View();
        }

        // POST: employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employee_id,first_name,last_name,email,phone_number,hire_date,job_id,salary,manager_id,department_id")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.department_id = new SelectList(db.departments, "department_id", "department_name", employee.department_id);
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_title", employee.job_id);
            ViewBag.manager_id = new SelectList(db.employees, "employee_id", "first_name", employee.manager_id);
            return View(employee);
        }

        // GET: employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.department_id = new SelectList(db.departments, "department_id", "department_name", employee.department_id);
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_title", employee.job_id);
            ViewBag.manager_id = new SelectList(db.employees, "employee_id", "first_name", employee.manager_id);
            return View(employee);
        }

        // POST: employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employee_id,first_name,last_name,email,phone_number,hire_date,job_id,salary,manager_id,department_id")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.department_id = new SelectList(db.departments, "department_id", "department_name", employee.department_id);
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_title", employee.job_id);
            ViewBag.manager_id = new SelectList(db.employees, "employee_id", "first_name", employee.manager_id);
            return View(employee);
        }

        // GET: employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employee employee = db.employees.Find(id);
            db.employees.Remove(employee);
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
