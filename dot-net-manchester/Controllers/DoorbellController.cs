using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using wpug.Classes;
using wpug.Models;
using System.Configuration;

namespace wpug.Controllers
{
    [RoutePrefix("doorbell")]
    public class DoorbellController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            ViewBag.Times = 0;

            return View();
        }


        [Route("Ring")]
        public async Task<ActionResult> Ring()
        {

            ViewBag.Result = await SendSlackMessage();

            return View("Index");
        }

        private async Task<bool> SendSlackMessage()
        {
            try
            {
                var encoding = new ASCIIEncoding();

                var message = new SlackMessage
                {
                    Channel = "#doorbell",
                    Icon = ":bell:",
                    Text = "Someone at the door!",
                    Username = "Doorbell"
                };


                var jsonMessage = JsonConvert.SerializeObject(message);
                var postData = encoding.GetBytes(jsonMessage);

                var request = WebRequest.Create(ConfigurationManager.AppSettings["DoorbellWebHookUrl"]);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.ContentLength = postData.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(postData, 0, postData.Length);
                }

                using (var response = await request.GetResponseAsync())
                {
                    if ((((HttpWebResponse)response).StatusCode) != HttpStatusCode.OK) return false;

                    var responseStream = response.GetResponseStream();

                    if (responseStream == null) return false;

                    using (var reader = new StreamReader(responseStream))
                    {
                        // Read the content.
                        var responseFromServer = reader.ReadToEnd();

                        if (responseFromServer.ToLowerInvariant() == "ok") return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}