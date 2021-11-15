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
            ViewBag.SQLNode = GetSQLServerNode();
            ViewBag.AZName = GetAZName();
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


        private string GetSQLServerNode()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["SQlConnection"].ConnectionString;
            var conn = new SqlConnection(connString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT NodeName FROM sys.dm_os_cluster_nodes where is_current_owner = 1";
            cmd.Connection = conn;
            conn.Open();
            var result = cmd.ExecuteScalar();
            conn.Close();
            
            if( result == null)
            {
                return "<SQLNodeNotFound>";
            }
            return result.ToString();
        }

        private string GetAZName()
        {
            // Read the file as one string.
            string azName = System.IO.File.ReadAllText(@"C:\\Temp\\AZ.txt");
            if (string.IsNullOrEmpty(azName))
                return "<AZNameNotFound>";            

            return azName;
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