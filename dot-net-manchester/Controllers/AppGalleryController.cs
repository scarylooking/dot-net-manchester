using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wpug.Controllers
{
    [RoutePrefix("appgallery")]
    public class AppGalleryController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}