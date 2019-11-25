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
    public class ManageAccountController : Controller
    {
        dbBcfaSystemEntitiesData db = new dbBcfaSystemEntitiesData();
        string ConnectionString = @"Data Source=RAMIREZ-PC;Initial Catalog=dbBcfaSystem;User ID=bcfa;Password=pass";


        // GET: ManageAccount
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult LoadAccount()
        {

            List<Account> list = new List<Account>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    String query = @"SELECT * FROM tblAccounts";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, con);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var accounts = new Account();
                        accounts.Password = row["Password"].ToString();
                        accounts.AccountID = Convert.ToInt32(row["AccountID"]);
                        accounts.Username = row["Username"].ToString();
                        accounts.FirstName = row["FirstName"].ToString();
                        accounts.LastName = row["LastName"].ToString();
                        accounts.AccountCreated = row["AccountCreated"].ToString();
                        accounts.LastLogin = row["LastLogin"].ToString();
                        list.Add(accounts);
                    }
                }
                return PartialView("~/Views/ManageAccount/_AccountTablePartial.cshtml", list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult SelectedAccount(int? id)
        {
            Account accounts = db.Accounts.FirstOrDefault(x => x.AccountID == id);
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int? accountid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("DELETE FROM tblAccounts WHERE AccountID = '" + accountid + "'", con);
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

        int newaccid;
        public JsonResult GetAccountID()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                int accountid;
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT MAX(AccountID) AS MaxID FROM tblAccounts", con);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    try
                    {
                        accountid = Convert.ToInt32(myReader["MaxID"]);
                    }
                    catch (InvalidCastException)
                    {
                        accountid = 10000;
                    }
                    newaccid = accountid + 1;
                }

                return Json(newaccid, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Create(int accountid, string firstname, string lastname, string username, string lastlogin ,string password, string accountcreated)
        {
            try
            {
                Account account = new Account
                {
                    AccountID = accountid,
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Password = password,
                    LastLogin = lastlogin,
                    AccountCreated = accountcreated
                };

                db.Accounts.Add(account);
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