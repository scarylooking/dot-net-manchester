using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wpug.models
{
    public class ContactFormPage : Page
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
        public string message { get; set; }
        public ContactFormStatus status { get; set; }
        public bool submitted { get; set; }

        public ContactFormPage()
        {
            status = new ContactFormStatus();
            submitted = false;
        }
    }

    public class ContactFormStatus
    {
        public bool email { get; set; }
        public bool message { get; set; }
        public bool name { get; set; }
        public bool recaptcha { get; set; }

        public ContactFormStatus()
        {
            email = true;
            message = true;
            name = true;
            recaptcha = true;
        }
    }
}