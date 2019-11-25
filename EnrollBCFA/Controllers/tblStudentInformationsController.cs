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

    public class tblStudentInformationsController : Controller
    {
        private dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";

        // GET: tblStudentInformations
        public ActionResult Index()
        {
            return View(db.tblStudentInformations.ToList());
        }

        // GET: tblStudentInformations/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudentInformation tblStudentInformation = db.tblStudentInformations.Find(id);
            if (tblStudentInformation == null)
            {
                return HttpNotFound();
            }
            return View(tblStudentInformation);
        }

        // GET: tblStudentInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblStudentInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,StType,StLastName,StFirstName,StMiddleName,StAge,StDateOfBirth,StPlaceOfBirth,StContact,StGender,StMaritalStatus,StCitizenship,StReligion,StAddress,StFatherName,StFatherOccupation,StMotherName,StMotherOccupation,StParentAddress,StElemSchool,StElemAddress,StHighSchool,StHighAddress")] tblStudentInformation tblStudentInformation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string studentid = tblStudentInformation.StudentID;
                    string firstname = tblStudentInformation.StFirstName;
                    string midname = tblStudentInformation.StMiddleName;
                    string lastname = tblStudentInformation.StLastName;
                    string fullname = firstname + " " + midname + " " + lastname;
                    CreateRequirements(studentid, fullname);

                    db.tblStudentInformations.Add(tblStudentInformation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                tblStudentInformation.CreateMessage = "Duplication of Student ID is not allowed.";
                return View("Create", tblStudentInformation);
                //return Redirect("~/SubjectLists/Create");
            }

            return View(tblStudentInformation);
        }

        // GET: tblStudentInformations/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudentInformation tblStudentInformation = db.tblStudentInformations.Find(id);
            if (tblStudentInformation == null)
            {
                return HttpNotFound();
            }
            return View(tblStudentInformation);
        }
        public JsonResult EditEnrolledStudent(string lastname, string firstname, string middlename,  string id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string fullname = lastname + ", " + firstname + ", " + middlename;
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("UPDATE tblEnrollment SET NameOfStudent = '"+fullname+"' WHERE StudentID = '"+id+"'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    
                }
                return Json(JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult EditRequirements(string lastname, string firstname, string middlename, string id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string fullname = firstname + " " + middlename + " " + lastname;
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("UPDATE tblRequirements SET Name = '" + fullname + "' WHERE StudentID = '" + id + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                }
                return Json(JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult CreateRequirements(string id, string fullname)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("INSERT INTO tblRequirements VALUES ( '"+id+"', '"+fullname+"', 'false', 'false', 'false', 'false' )", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                }
                return Json(JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        // POST: tblStudentInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,StType,StLastName,StFirstName,StMiddleName,StAge,StDateOfBirth,StPlaceOfBirth,StContact,StGender,StMaritalStatus,StCitizenship,StReligion,StAddress,StFatherName,StFatherOccupation,StMotherName,StMotherOccupation,StParentAddress,StElemSchool,StElemAddress,StHighSchool,StHighAddress")] tblStudentInformation tblStudentInformation)
        {
            string ln = tblStudentInformation.StLastName;
            string fn = tblStudentInformation.StFirstName;
            string mn = tblStudentInformation.StMiddleName;
            string id = tblStudentInformation.StudentID;

            string fullname = fn + " " + mn + " " + ln;

            if (ModelState.IsValid)
            {
                db.Entry(tblStudentInformation).State = EntityState.Modified;
                db.SaveChanges();
                EditEnrolledStudent(ln, fn, mn, id);
                EditRequirements(ln, fn, mn, id);

                return RedirectToAction("Index");
            }
            return View(tblStudentInformation);
        }

        // GET: tblStudentInformations/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudentInformation tblStudentInformation = db.tblStudentInformations.Find(id);
            if (tblStudentInformation == null)
            {
                return HttpNotFound();
            }
            return View(tblStudentInformation);
        }

        // POST: tblStudentInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblStudentInformation tblStudentInformation = db.tblStudentInformations.Find(id);
            db.tblStudentInformations.Remove(tblStudentInformation);
            
            try
            {
                tblEnrollment enrolledstudent = db.tblEnrollments.Where(x => x.StudentID == id).FirstOrDefault();
                db.tblEnrollments.Remove(enrolledstudent);
            }
            catch (ArgumentNullException)
            {
               
            }
            try
            {
                tblRequirement requirements = db.tblRequirements.Where(x => x.StudentID == id).FirstOrDefault();
                db.tblRequirements.Remove(requirements);
            }
            catch (ArgumentNullException)
            {

            }

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
