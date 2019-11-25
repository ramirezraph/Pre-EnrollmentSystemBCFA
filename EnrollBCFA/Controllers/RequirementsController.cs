using EnrollBCFA.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EnrollBCFA.Controllers
{
    public class RequirementsController : Controller
    {
        private dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";

        // GET: Requirements
        public ActionResult Index()
        {
            return View(db.tblRequirements.OrderBy(x => x.Name).ToList());
        }
        public JsonResult UpdateReq(string id, bool f137 = false, bool nso = false, bool gm = false, bool gc = false )
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand2 = new SqlCommand("UPDATE tblRequirements SET Form137 = '" + f137 + "', NSO = '"+nso+"', GoodMoral = '"+gm+"', GradeCard = '"+gc+"' WHERE StudentID = '"+id+"'", con);
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