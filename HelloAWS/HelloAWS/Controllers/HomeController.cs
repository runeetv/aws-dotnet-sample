using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloAWS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.LocalServer = System.Environment.MachineName;
            ViewBag.SQLServer = GetSQLServerName();
            return View();
        }

        private string GetSQLServerName()
        {
            var connString =  System.Configuration.ConfigurationManager. ConnectionStrings["SQlConnection"].ConnectionString;
            var conn = new SqlConnection(connString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select @@SERVERNAME";
            cmd.Connection = conn;
            conn.Open();
            var result = cmd.ExecuteScalar();
            conn.Close();
            return result.ToString();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}