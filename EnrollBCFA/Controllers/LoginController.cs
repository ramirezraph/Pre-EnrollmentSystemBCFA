using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrollBCFA.Models;
using System.Data.SqlClient;

namespace EnrollBCFA.Controllers
{
    public class LoginController : Controller
    {
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Authorize(EnrollBCFA.Models.AccountMetaData userModelAccount)
        {
            using (dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData())
            {
                var userDetails = db.Accounts.FirstOrDefault(x => x.Username == userModelAccount.Username && x.Password == userModelAccount.Password);
                if (userDetails == null)
                {
                    userModelAccount.LoginErrorMessage = "Username or Password is incorrect.";
                    return View("Index", userModelAccount);
                }
                else
                {
                    Session["AccountID"] = userDetails.AccountID;
                    Session["Username"] = userDetails.Username;
                    Session["FirstName"] = userDetails.FirstName;
                    Session["LastName"] = userDetails.LastName;
                    Session["Password"] = userDetails.Password;
                    Session["AccountCreated"] = userDetails.AccountCreated;

                    updateLastLogin(Convert.ToInt32(Session["AccountID"]));

                    return RedirectToAction("Index", "Bcfa");
                }
            }
        }
        public ActionResult updateLastLogin(int accountid)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string datetoday = DateTime.Now.ToString();
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("UPDATE tblAccounts SET LastLogin = '" + datetoday + "' WHERE AccountID = '" + accountid + "'", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                }
                return Json(JsonRequestBehavior.AllowGet); // return the list object in json form
            }
        }

        public ActionResult SetSessionVariable(string uservalue, string fvalue, string lvalue)
        {
            Session["Username"] = uservalue;
            Session["FirstName"] = fvalue;
            Session["LastName"] = lvalue;

            return this.Json(new { success = true });
        }
        public ActionResult SetSessionPassword(string newpassword)
        {
            Session["Password"] = newpassword;
            return this.Json(new { success = true });
        }
        // use to logout then reset the account ID
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");

            // put this code for logout button:
            /*
             * <a href="@Url.Action("Logout", "Login")">Logout</a>
             */
        }
    }   
}