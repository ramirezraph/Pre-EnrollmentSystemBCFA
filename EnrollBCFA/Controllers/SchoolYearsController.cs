using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnrollBCFA.Models;

namespace EnrollBCFA.Controllers
{
    public class SchoolYearsController : Controller
    {
        private dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();

        // GET: SchoolYears
        public ActionResult Index()
        {
            return View(db.tblSchoolYears.ToList());
        }
        // GET: SchoolYears/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSchoolYear tblSchoolYear = db.tblSchoolYears.Find(id);
            if (tblSchoolYear == null)
            {
                return HttpNotFound();
            }
            return View(tblSchoolYear);
        }

        // GET: SchoolYears/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolYear,status")] tblSchoolYear tblSchoolYear)
        {
            if (ModelState.IsValid)
            {
                db.tblSchoolYears.Add(tblSchoolYear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSchoolYear);
        }

        // GET: SchoolYears/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSchoolYear tblSchoolYear = db.tblSchoolYears.Find(id);
            if (tblSchoolYear == null)
            {
                return HttpNotFound();
            }
            return View(tblSchoolYear);
        }

        // POST: SchoolYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolYear,status")] tblSchoolYear tblSchoolYear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSchoolYear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSchoolYear);
        }

        // GET: SchoolYears/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSchoolYear tblSchoolYear = db.tblSchoolYears.Find(id);
            if (tblSchoolYear == null)
            {
                return HttpNotFound();
            }
            return View(tblSchoolYear);
        }

        // POST: SchoolYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblSchoolYear tblSchoolYear = db.tblSchoolYears.Find(id);
            db.tblSchoolYears.Remove(tblSchoolYear);
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
