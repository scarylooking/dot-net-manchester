using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wpug.Controllers
{
    [RoutePrefix("about")]
    public class AboutController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}