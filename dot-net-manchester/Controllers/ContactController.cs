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
using wpug.Models;
using wpug.Classes;
using System.Configuration;

namespace wpug.Controllers
{
    [RoutePrefix("contact")]
    public class ContactController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            ViewBag.State = 0;

            return View();
        }
        
        [ValidateAntiForgeryToken]
        [Route("Send")]
        public async Task<ActionResult> Send(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var reCaptcha = Request["g-recaptcha-response"];
                
                if (await ReCaptcha.Validate(reCaptcha)) 
                {
                    var result = await SendSlackMessage(model);

                    ViewBag.State = result;

                    if (result == 3)
                    {
                        model = new ContactModel();
                    }
                }
                else
                {
                    ViewBag.State = 4;
                }
            }
            else
            {
                ViewBag.State = 1;
            }

            // The view was invalid
            return View("index");
        }

        private async Task<int> SendSlackMessage(ContactModel model)
        {
            try
            {
                var encoding = new ASCIIEncoding();

                var safeEmail = HttpUtility.HtmlEncode(model.Email);
                var safeName = HttpUtility.HtmlEncode(model.Name);
                var safeMessage = HttpUtility.HtmlEncode(model.Message);
                const string messageFrom = "New message via wpug.uk...";

                var message = new SlackAttachmentMessage();

                var attachment = new SlackAttachment
                {
                    Colour = "#4c9689",
                    Fallback = messageFrom,
                    PreText = messageFrom
                };

                attachment.Fields.Add(new SlackField
                {
                    Short = true,
                    Title = "Email",
                    Value = safeEmail
                });

                attachment.Fields.Add(new SlackField
                {
                    Short = true,
                    Title = "Name",
                    Value = safeName
                });

                attachment.Fields.Add(new SlackField
                {
                    Short = false,
                    Title = "Message",
                    Value = safeMessage
                });

                message.Attachments.Add(attachment);

                var jsonMessage = JsonConvert.SerializeObject(message);
                var postData = encoding.GetBytes(jsonMessage);

                var request = WebRequest.Create(ConfigurationManager.AppSettings["SlackContactWebHook"]);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.ContentLength = postData.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(postData, 0, postData.Length);
                }

                using (var response = await request.GetResponseAsync())
                {
                    if ((((HttpWebResponse)response).StatusCode) != HttpStatusCode.OK) return 1;

                    var responseStream = response.GetResponseStream();

                    if (responseStream == null) return 1;

                    using (var reader = new StreamReader(responseStream))
                    {
                        // Read the content.
                        var responseFromServer = reader.ReadToEnd();

                        if (responseFromServer.ToLowerInvariant() == "ok") return 3;
                    }
                }
            }
            catch (Exception)
            {
                return 2;
            }

            return 1;
        }
    }
}