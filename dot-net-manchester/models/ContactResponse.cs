using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wpug.models
{
    public class ContactResponse
    {
        public bool name { get; set; }
        public bool emailAddress { get; set; }
        public bool message { get; set; }
        public bool recaptcha { get; set; }
        public bool sent { get; set; }
        public bool failed { get; set; }

        public ContactResponse()
        {
            name = true;
            emailAddress = true;
            message = true;
            recaptcha = true;
            sent = false;
            failed = false;
        }
    }
}