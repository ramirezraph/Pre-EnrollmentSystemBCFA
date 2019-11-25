using EnrollBCFA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrollBCFA.Controllers
{
    public class EnrollmentController : Controller
    {

        dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();

        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";

        List<SelectListItem> RegisteredStudentList = new List<SelectListItem>();
        List<SelectListItem> NEGradeLevelList = new List<SelectListItem>();
        List<SelectListItem> NESectionList = new List<SelectListItem>();

        // GET: Enrollment
        public ActionResult Index()
        {
            ViewBag.comboboxSearch = GetNamesRegistered();
            ViewBag.gradelevelenroll = NEGradeLevelList;
            ViewBag.sectionenroll = NESectionList;
            return View();
        }
        public ActionResult PrintEnrollment()
        {
            DateTime today = DateTime.Today;
            string datetoday = today.ToString("d");
            ViewBag.DateNow = datetoday;
            return View();
        }

        public SelectList GetNamesRegistered()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT StLastName, StFirstName, StMiddleName FROM tblStudentInformation", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    RegisteredStudentList.Add(new SelectListItem { Text = myReader["StLastName"].ToString() +", "+ myReader["StFirstName"].ToString() + ", " + myReader["StMiddleName"].ToString(), Value = myReader["StLastName"].ToString() + ", " + myReader["StFirstName"].ToString() + ", " + myReader["StMiddleName"].ToString() });
                }
                con.Close();
                return new SelectList(RegisteredStudentList, "Value", "Text"); // return the list object
            }
        }
        public JsonResult GetStudentInfo(string lastname, string firstname, string middlename)
        {
            tblStudentInformation studentinfo = db.tblStudentInformations.FirstOrDefault(x => x.StLastName == lastname || x.StFirstName == firstname || x.StMiddleName == middlename);
            return Json(studentinfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult isEnrolled(string nameofstudent)
        {
            tblEnrollment thestudent = db.tblEnrollments.FirstOrDefault(x => x.NameOfStudent == nameofstudent);
            return Json(thestudent, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GradeLevel(string type)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT DISTINCT GradeLevel FROM tblSection WHERE SType = '" + type + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    NEGradeLevelList.Add(new SelectListItem { Text = myReader["GradeLevel"].ToString(), Value = myReader["GradeLevel"].ToString() });
                }
                return Json(NEGradeLevelList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult Section(string level)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT SectionName FROM tblSection WHERE GradeLevel = '" + level + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    NESectionList.Add(new SelectListItem { Text = myReader["SectionName"].ToString(), Value = myReader["SectionName"].ToString() });
                }
                return Json(NESectionList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }

        int displayid;
        public JsonResult GetEnrollmentID()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                int maxid;
                int thisid;
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT MAX(id) AS MaxID FROM tblEnrollment", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    try
                    {
                        maxid = Convert.ToInt32(myReader["MaxID"]);
                    }
                    catch (InvalidCastException)
                    {
                        maxid = 0;
                    }
                    
                    thisid = maxid + 1;
                    displayid = 10000 + thisid;
                }
                
                return Json(displayid, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult LoadSched(string section)
        {
            List<tblSchedule> list = new List<tblSchedule>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    String query = @"SELECT * FROM tblSchedule WHERE Section = '" + section + "'";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, con);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var schedules = new tblSchedule();
                        schedules.SubjectCode = row["SubjectCode"].ToString();
                        schedules.DescriptiveTitle = row["DescriptiveTitle"].ToString();
                        schedules.Time = row["Time"].ToString();
                        schedules.Day = row["Day"].ToString();
                        schedules.Teacher = row["Teacher"].ToString();
                        list.Add(schedules);
                    }
                }
                return PartialView("~/Views/Enrollment/_SchedTablePartial.cshtml", list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PartialViewResult LoadSchedPrint(string section)
        {
            List<tblSchedule> list = new List<tblSchedule>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    String query = @"SELECT * FROM tblSchedule WHERE Section = '" + section + "'";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, con);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var schedules = new tblSchedule();
                        schedules.SubjectCode = row["SubjectCode"].ToString();
                        schedules.DescriptiveTitle = row["DescriptiveTitle"].ToString();
                        schedules.Time = row["Time"].ToString();
                        schedules.Day = row["Day"].ToString();
                        schedules.Teacher = row["Teacher"].ToString();
                        list.Add(schedules);
                    }
                }
                return PartialView("~/Views/Enrollment/_SchedTablePrintPartial.cshtml", list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult Create(string academicyear, string enrollmentid, string studentid, string nameofstudent, string studenttype, string status, string gradelevel, string section, string dateenrolled)
        {
            try
            {
                tblEnrollment enrollment = new tblEnrollment
                {
                    AcademicYear = academicyear,
                    EnrollmentID = enrollmentid,
                    StudentID = studentid,
                    NameOfStudent = nameofstudent,
                    StudentType = studenttype,
                    Status = status,
                    GradeLevel = gradelevel,
                    Section = section,
                    DateEnrolled = dateenrolled
                };
                db.tblEnrollments.Add(enrollment);
                db.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}