using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Castrol.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string CastrolContextConnectionString { get; set; }
        public static string FtpServer { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CastrolContextConnectionString = ConfigurationManager.ConnectionStrings["CastrolContext"].ConnectionString;
            if (string.IsNullOrEmpty(CastrolContextConnectionString))
                throw new MissingFieldException("The CastrolContext connectionString is required!");

            FtpServer = ConfigurationManager.AppSettings["FtpServer"];
            if (string.IsNullOrEmpty(FtpServer))
                throw new MissingFieldException("The FTP Server is required!");
        }
    }
}
