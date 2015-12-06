using System.Web.Mvc;
using System.Web.Routing;

namespace wpug
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();



            //routes.MapRoute("Events", "Events/{id}", new { controller = "Events", action = "Index", id = UrlParameter.Optional } );
            //routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional } );
        }
    }
}
