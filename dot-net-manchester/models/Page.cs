using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wpug.models
{
    public class Page
    {
        private string _name;

        public string Title {
            get {
                return "WPUGNW" + (string.IsNullOrWhiteSpace(_name) ? "" : $" &#124; {_name}");
            }
            set {
                _name = value;
            }
        }
        public string Description { get; set; }
    }
}