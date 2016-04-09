using Nancy;
using wpug.models;

namespace wpug.modules
{
    public class Index : NancyModule
    {
        public Index()
        {
            Get["/"] = _ => {

                var page = new Page() {
                    Title = "Home"
                };

                return View["Index", page];
            };
        }
    }
}