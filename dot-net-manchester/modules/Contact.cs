using Nancy;
using Nancy.Helpers;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using wpug.models;
using wpug.utility;
using Newtonsoft.Json;

namespace wpug.modules
{
    public class Contact : NancyModule
    {
        private readonly string pagename = "Contact Us";

        public Contact()
        {
            Post["/contact", true] = async (parameters, ct) =>
            {
                var reCaptchaToken = Request.Form["reCaptchaToken"].Value;

                if (string.IsNullOrWhiteSpace(reCaptchaToken))
                {
                    return Nancy.HttpStatusCode.Forbidden;
                }

                var model = this.Bind<ContactRequest>();
                var response = new ContactResponse();
                var failureFlag = false;

                if (!ReCaptcha.Validate(reCaptchaToken))
                {
                    response.recaptcha = false;
                    failureFlag = true;
                }

                if (string.IsNullOrWhiteSpace(model.emailAddress))
                {
                    response.emailAddress = false;
                    failureFlag = true;
                }

                if (string.IsNullOrWhiteSpace(model.message))
                {
                    response.message = false;
                    failureFlag = true;
                }

                if (string.IsNullOrWhiteSpace(model.name))
                {
                    response.name = false;
                    failureFlag = true;
                }

                if (failureFlag)
                {
                    return Negotiate.WithModel(response).WithStatusCode(Nancy.HttpStatusCode.BadRequest);
                }


                var slackResult = await SendSlackMessage(model);

                if (slackResult)
                {
                }
                else
                {
                }

                return View["contact", model];
            };

            Get["/contact"] = _ => navigateToContactView();
            Get["/contactus"] = _ => navigateToContactView();
            Get["/contact-us"] = _ => navigateToContactView();
        }

        private ContactRequest modelBase()
        {
            return new ContactRequest() { Title = pagename };
        }

        private Negotiator navigateToContactView()
        {
            return View["contact", modelBase()];
        }


        private async Task<bool> SendSlackMessage(ContactRequest model)
        {
            try
            {
                var safeEmail = HttpUtility.HtmlEncode(model.emailAddress);
                var safeName = HttpUtility.HtmlEncode(model.name);
                var safeMessage = HttpUtility.HtmlEncode(model.message);
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
                var postData = Encoding.ASCII.GetBytes(jsonMessage);

                var request = WebRequest.Create(ConfigurationManager.AppSettings["SlackContactWebHook"]);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.ContentLength = postData.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(postData, 0, postData.Length);
                }

                using (var response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var responseStream = response.GetResponseStream();

                    if (responseStream == null)
                    {
                        return false;
                    }

                    using (var reader = new StreamReader(responseStream))
                    {
                        var responseFromServer = reader.ReadToEnd();

                        if (responseFromServer.Equals("ok", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}