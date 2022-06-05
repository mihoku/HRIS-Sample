﻿using System;
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
    public class departmentsController : Controller
    {
        private HRISEntities db = new HRISEntities();

        // GET: departments
        public ActionResult Index()
        {
            var departments = db.departments.Include(d => d.location);
            return View(departments.ToList());
        }

        // GET: departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: departments/Create
        public ActionResult Create()
        {
            ViewBag.location_id = new SelectList(db.locations, "location_id", "street_address");
            return View();
        }

        // POST: departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "department_id,department_name,location_id")] department department)
        {
            if (ModelState.IsValid)
            {
                db.departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.location_id = new SelectList(db.locations, "location_id", "street_address", department.location_id);
            return View(department);
        }

        // GET: departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_id = new SelectList(db.locations, "location_id", "street_address", department.location_id);
            return View(department);
        }

        // POST: departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "department_id,department_name,location_id")] department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_id = new SelectList(db.locations, "location_id", "street_address", department.location_id);
            return View(department);
        }

        // GET: departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            department department = db.departments.Find(id);
            db.departments.Remove(department);
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
