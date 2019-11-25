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
    public class SubjectListsController : Controller
    {
        private dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();

        List<SelectListItem> TeacherList = new List<SelectListItem>();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";


        // GET: SubjectLists
        public ActionResult Index()
        {
            return View(db.SubjectLists.OrderBy(x => x.SubjectType).ThenBy(x => x.SubjectName).ToList());
        }
        
        // GET: SubjectLists/Create
        public ActionResult Create()
        {
            ViewBag.SubjectTeacher = GetAllTeacher();

            return View();
        }

        // POST: SubjectLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectCode,SubjectName,SubjectType,SubjectTeacher")] SubjectList subjectList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.SubjectLists.Add(subjectList);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                subjectList.CreateMessage = "Duplicate Subject code is not allowed.";
                return View("Create", subjectList);
                //return Redirect("~/SubjectLists/Create");
            }

            return View(subjectList);
        }

        public SelectList GetAllTeacher()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT TeacherName FROM tblTeacher", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    TeacherList.Add(new SelectListItem { Text = myReader["TeacherName"].ToString(), Value = myReader["TeacherName"].ToString() });
                }
                con.Close();
                return new SelectList(TeacherList, "Value", "Text"); // return the list object
            }
        }

        // GET: SubjectLists/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.SubjectTeacher = GetAllTeacher();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectList subjectList = db.SubjectLists.Find(id);
            if (subjectList == null)
            {
                return HttpNotFound();
            }
            return View(subjectList);
        }

        // POST: SubjectLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectCode,SubjectName,SubjectType,SubjectTeacher")] SubjectList subjectList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subjectList);
        }

        // GET: SubjectLists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectList subjectList = db.SubjectLists.Find(id);
            if (subjectList == null)
            {
                return HttpNotFound();
            }
            return View(subjectList);
        }

        // POST: SubjectLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SubjectList subjectList = db.SubjectLists.Find(id);
            db.SubjectLists.Remove(subjectList);
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
