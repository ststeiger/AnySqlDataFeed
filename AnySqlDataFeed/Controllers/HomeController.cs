
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AnySqlDataFeed.Controllers
{


    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }


        public ContentResult Serialize()
        {
            AnySqlDataFeed.OData.TableDataFeed.GetSerializationData("T_Admin", this.HttpContext.Response.Output);

            return Content("OK");
        }


    } // End Class HomeController : Controller


} // End Namespace AnySqlDataFeed.Controllers
