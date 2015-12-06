using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace wpug.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "If you don't tell us your name how will we know who you are?")]
        public string Name { get; set; }

        [Required(ErrorMessage = "If you don't give us your email address how can we reply to you?")]
        public string Email { get; set; }

        [Required(ErrorMessage = "It's a bit pointless contacting us without a message!")]
        public string Message { get; set; }

        public int State = 0;
    }
}