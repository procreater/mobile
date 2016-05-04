using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CordovaSPA.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyResult()
        {

            var result = Helper.Burak;

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Update()
        {

            var result = Helper.Burak;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
