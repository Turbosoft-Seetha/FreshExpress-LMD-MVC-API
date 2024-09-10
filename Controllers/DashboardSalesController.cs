using Org.BouncyCastle.Asn1.Ocsp;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using System;
using System.Configuration;
using System.Data;
using System.Web.Mvc;

namespace MVC_API.Controllers
{
    public class DashboardSalesController : Controller
    {

        public ActionResult SalesReport()
        {
          
            return View();
        }

        public ActionResult GetReport()
        {
            var s = Server.MapPath("../Dashboard/license.key");
            Stimulsoft.Base.StiLicense.LoadFromFile(s);

            var report = StiReport.CreateNewDashboard();
            var path = Server.MapPath("../Dashboard/SalesDashboard.mrt");
            report.Load(path);

            string url = ConfigurationManager.AppSettings.Get("FE_Connection");
          

            ((StiSqlDatabase)report.Dictionary.Databases["Fresh Express"]).ConnectionString = url;

            return StiMvcViewer.GetReportResult(report);
        }

        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }

        public ActionResult RouteReport()
        {
            return View();
        }

        public ActionResult GetRouteReport()
        {
            var s = Server.MapPath("../Dashboard/license.key");
            Stimulsoft.Base.StiLicense.LoadFromFile(s);

            var report = StiReport.CreateNewDashboard();
            var path = Server.MapPath("../Dashboard/DeliveryByCustomer.mrt");
            report.Load(path);

            //report["@para2"] = ResponseID.ToString();
            //e.Report = report;

            string url = ConfigurationManager.AppSettings.Get("FE_Connection");

            ((StiSqlDatabase)report.Dictionary.Databases["Fresh Express"]).ConnectionString = url;

            return StiMvcViewer.GetReportResult(report);
        }

        public ActionResult RouteViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
    }
}


