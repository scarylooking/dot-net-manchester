using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using wpug.Models;

namespace wpug.Classes
{
    public class ReCaptchaResponse
    {
        public string success { get; set; }
    }

    public static class ReCaptcha
    {
        public async static Task<bool> Validate(String response)
        {
            if (response == null) return false;

            var requestUrl = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", ConfigurationManager.AppSettings["ReCaptchaSecret"], response);
            var request = WebRequest.Create(requestUrl);

            request.Method = "GET";

            using (var httpResponse = await request.GetResponseAsync())
            {
                if ((((HttpWebResponse)httpResponse).StatusCode) != HttpStatusCode.OK) return false;

                var responseStream = httpResponse.GetResponseStream();

                if (responseStream == null) return false;

                using (var reader = new StreamReader(responseStream))
                {
                    // Read the content.
                    var responseFromServer = reader.ReadToEnd();

                    var serializer = new JavaScriptSerializer();

                    var data = serializer.Deserialize<ReCaptchaResponse>(responseFromServer);

                    return Convert.ToBoolean(data.success);
                }
            }
        }
    }
}