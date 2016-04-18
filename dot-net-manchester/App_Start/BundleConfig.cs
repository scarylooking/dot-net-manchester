using System.Web;
using System.Web.Optimization;

namespace wpug
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/js/jqueryval").Include(
                        "~/scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                        "~/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                      "~/scripts/bootstrap.js",
                      "~/scripts/date.js",
                      "~/scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/js/moment").Include(
                        "~/scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/js/knockout").Include(
                      "~/scripts/knockout.js",
                      "~/scripts/knockout.mapping.js"));

            bundles.Add(new ScriptBundle("~/js/meetup-event").Include(
                      "~/scripts/meetup-event.js"));

            bundles.Add(new StyleBundle("~/css/min").Include(
                      "~/css/bootstrap-cosmo.css",
                      "~/css/font-awesome.css",
                      "~/css/site.css"));

        }
    }
}
