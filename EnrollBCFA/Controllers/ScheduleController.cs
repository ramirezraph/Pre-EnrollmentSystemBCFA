using EnrollBCFA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EnrollBCFA.Controllers
{
    public class ScheduleController : Controller
    {

        dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";
        

        List<SelectListItem> GradeLevelList = new List<SelectListItem>();
        List<SelectListItem> SectionList = new List<SelectListItem>();
        List<SelectListItem> StudentTypeList = new List<SelectListItem>();
        List<SelectListItem> SubjectCodeList = new List<SelectListItem>();
        List<SelectListItem> SubjectTeacherList = new List<SelectListItem>();
        List<SelectListItem> TeacherIDList = new List<SelectListItem>();
        string adviserName;
        string openSchoolYear;
        string descriptivetitle;

        // GET: Schedule
        public ActionResult Index()
        {
            ViewBag.vartype = GetOptions();
            ViewBag.varlevel = GradeLevelList;
            ViewBag.varsection = SectionList;
            ViewBag.subjectcode = SubjectCodeList;
            ViewBag.teachername = SubjectTeacherList;
            ViewBag.teacherid = TeacherIDList;
            return View();
        }
        public JsonResult SelectedRowSched(int? id)
        { 
            tblSchedule schedule = db.tblSchedules.FirstOrDefault(x => x.SchedID == id);
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult LoadSched(string section)
        {

            List<tblSchedule> list = new List<tblSchedule>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    String query = @"SELECT * FROM tblSchedule WHERE Section = '"+section+"'";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, con);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var schedules = new tblSchedule();
                        schedules.SchedID = Convert.ToInt32(row["SchedID"]);
                        schedules.SubjectCode = row["SubjectCode"].ToString();
                        schedules.DescriptiveTitle = row["DescriptiveTitle"].ToString();
                        schedules.Time = row["Time"].ToString();
                        schedules.Day = row["Day"].ToString();
                        schedules.GradeLevel = row["GradeLevel"].ToString();
                        schedules.Section = row["Section"].ToString();
                        schedules.Teacher = row["Teacher"].ToString();
                        list.Add(schedules);
                    }
                }
                return PartialView("~/Views/Schedule/_SchedTablePartial.cshtml", list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult GetOpenSchoolYear()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT SchoolYear FROM tblSchoolYear WHERE status='Open'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    openSchoolYear = myReader["SchoolYear"].ToString();
                }
                return Json(openSchoolYear, JsonRequestBehavior.AllowGet);
            }
        }

        public SelectList GetOptions()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT DISTINCT SType FROM tblSection", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    StudentTypeList.Add(new SelectListItem { Text = myReader["SType"].ToString(), Value = myReader["SType"].ToString() });
                }
                con.Close();
                return new SelectList(StudentTypeList, "Value", "Text"); // return the list object
            }
        }
        public JsonResult GradeLevel(string type)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT DISTINCT GradeLevel FROM tblSection WHERE SType = '"+ type + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    GradeLevelList.Add(new SelectListItem { Text = myReader["GradeLevel"].ToString(), Value = myReader["GradeLevel"].ToString() });
                }
                return Json(GradeLevelList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult Section(string level)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT SectionName FROM tblSection WHERE GradeLevel = '"+ level + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    SectionList.Add(new SelectListItem { Text = myReader["SectionName"].ToString(), Value = myReader["SectionName"].ToString() });
                }
                return Json(SectionList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult SubjectCode(string type)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT SubjectCode FROM tblSubjectList WHERE SubjectType = '" + type + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    SubjectCodeList.Add(new SelectListItem { Text = myReader["SubjectCode"].ToString(), Value = myReader["SubjectCode"].ToString() });
                }
                return Json(SubjectCodeList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult Teacher(string code)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT SubjectTeacher FROM tblSubjectList WHERE SubjectCode = '" + code + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    SubjectTeacherList.Add(new SelectListItem { Text = myReader["SubjectTeacher"].ToString(), Value = myReader["SubjectTeacher"].ToString() });
                }
                return Json(SubjectTeacherList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        public JsonResult TeacherID(string name)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT TeacherID FROM tblTeacher WHERE TeacherName = '" + name + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    TeacherIDList.Add(new SelectListItem { Text = myReader["TeacherID"].ToString(), Value = myReader["TeacherID"].ToString() });
                }
                return Json(TeacherIDList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
        
        public JsonResult GetSectionAdviser(string sectionname)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT SectionAdviser FROM tblSection WHERE SectionName = '" + sectionname + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    adviserName = myReader["SectionAdviser"].ToString();
                }
                return Json(adviserName, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDescriptiveTitle(string code)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT SubjectName FROM tblSubjectList WHERE SubjectCode = '" + code + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    descriptivetitle = myReader["SubjectName"].ToString();
                }
                return Json(descriptivetitle, JsonRequestBehavior.AllowGet);
            }
        }


        // CRUD For Schedule
        public JsonResult CheckCreateDuplicate(string academicyear, string section, string subjectcode, string teacher)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("SELECT * FROM tblSchedule WHERE AcademicYear = '" + academicyear + "' AND Section = '" + section + "' AND SubjectCode = '" + subjectcode + "' AND Teacher = '" + teacher + "'", con);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                    }
                    return Json(JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult Create(string academicyear, string type, string gradelevel, string  section, string adviser, string subjectcode, string descriptivetitle, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, string day, string time, string timefrom, string timeto, string teacherid, string teacher)
        {
            try
            {
                tblSchedule schedule = new tblSchedule
                {
                   AcademicYear = academicyear,
                   Type = type,
                   GradeLevel = gradelevel,
                   Section = section,
                   Adviser = adviser,
                   SubjectCode = subjectcode,
                   DescriptiveTitle = descriptivetitle,
                   Monday = monday,
                   Tuesday = tuesday,
                   Wednesday = wednesday,
                   Thursday = thursday,
                   Friday = friday,
                   Saturday = saturday,
                   Day = day,
                   Time = time,
                   TimeFrom = timefrom,
                   TimeTo = timeto,
                   TeacherID = teacherid,
                   Teacher = teacher
                   };
                   db.tblSchedules.Add(schedule);
                   db.SaveChanges();
                   return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult Delete(string academicyear, string section, string subjectcode, string teacher)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("DELETE FROM tblSchedule WHERE AcademicYear = '"+ academicyear + "' AND Section = '"+section+"' AND SubjectCode = '"+subjectcode+"' AND Teacher = '"+teacher+"'", con);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                    }
                    return Json(JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult Update(string academicyear, string section, string subjectcode, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, string day, string time, string timefrom, string timeto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand2 = new SqlCommand("UPDATE tblSchedule SET Monday = '" + monday + "', Tuesday = '" + tuesday + "', Wednesday = '" + wednesday + "', Thursday = '" + thursday + "', Friday = '" + friday + "', Saturday = '" + saturday + "', Day = '" + day + "', Time = '" + time + "', TimeFrom = '" + timefrom + "', TimeTo = '" + timeto + "' WHERE AcademicYear = '" + academicyear + "' AND Section = '" + section + "' AND SubjectCode = '" + subjectcode + "'", con);
                    myReader = myCommand2.ExecuteReader();
                    while (myReader.Read())
                    {

                    }
                    return Json(JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        

    }
}