using System.Web;
using System.Web.Optimization;

namespace NexusMD.MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/adminlte/css/adminlte.min.css",
                      "~/adminlte/plugins/fullcalendar/main.min.css",
                      "~/adminlte/plugins/fullcalendar-daygrid/main.min.css",
                      "~/adminlte/plugins/fullcalendar-bootstrap/main.min.css",
                      "~/adminlte/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new ScriptBundle("~/adminlte/js").Include(
                       "~/adminlte/js/adminlte.min.js",
                       "~/adminlte/plugins/fullcalendar/main.js",
                       "~/adminlte/plugins/fullcalendar-daygrid/main.js",
                       "~/adminlte/plugins/fullcalendar-bootstrap/main.js"
                       ));
        }
    }
}
