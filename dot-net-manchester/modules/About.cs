using Nancy;
using Nancy.Responses.Negotiation;

namespace wpug.modules
{
    public class About : NancyModule
    {
        public About()
        {
            Get["/about"] = _ => navigateToAboutView();
            Get["/about-us"] = _ => navigateToAboutView();
            Get["/aboutus"] = _ => navigateToAboutView();
        }

        public Negotiator navigateToAboutView()
        {
            return View["about.html"];
        }        
    }
}