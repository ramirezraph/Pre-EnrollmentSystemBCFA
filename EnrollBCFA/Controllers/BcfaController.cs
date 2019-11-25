using EnrollBCFA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrollBCFA.Controllers
{
    public class BcfaController : Controller
    {
        dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";
        string countstudent;
        string countteacher;
        string countsection;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enrollment()
        {
            return RedirectToAction("Index", "Enrollment");
        }

        public ActionResult Student()
        {
            return RedirectToAction("Index", "tblStudentInformations");
        }

        public ActionResult ClassList()
        {
            return RedirectToAction("Index", "ClassList");
        }

        public ActionResult EnrolledStudent()
        {
            return RedirectToAction("Index", "ES");
        }

        public ActionResult Scheduling()
        {
            ViewBag.VBTypeList = "";
            return RedirectToAction("Index", "Schedule");
        }

        public ActionResult Teacher()
        {
            return RedirectToAction("Index", "Teachers");
        }

        public ActionResult Subjects()
        {
            return RedirectToAction("Index", "SubjectLists");
        }
        public ActionResult SchoolYear()
        {
            return RedirectToAction("Index", "SchoolYears");
        }
        public ActionResult Section()
        {
            return RedirectToAction("Index", "Sections");
        }
        public ActionResult ManageUser()
        {
            return RedirectToAction("Index", "ManageAccount");
        }
        public ActionResult Requirements()
        {
            return RedirectToAction("Index", "Requirements");
        }

        public JsonResult CountStudent()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT COUNT(id) AS Count FROM tblEnrollment", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    countstudent = myReader["Count"].ToString();
                }
                return Json(countstudent, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
            
        }
        public JsonResult CountTeacher()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT COUNT(TeacherID) AS Count FROM tblTeacher", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    countteacher = myReader["Count"].ToString();
                }
                return Json(countteacher, JsonRequestBehavior.AllowGet); // return the list object in json form
            }

        }
        public JsonResult CountSection()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT COUNT(SectionID) AS Count FROM tblSection", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    countsection = myReader["Count"].ToString();
                }
                return Json(countsection, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
       
        public JsonResult UpdateAccount(string lname, string fname, string username, int accountid)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("UPDATE tblAccounts SET LastName = '"+lname+"', FirstName = '"+fname+"', Username = '"+username+"' WHERE AccountID = '"+ accountid + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    
                }
                return Json(JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult UpdateAccountPassword(int accountid, string newpassword)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("UPDATE tblAccounts SET Password = '" + newpassword + "' WHERE AccountID = '" + accountid + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                }
                return Json(JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
    }
}