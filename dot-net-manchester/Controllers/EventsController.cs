using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace wpug.Controllers
{

    [RoutePrefix("events")]
    public class EventsController : Controller
    {
        [Route("{eventID?}")]
        public ActionResult Index(string eventId = null)
        {
            if (eventId == null)
            {
                return View();
            }

            return View(eventId);
        }
       
    }
}