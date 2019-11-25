using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnrollBCFA.Models;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace EnrollBCFA.Controllers
{
    public class SectionsController : Controller
    {
        private dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";

        List<SelectListItem> StudentType = new List<SelectListItem>();
        List<SelectListItem> Teacher = new List<SelectListItem>();


        // GET: Sections
        public ActionResult Index()
        {
            return View(db.tblSections.OrderBy(x => x.GradeLevel.Length).ThenBy(x => x.GradeLevel).ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSection tblSection = db.tblSections.Find(id);
            if (tblSection == null)
            {
                return HttpNotFound();
            }
            return View(tblSection);
        }

        // GET: Sections/Create
        public ActionResult Create()
        {
            ViewBag.SectionAdviser = GetAdvisers();

            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SType,SectionID,GradeLevel,SectionName,SectionAdviser")] tblSection tblSection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.tblSections.Add(tblSection);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tblSection);
            }
            catch (DbUpdateException)
            {
                tblSection.CreateMessage = "The section you are trying to create already exists.";
                return View("Create", tblSection);
            }
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SectionAdviser = GetAdvisers();

            tblSection tblSection = db.tblSections.Find(id);
            if (tblSection == null)
            {
                return HttpNotFound();
            }
            return View(tblSection);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SType,SectionID,GradeLevel,SectionName,SectionAdviser")] tblSection tblSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSection);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSection tblSection = db.tblSections.Find(id);
            if (tblSection == null)
            {
                return HttpNotFound();
            }
            return View(tblSection);
        }
        public SelectList GetAdvisers()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT TeacherName FROM tblTeacher", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Teacher.Add(new SelectListItem { Text = myReader["TeacherName"].ToString(), Value = myReader["TeacherName"].ToString() });
                }
                con.Close();
                return new SelectList(Teacher, "Value", "Text"); // return the list object
            }
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblSection tblSection = db.tblSections.Find(id);
            db.tblSections.Remove(tblSection);
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
