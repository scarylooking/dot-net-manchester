using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wpug.models
{
    public class ContactRequest : Page
    {
        public string name { get; set; }
        public string emailAddress { get; set; }
        public string message { get; set; }
    }   
}