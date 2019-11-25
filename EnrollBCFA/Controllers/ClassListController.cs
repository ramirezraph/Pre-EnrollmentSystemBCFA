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
    public class ClassListController : Controller
    {
        dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";


        List<SelectListItem> GradeLevelList = new List<SelectListItem>();
        List<SelectListItem> SectionList = new List<SelectListItem>();


        // GET: ClassList
        public ActionResult Index()
        {
            ViewBag.varlevelclass = GetGradeLevel();
            ViewBag.varsectionclass = SectionList;
            return View();
        }
        public PartialViewResult LoadClass(string sectionname)
        {

            List<tblEnrollment> list = new List<tblEnrollment>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    //String query = @"SELECT * FROM tblEnrollment WHERE SectionName = '"+sectionname+"'";
                    String query = @"SELECT ROW_NUMBER() OVER (ORDER BY NameOfStudent) AS [No.], StudentID, NameOfStudent FROM tblEnrollment WHERE Section = '"+sectionname+"'";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, con);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var classreport = new tblEnrollment();
                        classreport.RowNumber = row["No."].ToString();
                        classreport.StudentID = row["StudentID"].ToString();
                        classreport.NameOfStudent = row["NameOfStudent"].ToString();
                        list.Add(classreport);
                    }
                }
                return PartialView("~/Views/ClassList/_ClassTablePartial.cshtml", list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult PrintClassRecord()
        {
            DateTime today = DateTime.Today;
            string datetoday = today.ToString("d");
            ViewBag.DateNow = datetoday;
            return View();
        }
        public PartialViewResult LoadClassPrint(string section)
        {
            List<tblEnrollment> list = new List<tblEnrollment>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    //String query = @"SELECT * FROM tblEnrollment WHERE SectionName = '"+sectionname+"'";
                    String query = @"SELECT ROW_NUMBER() OVER (ORDER BY NameOfStudent) AS [No.], StudentID, NameOfStudent FROM tblEnrollment WHERE Section = '" + section + "'";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, con);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var classreport = new tblEnrollment();
                        classreport.RowNumber = row["No."].ToString();
                        classreport.StudentID = row["StudentID"].ToString();
                        classreport.NameOfStudent = row["NameOfStudent"].ToString();
                        list.Add(classreport);
                    }
                }
                return PartialView("~/Views/ClassList/_ClassTablePrintPartial.cshtml", list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SelectList GetGradeLevel()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT DISTINCT GradeLevel, Len(GradeLevel) FROM tblSection GROUP BY GradeLevel ORDER BY Len(GradeLevel) ASC, GradeLevel", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    GradeLevelList.Add(new SelectListItem { Text = myReader["GradeLevel"].ToString(), Value = myReader["GradeLevel"].ToString() });
                }
                con.Close();
                return new SelectList(GradeLevelList, "Value", "Text"); // return the list object
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
                    SectionList.Add(new SelectListItem { Text = myReader["SectionName"].ToString(), Value = myReader["SectionName"].ToString() });
                }
                return Json(SectionList, JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }
    }
}