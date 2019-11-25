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
    public class ESController : Controller
    {

        dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";

        List<SelectListItem> RegisteredStudentList = new List<SelectListItem>();

        // GET: ES
        public ActionResult Index()
        {
            ViewBag.comboboxSearch = GetNamesRegistered();
            return View();
        }
        public SelectList GetNamesRegistered()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT NameOfStudent FROM tblEnrollment", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    RegisteredStudentList.Add(new SelectListItem { Text = myReader["NameOfStudent"].ToString(), Value = myReader["NameOfStudent"].ToString() });
                }
                con.Close();
                return new SelectList(RegisteredStudentList, "Value", "Text"); // return the list object
            }
        }
        public PartialViewResult LoadES(string nameofstudent)
        {

            List<tblEnrollment> list = new List<tblEnrollment>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                     
                    if (nameofstudent == "" || nameofstudent.Length == 0)
                    {
                        String query = @"SELECT * FROM tblEnrollment";
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, con);
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            var enrolledstudent = new tblEnrollment();
                            enrolledstudent.id = Convert.ToInt32(row["id"]);
                            enrolledstudent.StudentID = row["StudentID"].ToString();
                            enrolledstudent.NameOfStudent = row["NameOfStudent"].ToString();
                            enrolledstudent.StudentType = row["StudentType"].ToString();
                            enrolledstudent.GradeLevel = row["GradeLevel"].ToString();
                            enrolledstudent.Section = row["Section"].ToString();
                            enrolledstudent.DateEnrolled = row["DateEnrolled"].ToString();
                            enrolledstudent.Status = row["Status"].ToString();
                            list.Add(enrolledstudent);
                        }
                    }
                    else if (nameofstudent.Length > 0)
                    {
                        String query = @"SELECT * FROM tblEnrollment WHERE NameOfStudent = '" + nameofstudent + "'";
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, con);
                        da.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            var enrolledstudent = new tblEnrollment();
                            enrolledstudent.id = Convert.ToInt32(row["id"]);
                            enrolledstudent.StudentID = row["StudentID"].ToString();
                            enrolledstudent.NameOfStudent = row["NameOfStudent"].ToString();
                            enrolledstudent.StudentType = row["StudentType"].ToString();
                            enrolledstudent.GradeLevel = row["GradeLevel"].ToString();
                            enrolledstudent.Section = row["Section"].ToString();
                            enrolledstudent.DateEnrolled = row["DateEnrolled"].ToString();
                            enrolledstudent.Status = row["Status"].ToString();
                            list.Add(enrolledstudent);
                        }
                    }
                }
                return PartialView("~/Views/ES/_ESTablePartial.cshtml", list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult SelectedES(int? id)
        {
            tblEnrollment enrolledstudent = db.tblEnrollments.FirstOrDefault(x => x.id == id);
            return Json(enrolledstudent, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DropES(string studentid)
        {
            tblEnrollment enrolledstudent = db.tblEnrollments.Where(x => x.StudentID == studentid).FirstOrDefault();
            db.tblEnrollments.Remove(enrolledstudent);
            db.SaveChanges();

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}