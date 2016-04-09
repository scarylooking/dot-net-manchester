using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace wpug.utility
{
    public class ReCaptchaResponse
    {
        public string success { get; set; }
    }

    public static class ReCaptcha
    {
        public static bool Validate(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                return false;
            }

            var requestUrl = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", ConfigurationManager.AppSettings["ReCaptchaSecret"], response);
            var request = WebRequest.Create(requestUrl);

            request.Method = "GET";

            using (var httpResponse = request.GetResponse() as HttpWebResponse)
            {

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                var responseStream = httpResponse.GetResponseStream();

                if (responseStream == null)
                {
                    return false;
                }

                using (var reader = new StreamReader(responseStream))
                {
                    var responseFromServer = reader.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    var data = serializer.Deserialize<ReCaptchaResponse>(responseFromServer);
                    return Convert.ToBoolean(data.success);
                }
            }
        }
    }
}